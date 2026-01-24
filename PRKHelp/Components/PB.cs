namespace PRKHelp.Components
{
    public class PocketBoss
    {
        public int ID { get; set; }
        public string name {  get; set; }
        public string playfield { get; set; }
        public string mobType { get; set; }
        public int level { get; set; }
        public string location { get; set; }
    }

    public class PB : Component
    {
        DB DB;
        public PB(DB _db)
        {
            DB = _db;
            LoadItems();
        }

        public override (int, string) ValidateParams(string[] _params)
        {
            return (1, "true");
        }


        public override int Process(string[] _params)
        {
            _params = _params.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            // Pattern was linked in command
            if (_params[0] == "<a")
            {
                int startingIndex = 0;
                int endingIndex = 0;
                for (var x = 1; x < _params.Length; x++)
                {
                    if (_params[x].Contains("'"))
                    {
                        if (startingIndex == 0)
                        {
                            startingIndex = x;
                            if (_params[x][_params[x].Length - 1].ToString() == ">") // '</a> ending of itemref
                            {
                                endingIndex = x;
                                break;
                            }
                        }
                        else
                            endingIndex = x;
                    }
                }
                _params[startingIndex] = _params[startingIndex].Replace("'", "");
                _params[endingIndex] = _params[endingIndex][..^5];
                _params = _params[startingIndex..(endingIndex+1)];
            }
            for (var i = 0; i < _params.Length; i++)
                _params[i] = _params[i].Replace("'", "");

            List<PocketBoss> pocketBosses = GetPocketBoss(_params);

            if (pocketBosses.Count < 1)
            {
                OutputStrings[0] = $"No matching pocket boss found.";
                return 1;
            }

            // If multiple matches return a window with script command for specific bosses
            if(pocketBosses.Count > 1)
            {
                OutputStrings[0] = $"<a href=\"text://Pocket Boss Search Results ({ValueColor}{pocketBosses.Count}{EndColor})<br><br>";
                foreach(PocketBoss boss in pocketBosses)
                {
                    OutputStrings[0] += $"{Indent}<a href='chatcmd:///pocketboss {boss.name}'>{boss.name}</a><br>";
                }
                OutputStrings[0] += $"\">Pocket Boss Search Results ({pocketBosses.Count})</a>";

                return 1;
            }

            // If only one boss matched search
            PocketBoss pocketBoss = pocketBosses[0];
            List<AOItem> symbiants = GetItemsOfPB(pocketBoss.ID);

            OutputStrings[0] = $"<a href=\"text://Remains of {pocketBoss.name} - Level {ValueColor}{pocketBoss.level}{EndColor}<br><br>";
            OutputStrings[0] += $"{HighlightColor}Location:{EndColor} {pocketBoss.playfield}, {pocketBoss.location}<br>";
            OutputStrings[0] += $"{HighlightColor}Found on:{EndColor} {pocketBoss.mobType}<br><br>";
            foreach (AOItem symbiant in symbiants)
            {
                OutputStrings[0] += $"{BuildItemRef(symbiant.lowid, symbiant.highid, symbiant.lowql, symbiant.name)} ({ValueColor}{symbiant.lowql}{EndColor})<br>";
            }
            OutputStrings[0] += $"\">Remains of {pocketBoss.name}</a>";
            return 1;
        }

        static void LoadItems()
        {
            DB.InsertSQLFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\SQL\\Pocketboss.sql");
            DB.InsertSQLFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\SQL\\Playfields.sql");
        }

        static List<AOItem> GetItemsOfPB(int _bossID)
        {
            string query = $"SELECT a.* FROM Symbiants p " +
                            $"LEFT JOIN Items a ON p.item_id = a.highid WHERE pocketboss_id = {_bossID} " +
                            "ORDER BY a.highql DESC, a.name ASC";

            return DB.QuerySymbiantsByPocketBoss(query);
        }

        static List<PocketBoss> GetPocketBoss(string[] _name)
        {
            string likeString = $"LIKE '%{_name[0]}%'";
            for (int i = 1; i < _name.Length; i++)
                likeString += $" AND name LIKE '%{_name[i]}%'";

            string query = $"SELECT p1.*, p2.long_name FROM Pocketboss p1 LEFT JOIN Playfields p2 ON p1.playfield_id = p2.id " +
                            $"WHERE name {likeString} ORDER BY name ASC";

            return DB.QueryPocketBoss(query);
        }
    }
}
