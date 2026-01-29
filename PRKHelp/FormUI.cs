
using System.Diagnostics;
using PRKHelp.Settings;
using PRKHelper.Helpbot;
using PRKHelper.Parser;

namespace PRKHelp
{
    public partial class FormUI : Form
    {
        static string PathPRK = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Funcom\\Anarchy Online");
        static List<string> Characters;
        static string LogFilePath;
        static string LogCombatFilePath;
        static string ScriptsFolderPath;
        string SelectedCharacter;
        Color DamageParseColor = Color.Cyan;
        CombatParser CombatParser;

        public FormUI()
        {
            InitializeComponent();
            activeWindow.Visible = false;
            SelectedCharacter = SettingsManager.GetLastSelectedCharacter();
            characterSelect.Text = SelectedCharacter;
            if (SelectedCharacter == String.Empty)
                characterSelect.Text = "Select Character";

            ScriptsFolderPath = SettingsManager.GetScriptsPath();
            if (ScriptsFolderPath.Length > 0)
            {
                string truncatedFolder = ScriptsFolderPath[PathPRK.Length..];
                folderPathText.Text = "..AO" + truncatedFolder;
            }
            else
                folderPathText.Text = "";
            UpdateSelectedCharacter(SelectedCharacter);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            Characters = SettingsManager.GetAllCharacters();
            foreach (string character in Characters)
            {
                characterSelect.Items.Add(character);
            }
            CombatParser = new CombatParser(damageAmount, xpAmount);
        }

        private void AddNewCharacter(object sender, EventArgs e)
        {
            Debug.WriteLine(characterSelect.Text);
            if (!Characters.Contains(characterSelect.Text))
            {
                Characters.Add(characterSelect.Text);
                characterSelect.Items.Add(characterSelect.Text);
            }
            UpdateSelectedCharacter(characterSelect.Text);
        }

        private void RemoveCharacter(object sender, EventArgs e)
        {
            SettingsManager.RemoveCharacter(characterSelect.Text);
            characterSelect.Items.Remove(characterSelect.Text);
            characterSelect.Text = "Select Character";
        }

        private void CharacterSelected(object sender, EventArgs e)
        {
            UpdateSelectedCharacter(characterSelect.Text);
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
                    SettingsManager.UpdateFilePath(LogFilePath, "LogFilePath", SelectedCharacter);
                    string truncated = LogFilePath[PathPRK.Length..];
                    filePathText.Text = "..AO" + truncated;
                }
            }
        }

        private void GetCombatLogFile(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = PathPRK;
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LogCombatFilePath = openFileDialog.FileName;
                    SettingsManager.UpdateFilePath(LogCombatFilePath, "LogCombatFilePath", SelectedCharacter);
                    string truncated = LogCombatFilePath[PathPRK.Length..];
                    combatPathText.Text = "..AO" + truncated;
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
                    SettingsManager.UpdateScriptsPath(ScriptsFolderPath);
                    string truncated = ScriptsFolderPath[PathPRK.Length..];
                    folderPathText.Text = "..AO" + truncated;
                }
            }
        }

        private void UpdateSelectedCharacter(string _character)
        {
            SelectedCharacter = _character;
            LogFilePath = SettingsManager.GetFilePath("LogFilePath", _character);
            LogCombatFilePath = SettingsManager.GetFilePath("LogCombatFilePath", _character);
            if (LogFilePath.Length > 0)
            {
                string truncatedFile = LogFilePath[PathPRK.Length..];
                filePathText.Text = "..AO" + truncatedFile;
            }
            else
                filePathText.Text = "";
            if (LogCombatFilePath.Length > 0)
            {
                string truncatedFile = LogCombatFilePath[PathPRK.Length..];
                combatPathText.Text = "..AO" + truncatedFile;
            }
            else
                combatPathText.Text = "";
        }

        // Called from UI button
        private void Start(object sender, EventArgs e)
        {
            if (SelectedCharacter == String.Empty)
                return;
            activeCharacter.Text = SelectedCharacter;
            // Clear File contents
            using (FileStream fileStream = new(LogFilePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite)) { }

            SettingsManager.UpdateLastSelectedCharacter(SelectedCharacter);
            ScriptManager.Init(ScriptsFolderPath);
            this.Size = new Size(Width, 250);
            settingsBox.Visible = false;
            activeWindow.Visible = true;
            TopMost = true;
            TransparencyKey = BackColor;
            FormBorderStyle = FormBorderStyle.None;
            CombatParser.UpdatePath(LogCombatFilePath);
            using (FileStream fileStream = new(LogCombatFilePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite)) { }
            CombatParser.Run();
            SetTrackingUI();

            // Start watching logfile
            InputController.Run(LogFilePath);
        }

        private void OpenSettingsWindow(object sender, EventArgs e)
        {
            settingsBox.Visible = true;
            activeWindow.Visible = false;
            TopMost = false;
            TransparencyKey = Color.Empty;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Size = new Size(Width, 380);
            //Tell Tracker to stop watching
        }

        private void ToggleXPTracking(object sender, EventArgs e)
        {
            if (xpToggle.Text.Equals("Start")) // Tracker is currently running
            {
                xpToggle.Text = "Stop";
                xpToggle.ForeColor = Color.Tomato;
                CombatParser.TrackXP();
            }
            else
            {
                xpToggle.Text = "Start";
                xpToggle.ForeColor = Color.MediumSpringGreen;
                CombatParser.StopXP();
            }
        }

        private void ToggleDamageTracking(object sender, EventArgs e)
        {
            if (damageToggle.Text.Equals("Start")) // Tracker is currently running
            {
                damageToggle.Text = "Stop";
                damageToggle.ForeColor = Color.Tomato;
                CombatParser.TrackDamage(petNameInput.Text);
            }
            else
            {
                damageToggle.Text = "Start";
                damageToggle.ForeColor = Color.MediumSpringGreen;
                CombatParser.StopDamage();
            }
        }

        private void ResetXPTracking(object sender, EventArgs e)
        {
            xpToggle.Text = "Start";
            xpToggle.ForeColor = Color.MediumSpringGreen;
            CombatParser.ResetXP();
        }

        private void ResetDamageTracking(object sender, EventArgs e)
        {
            damageToggle.Text = "Start";
            damageToggle.ForeColor = Color.MediumSpringGreen;
            CombatParser.ResetDamage(petNameInput.Text);
        }

        private void SetTrackingUI()
        {
            damageAmount.Text = "0";
            xpAmount.Text = "0";
            xpToggle.Text = "Start";
            xpToggle.ForeColor = Color.MediumSpringGreen;
            damageToggle.Text = "Start";
            damageToggle.ForeColor = Color.MediumSpringGreen;
        }

        private void xpAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
