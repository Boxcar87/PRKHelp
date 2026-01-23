namespace PRKHelp
{
    public class InputController
    {
        async public static Task Run(string _logFilePath)
        {
            bool hasContent = false;
            using (FileStream fileStream = new(_logFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                if (fileStream.Length > 0)
                {
                    hasContent = true;
                    using (StreamReader reader = new(fileStream))
                    {
                        string rawText = reader.ReadToEnd();

                        if (rawText.Length < 1)
                            return;

                        string[] filtering = rawText.Split(']');
                        string command = filtering[1];

                        if (command[0].ToString().Equals("!"))
                        {
                            command = command.Trim('\n');
                            command = command.Trim(' ');
                            Router.Handle(command[1..]);
                        }
                    }
                }
            }

            // Clear log file contents
            if (hasContent)
                using (FileStream fileStream = new(_logFilePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite)) { }

            await Task.Delay(250);
            Run(_logFilePath);
        }
    }
}
