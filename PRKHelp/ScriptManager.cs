namespace PRKHelp
{
    internal class ScriptManager
    {
        static string ScriptOutputFile;
        static int ExecutionDelay = 1500;

        public static void Init(string _scriptsFolderPath)
        {
            ScriptOutputFile = Path.Combine(_scriptsFolderPath, "PRKHelp");
            // Generate Script file if it doesnt exist
            using (FileStream scriptStream = new(ScriptOutputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)) {}

            GenerateInterfaceScripts(_scriptsFolderPath);
        }

        public static void WriteOutput(List<string> _output)
        {
            // Populate script file with output in paginated form
            for (var i = 0; i < _output.Count; i++)
            {
                string _outputIndexString = ScriptOutputFile.ToString();
                if (i > 0)
                    _outputIndexString += i.ToString();

                using (FileStream scriptStream = new(_outputIndexString, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter scriptWriter = new(scriptStream))
                    {
                        scriptStream.SetLength(0); 
                        scriptWriter.Write($"{_output[i]}");
                        
                        // Create new page references as needed
                        if(_output.Count > 1 && _output.Count > i + 1)
                        {
                            scriptWriter.Write($"\n/PRKHELP{i + 1}");
                        }
                    }
                }                
            }
        }

        private static void GenerateInterfaceScripts(string _scriptsFolderPath)
        {
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "calc"), $"/w !calc %1\n/delay {ExecutionDelay}\n/PRKHelp");
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "oe"), $"/w !oe %1\n/delay {ExecutionDelay}\n/PRKHelp");
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "mafist"), $"/w !mafist %1\n/delay {ExecutionDelay}\n/PRKHelp");
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "timer"), $"/w !timer %1 %2\n/delay {ExecutionDelay}\n/PRKHelp");
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "timers"), $"/w !timers\n/delay {ExecutionDelay}\n/PRKHelp");
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "itemfind"), $"/w !itemfind %1 %2 %3 %4 %5 %6 %7 %8 %9\n/delay {ExecutionDelay}\n/PRKHelp");
        }
    }
}
