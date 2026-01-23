
using PRKHelp.Settings;

namespace PRKHelp
{
    public partial class FormUI : Form
    {
        static string PathPRK = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Funcom\\Anarchy Online");
        static string LogFilePath;
        static string ScriptsFolderPath;

        public FormUI()
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

        // Called from UI button
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

        // Called from UI button
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

        // Called from UI button
        private void Start(object sender, EventArgs e)
        {
            // Clear File contents
            using (FileStream fileStream = new(LogFilePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite)) {}

            ScriptManager.Init(ScriptsFolderPath);
            this.Size = new Size(Width, 280);
            runningText.Visible = true;

            // Start watching logfile
            InputController.Run(LogFilePath);
        }
    }
}
