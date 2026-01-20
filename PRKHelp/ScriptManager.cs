using System;
using System.IO;
using System.Reflection.Metadata;

namespace PRKHelp
{
    internal class ScriptManager
    {
        static string ScriptOutputFile;
        static GameTimers Timers;

        public static void Run(string _logFilePath, string _scriptsFolderPath, GameTimers _timers)
        {
            ScriptOutputFile = Path.Combine(_scriptsFolderPath, "PRKHelp");
            Timers = _timers;
            GenerateInterfaceScripts(_scriptsFolderPath);
        }

        public static void RouteCommand(string _filePath)
        {
            using (FileStream readStream = new(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new(readStream))
                {
                    string rawText = reader.ReadToEnd();
                    if (rawText.Length < 1)
                        return;

                    rawText = rawText.Trim('\n');
                    rawText = rawText.Trim(' ');
                    string[] filtering = rawText.Split(']');
                    string command = filtering[1];
                    string[] parameters = command.Split(' ');
                    string response = "";
                    if (!command[0].ToString().Equals("!"))
                    {
                        return;
                    }
                    else
                    {
                        switch (parameters[0].ToLower())
                        {
                            case "!calc":
                                if (parameters.Length < 2)
                                    response = "/text Invalid format. Please use /calc 1+1";
                                else
                                    response = Calc.GetResult(parameters[1]);
                                break;
                            case "!oe":
                                if (parameters.Length < 2)
                                    response = "/text Invalid format. Please use /oe 100";
                                else
                                    response = OE.GetResult(parameters[1]);
                                break;
                            case "!mafist":
                                if (parameters.Length < 2)
                                    response = "/text Invalid format. Please use /mafist 300 1h10m30s";
                                else
                                    response = MA.GetResult(parameters[1]);
                                break;
                            case "!timer":
                                if (parameters.Length < 3)
                                    response = "/text Invalid format. Please use /timer Name 1h10m30s";
                                else
                                    response = Timers.CreateTimer(parameters[1], parameters[2]);
                                break;
                            case "!timers":
                                response = Timers.GetTimers();
                                break;
                            //case "!timerend":
                            //    response = Timers.GetTimers();
                            //    break;
                            default:
                                response = "/text Invalid command";
                                return;
                        }
                        //File.WriteAllText(ScriptOutputFile, response);
                        using (FileStream scriptStream = new(ScriptOutputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            using (StreamWriter scriptWriter = new(scriptStream))
                            {
                                scriptStream.SetLength(0);
                                scriptWriter.Write(response);
                            }
                        }
                    }
                }
            }
            using (FileStream writeStream = new(_filePath, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite)) 
            { 
            }
        }

        private static void GenerateInterfaceScripts(string _scriptsFolderPath)
        {
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "calc"), "/w !calc %1\n/delay 1500\n/PRKHelp");
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "oe"), "/w !oe %1\n/delay 1500\n/PRKHelp");
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "mafist"), "/w !mafist %1\n/delay 1500\n/PRKHelp");
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "timer"), "/w !timer %1 %2\n/delay 1500\n/PRKHelp");
            File.WriteAllText(Path.Combine(_scriptsFolderPath, "timers"), "/w !timers\n/delay 1500\n/PRKHelp");
        }
    }
}
