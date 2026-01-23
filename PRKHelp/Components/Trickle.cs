namespace PRKHelp.Components
{
    public class Trickle : Component
    {
        DB DB;
        Dictionary<string, float> StatValues;

        public Trickle(DB _db)
        {
            DB = _db;
            LoadItems();
            ParamSyntax = $"/trickle stat amount stat amount stat amount";
            StatValues = CreateEmptyStatValues();
        }
        public override (int, string) ValidateParams(string[] _params)
        {
            _params = _params.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (_params.Length > 8)
                return (-1, $"Error. Can only input 4 Stats at a maximum");

            if (_params.Length == 0 || _params.Length % 2 != 0)
                return (-1, $"Error. Wrong input amount. Check your inputs, should match {ParamSyntax}");

            for(var i=1; i< _params.Length+1; i++)
            {
                if (i % 2 != 0)
                {
                    string cleanParam = ParseParam(_params[i-1]);
                    if (!StatValues.ContainsKey(cleanParam))
                        return (-1, $"Error. Invalid stat. Check your inputs, should match {ParamSyntax}");
                }
                else 
                {
                    bool isInt = Int32.TryParse(_params[i-1], out int numValue);
                    if (!isInt)
                        return (-1, $"Error. Invalid value. Check your inputs, should match {ParamSyntax}");
                    if (numValue < 1)
                        return (-1, $"Error. Values must be greater than 0");
                }
            }
            return (1, "true");
        }
        public override int Process(string[] _params)
        {
            // Convert inputs into array for DB query
            for(var i=0; i< _params.Length; i+=2)
            {
                StatValues[ParseParam(_params[i])] = Int32.Parse(_params[i+1]);
            }
            float[] values = new float[6]{
                StatValues["agility"],
                StatValues["intelligence"],
                StatValues["psychic"],
                StatValues["stamina"],
                StatValues["strength"],
                StatValues["sense"],
            };
            Dictionary<string, Dictionary<string, float>> trickleValues = GetTrickleAmounts(values);


            // Map results into readable window style text
            string superIndent = $"                                                  ";
            string paramsString = String.Join(" ", _params);
            
            // Create two different sections for trickle info
            OutputStrings[0] = $"<a href=\"text://Trickle Results {paramsString}<br>";
            OutputStrings.Add($"<a href=\"text://Trickle Results {paramsString}<br>");

            // Create custom section in gear section for treatment and comp lit
            bool headerMade = false;
            if(trickleValues.ContainsKey("Combat & Healing"))
            {
                if(trickleValues["Combat & Healing"].ContainsKey("Treatment"))
                {
                    OutputStrings[0] += $"<br>{HighlightColor}Treatment & Comp. Liter{EndColor}<div align=right>";
                    OutputStrings[0] += $"Treatment | {ValueColor}{(trickleValues["Combat & Healing"]["Treatment"] / 4).ToString("N2")}{EndColor}{superIndent}<br>";
                    headerMade = true;
                }
            }
            if (trickleValues.ContainsKey("Trade & Repair"))
            {
                if (trickleValues["Trade & Repair"].ContainsKey("Comp. Liter"))
                {
                    if (!headerMade)
                            OutputStrings[0] += $"<br>{HighlightColor}Treatment & Comp. Liter{EndColor}<div align=right>";
                    OutputStrings[0] += $"Comp. Liter | {ValueColor}{(trickleValues["Trade & Repair"]["Comp. Liter"] / 4).ToString("N2")}{EndColor}{superIndent}<br>";
                }
            }
            if (headerMade)
                OutputStrings[0] += $"</div>";

            // Populate windows accordingly for all other stats
            foreach (KeyValuePair<string, Dictionary<string, float>> statGroup in trickleValues)
            {
                int pageIndex = 0;
                if (statGroup.Key == "Trade & Repair" || statGroup.Key == "Nanos & Casting" || statGroup.Key == "Combat & Healing" || statGroup.Key == "Body & Defense")
                    pageIndex = 1;

                if(trickleValues[statGroup.Key].Count > 0)
                    OutputStrings[pageIndex] += $"<br>{HighlightColor}{statGroup.Key}{EndColor}<div align=right>";

                foreach (KeyValuePair<string, float> stat in trickleValues[statGroup.Key]) 
                {
                    OutputStrings[pageIndex] += $"{stat.Key} | {ValueColor}{(stat.Value/4).ToString("N2")}{EndColor}{superIndent}<br>";
                }
                OutputStrings[pageIndex] += $"</div>";
            }
            OutputStrings[0] += $"\">Trickle Results | Gear / Imps</a>";
            OutputStrings[1] += $"\">Trickle Results | Combat / Nanos / Trade</a>";

            StatValues = CreateEmptyStatValues();
            return 1;
        }

        static string ParseParam(string _param)
        {
            switch (_param.ToLower())
            {
                case "agi":
                case "agility":
                    return "agility";
                case "int":
                case "intel":
                case "intelligence":
                    return "intelligence";
                case "psy":
                case "psychic":
                    return "psychic";
                case "sta":
                case "stam":
                case "stamina":
                    return "stamina";
                case "str":
                case "strength":
                    return "strength";
                case "sen":
                case "sens":
                case "sense":
                    return "sense";
                default:
                    return _param;
            }
        }

        static Dictionary<string, float> CreateEmptyStatValues()
        {
            return new Dictionary<string, float>()
            {
                {"agility", 0},
                {"intelligence", 0},
                {"psychic", 0},
                {"stamina", 0},
                {"strength", 0},
                {"sense", 0},
            };
        }
        static Dictionary<string, Dictionary<string, float>> GetTrickleAmounts(float[] _statValues)
        {

            string query = $"SELECT group_name, name, amount_agility, amount_intelligence, amount_psychic, amount_stamina, amount_strength, amount_sense, " +
                            $"(amount_agility * {_statValues[0]} + amount_intelligence * {_statValues[1]} + amount_psychic * {_statValues[2]} + amount_stamina * {_statValues[3]} + amount_strength * {_statValues[4]} + amount_sense * {_statValues[5]}) AS amount " +
                            $"FROM Trickle GROUP BY id, group_name, name, amount_agility, amount_intelligence, amount_psychic, amount_stamina, amount_strength,amount_sense " +
                            $"HAVING amount > 0 ORDER BY id";

            return DB.QueryTrickle(query);
        }

        static void LoadItems()
        {
            DB.InsertSQLFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\SQL\\Trickle.sql");
        }
    }
}
