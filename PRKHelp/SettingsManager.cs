
using System;
using System.IO;
using System.Text.Json;

namespace PRKHelp
{
    public partial class SettingsManager
        {
        readonly static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        readonly static string Settings = Path.Combine(AppDataPath, "PRKHelp", "Settings");
        readonly static string SettingsFile = "PRKHelpPaths.json";
        public static string GetPath(string _pathType)
        {

            string path;
            Directory.CreateDirectory(Settings);

            if (!File.Exists(Path.Combine(Settings, SettingsFile)))
            {
                string newFileString = JsonSerializer.Serialize(new PathSettings(), new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(Path.Combine(Settings, SettingsFile), newFileString);
                return "";
            }

            string jsonString = File.ReadAllText(Path.Combine(Settings, SettingsFile));
            try
            {
                PathSettings paths = JsonSerializer.Deserialize<PathSettings>(jsonString);
                path = (string)paths.GetType().GetProperty(_pathType).GetValue(paths);
            }
            catch (JsonException _ex)
            {

                // Currently returning an object where all values are null in the event it cannot parse the file
                // In this situation the UIWindowController will reset/repopulate the default values as the windows are opened for the first time
                path = "";
            }

            return path;
        }

        public static void UpdatePath(string _path, string _pathType)
        {
            PathSettings settings = GetAllPaths();
            settings.GetType().GetProperty(_pathType).SetValue(settings, _path, null);
            string jsonString = "";
            try
            {
                jsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(Path.Combine(Settings, SettingsFile), jsonString);
            }
            catch (JsonException _ex)
            {
            }
        }

        static PathSettings GetAllPaths()
        {
            if (!File.Exists(Path.Combine(Settings, SettingsFile)))
            {
                return new PathSettings();
            }
            string jsonString = File.ReadAllText(Path.Combine(Settings, SettingsFile));
            return JsonSerializer.Deserialize<PathSettings>(jsonString);
        }
    }
}

