namespace PRKHelp
{
    internal class ScriptManager
    {
        static string ScriptOutputFile;
        static int ExecutionDelay = 1500;

        public static void Init(string _scriptsFolderPath)
        {
            // This exists to automatically clean up old versions of script generation pathing
            string PRKPath = Path.Combine(_scriptsFolderPath, "PRKHelp");
            if (File.Exists(PRKPath)) // This checks if a file exists at the path
            {
                FileAttributes attributes = File.GetAttributes(PRKPath);
                if (!attributes.HasFlag(FileAttributes.Directory))
                    File.Delete(Path.Combine(_scriptsFolderPath, "PRKHelp"));
            }

            Directory.CreateDirectory(Path.Combine(_scriptsFolderPath, "PRKHelp"));
            ScriptOutputFile = Path.Combine(_scriptsFolderPath, "PRKHelp/Output");
            // Generate Script file if it doesnt exist
            using (FileStream scriptStream = new(ScriptOutputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)) { }
            // Generate Chat link file
            using (FileStream itemLinkStream = new(Path.Combine(_scriptsFolderPath, "PRKHelp/Itemlink"), FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using(StreamWriter linkWriter = new(itemLinkStream))
                {
                    linkWriter.Write($"<a href=\"itemref://%1/%2/%3\">%4</a>");
                }
            }

            GenerateInterfaceScripts(_scriptsFolderPath);
        }

        // Supports paginated output.
        // Each element in _output should be a new page.
        public static void WriteOutput(List<string> _output)
        {
            for (var i = 0; i < _output.Count; i++)
            {
                string _outputIndexString = ScriptOutputFile.ToString();
                if (i > 0)
                    _outputIndexString += i.ToString();

                using (FileStream scriptStream = new(_outputIndexString, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter scriptWriter = new(scriptStream))
                    {
                        scriptWriter.Write($"/text {_output[i]}");
                        
                        // Create new page references as needed
                        if(_output.Count > 1 && _output.Count > i + 1)
                        {
                            scriptWriter.Write($"\n/PRKHelp/Output{i + 1}");
                        }
                    }
                }                
            }
        }

        // Add new script here so player can call the function
        // Append appropriate amount of parameter inputs via add %1 %2 etc
        private static void GenerateInterfaceScripts(string _scriptsFolderPath)
        {
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "calc"), $"/w !calc %1\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // Input is number
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "oe"), $"/w !oe %1\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // Input is number
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "mafist"), $"/w !mafist %1\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // Input is number
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "timer"), $"/w !timer %1 %2\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // First input is string second is number
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "timers"), $"/w !timers\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // No inputs
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "itemfind"), $"/w !itemfind %1 %2 %3 %4 %5 %6 %7 %8 %9\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // Allows 9 inputs, each input is a word of the item name, first input can be ql
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "trickle"), $"/w !trickle %1 %2 %3 %4 %5 %6 %7 %8 %9\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // Allows 8 inputs, 9th input is used for error handling
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "symbiant"), $"/w !symbiant %1 %2 %3 %4 %5 %6 %7 %8 %9\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // Allows 9 inputs,  each input is a word of the item name
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "pb"), $"/w !pocketboss %1 %2 %3 %4 %5 %6 %7 %8 %9\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // Allows 9 inputs, 5th input could be start of inserted pattern name
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "pocketboss"), $"/w !pocketboss %1 %2 %3 %4 %5 %6 %7 %8 %9\n/delay {ExecutionDelay}\n/PRKHelp/Output"); // Allows 9 inputs, 5th input could be start of inserted pattern name
        }
    }
}
