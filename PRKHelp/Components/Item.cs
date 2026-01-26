namespace PRKHelp.Components
{
    public class AOItem
    {
        internal int lowid { get; set; }
        internal int highid { get; set; }
        internal int lowql { get; set; }
        internal int highql { get; set; }
        internal string name { get; set; }
        internal int icon { get; set; }
    }

    public class Item : Component
    {
        DB DB;
        public Item(DB _db)
        {
            LoadItems();
            DB = _db;
        }

        // First input for Items can be int or string and spaces in names count as separate params
        // Returning true cause Process handles orienting the parameters
        public override (int, string) ValidateParams(string[] _params)
        {
            ParamSyntax = $"/item 100 item name";
            return (1, "true");
        }
        public override int Process(string[] _params)
        {
            _params = _params.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            for (int i=0; i< _params.Length; i++)
            {
                _params[i] = _params[i].Replace("'", "''");
            }
            
            string name = string.Join(" ", _params);
            int ql = 0;
            if (int.TryParse(_params[0], out ql))
                name = string.Join(" ", _params[1..]);

            if (ql > 0)
                _params = _params[1..];

            List<AOItem> items = GetItemsByName(_params, ql);
            if (items.Count == 0)
            {
                OutputStrings[0] = $"No matching items. Inputs should match {ParamSyntax}";
                return 1;
            }
            int page = 0;
            int pageFirstItem = 1;
            int pageItem = 0;
            OutputStrings[page] = $"<a href=\"text://{HighlightColor}Item Search Results{EndColor}<br><br>";
            foreach (AOItem item in items)
            {
                int localQl = ql;
                if (ql < 1)
                    localQl = item.lowql;
                else
                    localQl = ql;

                string cleanName = item.name.Replace("'", "");
                cleanName = cleanName.Replace("\"", "");
                cleanName = cleanName.Replace(" ", "_");

                pageItem++;

                OutputStrings[page] += $"<img src=rdb://{item.icon}><br>";
                OutputStrings[page] += $"{BuildItemRef(item.lowid, item.highid, localQl, item.name)} [{localQl}] [{BuildItemRef(item.lowid, item.highid, item.lowql, item.lowql.ToString())} - {BuildItemRef(item.lowid, item.highid, item.highql, item.highql.ToString())}]<br>";
                OutputStrings[page] += $"Link to chat - QL {localQl} <a href='chatcmd:///PRKHelp/Itemlink {item.lowid} {item.highid} {localQl} {cleanName}'>[->]</a><br><br>";
                
                if (OutputStrings[page].Length > 3500)
                {
                    OutputStrings[page] += $"\">Item Search Results ({pageFirstItem} - {pageItem} of {items.Count})</a>";
                    OutputStrings.Add($"<a href=\"text://{HighlightColor}Item Search Results ({pageFirstItem} - {pageItem} of {items.Count}){EndColor}<br><br>");
                    page++;
                    pageFirstItem = pageItem + 1;
                }
            }
            OutputStrings[page] += $"\">Item Search Results ({pageFirstItem} - {pageItem} of {items.Count})</a>";

            return 1;
        }

        static void LoadItems()
        {
            DB.InsertSQLFile(Path.GetDirectoryName(Application.ExecutablePath)+"\\SQL\\Items.sql");
        }

        static List<AOItem> GetItemsByName(string[] _name, int _ql)
        {
            string likeString = $"LIKE '%{_name[0]}%'";
            for (int i=1; i<_name.Length; i++)
                likeString += $" AND name LIKE '%{_name[i]}%'";

            string query = $"SELECT * FROM Items WHERE name {likeString} ";
            if (_ql > 0)
                query += $"AND lowql <= {_ql} AND highql >= {_ql} ";

            query += "ORDER BY name ASC, highql DESC";

            return DB.QueryItem(query, String.Join(" ", _name));
        }
    }
}
