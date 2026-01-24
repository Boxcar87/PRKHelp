using System.Diagnostics;

namespace PRKHelp.Components
{
    public class Symbiant : Component
    {
        DB DB;
        public Symbiant(DB _db) 
        {
            ParamSyntax = "/symbiant name";
            DB = _db;
            LoadItems();
        }

        //
        public override (int, string) ValidateParams(string[] _params)
        {
            return (1, "true");
        }

        public override int Process(string[] _params)
        {
            _params = _params.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            for (int i=0; i<_params.Length; i++)
                _params[i] = TranslateSlotName(_params[i]);
            string symbiantParam = String.Join(" ", _params);
            List<(string, AOItem)> symbiantList = GetSymbiantsByName(_params);
            if(symbiantList.Count < 1)
            {
                OutputStrings[0] = $"No matching symbiants found.";
                return 1;
            }

            int index = 0;
            int pageFirstItem = 1;
            int itemIndex = 0;
            OutputStrings[0] = $"<a href=\"text://Symbiant Search Results ({ValueColor}{symbiantList.Count}{EndColor})<br><br>";
            foreach((string, AOItem) symbiant in symbiantList)
            {
                itemIndex++;
                OutputStrings[index] += $"{BuildItemRef(symbiant.Item2.lowql, symbiant.Item2.highql, symbiant.Item2.lowql, symbiant.Item2.name)} ({ValueColor}{symbiant.Item2.lowql}{EndColor})<br>";
                OutputStrings[index] += $"{HighlightColor}Found on{EndColor} <a href='chatcmd:///pocketboss {symbiant.Item1}'>{symbiant.Item1}</a><br><br>";
                if(OutputStrings[index].Length > 3500)
                {
                    Debug.WriteLine(OutputStrings[index].Length);
                    index++;
                    OutputStrings[index-1] += $"\">Symbiant Search Results ({pageFirstItem} - {itemIndex} of {symbiantList.Count})</a>";
                    OutputStrings.Add($"<a href=\"text://Symbiant Search Results ({ValueColor}{symbiantList.Count}{EndColor})<br><br>");
                    pageFirstItem = itemIndex+1;
                }
            }
            OutputStrings[index] += $"\">Symbiant Search Results ({pageFirstItem} - {itemIndex} of {symbiantList.Count})</a>";
            return 1;
        }

        static string TranslateSlotName(string _string)
        {
            Dictionary<string, string> slots = new Dictionary<string, string>()
            {
                {"eye", "ocular"},
                {"head", "brain"},
                {"rarm", "right arm"},
                {"larm", "left arm"},
                {"rwrist", "right wrist"},
                {"lwrist", "left wrist"},
                {"rhand", "right hand"},
                {"lhand", "left hand"},
                {"leg", "thigh"},
                {"legs", "thigh"},
                {"arty", "artillery"}
            };

            if (slots.TryGetValue(_string, out string translated))
                return translated;
            else
                return _string;
        }
        static void LoadItems()
        {
            DB.InsertSQLFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\SQL\\Symbiants.sql");
        }

        static List<(string, AOItem)> GetSymbiantsByName(string[] _name)
        {
            string likeString = $"LIKE '%{_name[0]}%'";
            for (int i = 1; i < _name.Length; i++)
                likeString += $" AND a.name LIKE '%{_name[i]}%'";

            string query = $"SELECT a.*, p2.name AS pocketboss_name FROM Symbiants p " +
                            $"LEFT JOIN Items a ON p.item_id = a.highid " +
                            $"LEFT JOIN Pocketboss p2 ON p.pocketboss_id = p2.id " +
                            $"WHERE a.name {likeString} " +
                            $"ORDER BY a.highql DESC, a.name ASC";

            return DB.QuerySymbiant(query);
        }
    }
}
