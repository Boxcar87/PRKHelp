namespace PRKHelp
{
    public partial class Form1 : Form
    {
        static string PathPRK = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Funcom\\Anarchy Online");
        static string LogFilePath;
        static string ScriptsFolderPath;
        static long FileSize;
        GameTimers Timers;
        public Form1()
        {
            InitializeComponent();
            LogFilePath = SettingsManager.GetPath("LogFilePath");
            ScriptsFolderPath = SettingsManager.GetPath("ScriptsFolderPath");
            if (LogFilePath.Length > 0)
            {
                string truncatedFile = LogFilePath[PathPRK.Length..];
                filePathText.Text = "..AO" + truncatedFile;
            }
            else
                filePathText.Text = "";
            if (ScriptsFolderPath.Length > 0)
            {
                string truncatedFolder = ScriptsFolderPath[PathPRK.Length..];
                folderPathText.Text = "..AO" + truncatedFolder;
            }
            else
                folderPathText.Text = "";
        }
        private void GetLogFile(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = PathPRK;
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LogFilePath = openFileDialog.FileName;
                    SettingsManager.UpdatePath(LogFilePath, "LogFilePath");
                    string truncated = LogFilePath[PathPRK.Length..];
                    filePathText.Text = "..AO"+truncated;
                }
            }
        }

        private void GetScriptsPath(object sender, EventArgs e)
        {

            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a folder to save files to:";
                folderDialog.SelectedPath = PathPRK;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    ScriptsFolderPath = folderDialog.SelectedPath;
                    SettingsManager.UpdatePath(ScriptsFolderPath, "ScriptsFolderPath");
                    string truncated = ScriptsFolderPath[PathPRK.Length..];
                    folderPathText.Text = "..AO" + truncated;
                }
            }
        }

        private void StartWatching(object sender, EventArgs e)
        {
            Timers = new(LogFilePath);
            //Watcher.Path = LogFilePath[..^8];
            //Watcher.Changed += (sender, e) => ScriptManager.RouteCommand(LogFilePath);

            FileInfo fileInfo = new FileInfo(LogFilePath);
            FileSize = fileInfo.Length;

            using (var fileStream = new FileStream(LogFilePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
            }
            ScriptManager.Run(LogFilePath, ScriptsFolderPath, Timers);
            this.Size = new Size(Width, 280);
            runningText.Visible = true;
            Run();
        }

        async static Task Run()
        {
            FileInfo fileInfo = new FileInfo(LogFilePath);
            if (fileInfo.Length != FileSize)
            {
                ScriptManager.RouteCommand(LogFilePath);
                FileSize = fileInfo.Length;
            }
            await Task.Delay(500);
            Run();
        }
    }
}
