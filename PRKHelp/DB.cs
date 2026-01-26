using System.Diagnostics;
using Microsoft.Data.Sqlite;
using PRKHelp.Components;

namespace PRKHelp
{
    public class DB
    {
        static SqliteConnectionStringBuilder ConnectionString { get; } = new()
        {
            DataSource = "PRKDB",
            Mode = SqliteOpenMode.Memory,
            Cache = SqliteCacheMode.Shared
        };

        static SqliteConnection Connection = new(ConnectionString.ToString());

        internal DB()
        {
            Connection.Open();
        }

        public static void InsertSQLFile(string _path)
        {
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = File.ReadAllText(_path);

                try
                {
                    command.ExecuteNonQuery();
                    //Debug.WriteLine("Database populated successfully from SQL file.");
                }
                catch (SqliteException ex)
                {
                    //Debug.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        public static List<AOItem> QueryItem(string _query, string _name)
        {
            List<AOItem> items = [];
            try
            {
                using (var command = new SqliteCommand(_query, Connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AOItem item = new()
                            {
                                lowid = reader.GetInt32(reader.GetOrdinal("lowid")),
                                highid = reader.GetInt32(reader.GetOrdinal("highid")),
                                lowql = reader.GetInt32(reader.GetOrdinal("lowql")),
                                highql = reader.GetInt32(reader.GetOrdinal("highql")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                icon = reader.GetInt32(reader.GetOrdinal("icon")),
                            };
                            if (item.name.Equals(_name, StringComparison.OrdinalIgnoreCase))
                                items.Insert(0, item);
                            else
                                items.Add(item);
                            item.name = item.name.Replace("\"", "\\\"");
                        }
                    }
                }
            }
            catch(Microsoft.Data.Sqlite.SqliteException _ex)
            {
                Debug.WriteLine(_ex);
            }
            return items;
        }

        public static Dictionary<string, Dictionary<string, float>> QueryTrickle(string _query)
        {
            Dictionary<string, Dictionary<string, float>> stats = new()
            {
                {"Body & Defense", new Dictionary<string, float>()},
                {"Combat & Healing", new Dictionary<string, float>()},
                {"Melee Weapons", new Dictionary<string, float>()},
                {"Melee Specials", new Dictionary<string, float>()},
                {"Ranged Weapons", new Dictionary<string, float>()},
                {"Ranged Specials", new Dictionary<string, float>()},
                {"Nanos & Casting", new Dictionary<string, float>()},
                {"Trade & Repair", new Dictionary<string, float>()}
            };

            using (var command = new SqliteCommand(_query, Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string group = reader.GetString(reader.GetOrdinal("group_name"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        float value = reader.GetFloat(reader.GetOrdinal("amount"));
                        stats[group].Add(name, value);
                    }
                }
            }
            return stats;
        }

        public static List<(string, AOItem)> QuerySymbiant(string _query)
        {
            List<(string, AOItem)> symbiantsWithBosses = [];

            using (var command = new SqliteCommand(_query, Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AOItem item = new()
                        {
                            lowid = reader.GetInt32(reader.GetOrdinal("lowid")),
                            highid = reader.GetInt32(reader.GetOrdinal("highid")),
                            lowql = reader.GetInt32(reader.GetOrdinal("lowql")),
                            name = reader.GetString(reader.GetOrdinal("name"))
                        };
                        item.name = item.name.Replace("\"", "\\\"");
                        string pocketBoss = reader.GetString(reader.GetOrdinal("pocketboss_name"));
                        symbiantsWithBosses.Add((pocketBoss, item));
                    }
                }
            }
            return symbiantsWithBosses;
        }

        public static List<PocketBoss> QueryPocketBoss(string _query)
        {
            List<PocketBoss> pocketBosses = [];
            try
            {
                using (var command = new SqliteCommand(_query, Connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PocketBoss boss = new()
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                playfield = reader.GetString(reader.GetOrdinal("long_name")),
                                mobType = reader.GetString(reader.GetOrdinal("mob_type")),
                                level = reader.GetInt32(reader.GetOrdinal("level")),
                                location = reader.GetString(reader.GetOrdinal("location"))
                            };
                            pocketBosses.Add(boss);
                        }
                    }
                }
            }
            catch (Microsoft.Data.Sqlite.SqliteException _ex)
            {
                Debug.WriteLine(_ex);
            }
            return pocketBosses;
        }

        public static List<AOItem> QuerySymbiantsByPocketBoss(string _query)
        {
            List<AOItem> symbiants = [];
            try
            {
                using (var command = new SqliteCommand(_query, Connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AOItem item = new()
                            {
                                lowid = reader.GetInt32(reader.GetOrdinal("lowid")),
                                highid = reader.GetInt32(reader.GetOrdinal("highid")),
                                lowql = reader.GetInt32(reader.GetOrdinal("lowql")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                            };
                            item.name = item.name.Replace("\"", "\\\"");
                            symbiants.Add(item);
                        }
                    }
                }
            }
            catch (Microsoft.Data.Sqlite.SqliteException _ex)
            {
                Debug.WriteLine(_ex);
            }
            return symbiants;
        }
    }
}
