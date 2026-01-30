using Microsoft.Data.Sqlite;

namespace PRKHelp
{
    partial class FormUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            getPathBtn = new Button();
            folderPathText = new Label();
            confirmBtn = new Button();
            richTextBox1 = new RichTextBox();
            filePathText = new Label();
            button1 = new Button();
            richTextBox2 = new RichTextBox();
            richTextBox3 = new RichTextBox();
            combatPathText = new Label();
            button2 = new Button();
            characterSelect = new ComboBox();
            button3 = new Button();
            button4 = new Button();
            settingsBox = new Panel();
            activeWindow = new Panel();
            richTextBox6 = new RichTextBox();
            petNameInput = new TextBox();
            exitButton = new Button();
            selectCharacterButton = new Button();
            xpReset = new Button();
            xpToggle = new Button();
            damageReset = new Button();
            damageToggle = new Button();
            xpAmount = new RichTextBox();
            damageAmount = new RichTextBox();
            richTextBox5 = new RichTextBox();
            richTextBox4 = new RichTextBox();
            activeCharacter = new RichTextBox();
            settingsBox.SuspendLayout();
            activeWindow.SuspendLayout();
            SuspendLayout();
            // 
            // getPathBtn
            // 
            getPathBtn.FlatStyle = FlatStyle.System;
            getPathBtn.Location = new Point(226, 130);
            getPathBtn.Name = "getPathBtn";
            getPathBtn.Size = new Size(90, 29);
            getPathBtn.TabIndex = 1;
            getPathBtn.Text = "Select File";
            getPathBtn.UseVisualStyleBackColor = false;
            getPathBtn.Click += GetLogFile;
            // 
            // folderPathText
            // 
            folderPathText.AutoSize = true;
            folderPathText.Font = new Font("Segoe UI", 6F);
            folderPathText.ForeColor = SystemColors.ControlDark;
            folderPathText.Location = new Point(15, 94);
            folderPathText.Name = "folderPathText";
            folderPathText.Size = new Size(38, 11);
            folderPathText.TabIndex = 2;
            folderPathText.Text = "textLabel";
            // 
            // confirmBtn
            // 
            confirmBtn.Location = new Point(113, 280);
            confirmBtn.Name = "confirmBtn";
            confirmBtn.Size = new Size(107, 34);
            confirmBtn.TabIndex = 3;
            confirmBtn.Text = "Start";
            confirmBtn.UseVisualStyleBackColor = true;
            confirmBtn.Click += Start;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.FromArgb(95, 81, 115);
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Font = new Font("Segoe UI", 11F);
            richTextBox1.ForeColor = SystemColors.Info;
            richTextBox1.Location = new Point(11, 130);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(209, 26);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "Select vicinity chat log file";
            // 
            // filePathText
            // 
            filePathText.AutoSize = true;
            filePathText.Font = new Font("Segoe UI", 6F);
            filePathText.ForeColor = SystemColors.ControlDark;
            filePathText.Location = new Point(15, 159);
            filePathText.Name = "filePathText";
            filePathText.Size = new Size(26, 11);
            filePathText.TabIndex = 5;
            filePathText.Text = "label1";
            // 
            // button1
            // 
            button1.Location = new Point(226, 69);
            button1.Name = "button1";
            button1.Size = new Size(90, 29);
            button1.TabIndex = 6;
            button1.Text = "Select Folder";
            button1.UseVisualStyleBackColor = true;
            button1.Click += GetScriptsPath;
            // 
            // richTextBox2
            // 
            richTextBox2.BackColor = Color.FromArgb(95, 81, 115);
            richTextBox2.BorderStyle = BorderStyle.None;
            richTextBox2.Font = new Font("Segoe UI", 11F);
            richTextBox2.ForeColor = SystemColors.Info;
            richTextBox2.Location = new Point(11, 69);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(209, 22);
            richTextBox2.TabIndex = 7;
            richTextBox2.Text = "Select your scripts folder";
            // 
            // richTextBox3
            // 
            richTextBox3.BackColor = Color.FromArgb(95, 81, 115);
            richTextBox3.BorderStyle = BorderStyle.None;
            richTextBox3.Font = new Font("Segoe UI", 11F);
            richTextBox3.ForeColor = SystemColors.Info;
            richTextBox3.Location = new Point(11, 181);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.ReadOnly = true;
            richTextBox3.Size = new Size(195, 26);
            richTextBox3.TabIndex = 9;
            richTextBox3.Text = "Select combat log file";
            // 
            // combatPathText
            // 
            combatPathText.AutoSize = true;
            combatPathText.Font = new Font("Segoe UI", 6F);
            combatPathText.ForeColor = SystemColors.ControlDark;
            combatPathText.Location = new Point(15, 210);
            combatPathText.Name = "combatPathText";
            combatPathText.Size = new Size(49, 11);
            combatPathText.TabIndex = 10;
            combatPathText.Text = "combatPath";
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.System;
            button2.Location = new Point(226, 181);
            button2.Name = "button2";
            button2.Size = new Size(90, 29);
            button2.TabIndex = 11;
            button2.Text = "Select File";
            button2.UseVisualStyleBackColor = false;
            button2.Click += GetCombatLogFile;
            // 
            // characterSelect
            // 
            characterSelect.FormattingEnabled = true;
            characterSelect.Location = new Point(11, 23);
            characterSelect.Name = "characterSelect";
            characterSelect.Size = new Size(181, 23);
            characterSelect.TabIndex = 12;
            characterSelect.Text = "Character";
            characterSelect.SelectedIndexChanged += CharacterSelected;
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.System;
            button3.Location = new Point(198, 23);
            button3.Name = "button3";
            button3.Size = new Size(62, 25);
            button3.TabIndex = 13;
            button3.Text = "Add Char";
            button3.UseVisualStyleBackColor = false;
            button3.Click += AddNewCharacter;
            // 
            // button4
            // 
            button4.FlatStyle = FlatStyle.System;
            button4.Location = new Point(266, 23);
            button4.Name = "button4";
            button4.Size = new Size(50, 25);
            button4.TabIndex = 14;
            button4.Text = "Remove";
            button4.UseVisualStyleBackColor = false;
            button4.Click += RemoveCharacter;
            // 
            // settingsBox
            // 
            settingsBox.Controls.Add(confirmBtn);
            settingsBox.Controls.Add(characterSelect);
            settingsBox.Controls.Add(button2);
            settingsBox.Controls.Add(filePathText);
            settingsBox.Controls.Add(button4);
            settingsBox.Controls.Add(folderPathText);
            settingsBox.Controls.Add(combatPathText);
            settingsBox.Controls.Add(button1);
            settingsBox.Controls.Add(button3);
            settingsBox.Controls.Add(getPathBtn);
            settingsBox.Controls.Add(richTextBox3);
            settingsBox.Controls.Add(richTextBox2);
            settingsBox.Controls.Add(richTextBox1);
            settingsBox.Location = new Point(1, 2);
            settingsBox.Name = "settingsBox";
            settingsBox.Size = new Size(331, 336);
            settingsBox.TabIndex = 15;
            // 
            // activeWindow
            // 
            activeWindow.Controls.Add(richTextBox6);
            activeWindow.Controls.Add(petNameInput);
            activeWindow.Controls.Add(exitButton);
            activeWindow.Controls.Add(selectCharacterButton);
            activeWindow.Controls.Add(xpReset);
            activeWindow.Controls.Add(xpToggle);
            activeWindow.Controls.Add(damageReset);
            activeWindow.Controls.Add(damageToggle);
            activeWindow.Controls.Add(xpAmount);
            activeWindow.Controls.Add(damageAmount);
            activeWindow.Controls.Add(richTextBox5);
            activeWindow.Controls.Add(richTextBox4);
            activeWindow.Controls.Add(activeCharacter);
            activeWindow.Dock = DockStyle.Fill;
            activeWindow.Location = new Point(0, 0);
            activeWindow.Name = "activeWindow";
            activeWindow.Size = new Size(329, 341);
            activeWindow.TabIndex = 15;
            // 
            // richTextBox6
            // 
            richTextBox6.BackColor = Color.FromArgb(95, 81, 115);
            richTextBox6.BorderStyle = BorderStyle.None;
            richTextBox6.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox6.ForeColor = Color.SlateGray;
            richTextBox6.Location = new Point(15, 69);
            richTextBox6.Name = "richTextBox6";
            richTextBox6.ReadOnly = true;
            richTextBox6.Size = new Size(59, 20);
            richTextBox6.TabIndex = 20;
            richTextBox6.Text = "Pet name";
            // 
            // petNameInput
            // 
            petNameInput.BackColor = Color.FromArgb(95, 81, 115);
            petNameInput.BorderStyle = BorderStyle.FixedSingle;
            petNameInput.ForeColor = Color.Gainsboro;
            petNameInput.Location = new Point(80, 66);
            petNameInput.Name = "petNameInput";
            petNameInput.Size = new Size(94, 23);
            petNameInput.TabIndex = 19;
            // 
            // exitButton
            // 
            exitButton.Dock = DockStyle.Bottom;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.ForeColor = Color.Salmon;
            exitButton.Location = new Point(0, 318);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(329, 23);
            exitButton.TabIndex = 18;
            exitButton.Text = "Exit to Settings";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += OpenSettingsWindow;
            // 
            // selectCharacterButton
            // 
            selectCharacterButton.FlatStyle = FlatStyle.Flat;
            selectCharacterButton.ForeColor = Color.LightCyan;
            selectCharacterButton.Location = new Point(185, 37);
            selectCharacterButton.Name = "selectCharacterButton";
            selectCharacterButton.Size = new Size(131, 23);
            selectCharacterButton.TabIndex = 17;
            selectCharacterButton.Text = "Select New Char";
            selectCharacterButton.UseVisualStyleBackColor = true;
            selectCharacterButton.Click += OpenSettingsWindow;
            // 
            // xpReset
            // 
            xpReset.FlatStyle = FlatStyle.Flat;
            xpReset.ForeColor = Color.LightCyan;
            xpReset.Location = new Point(261, 148);
            xpReset.Name = "xpReset";
            xpReset.Size = new Size(55, 23);
            xpReset.TabIndex = 16;
            xpReset.Text = "Reset";
            xpReset.UseVisualStyleBackColor = true;
            xpReset.Click += ResetXPTracking;
            // 
            // xpToggle
            // 
            xpToggle.FlatStyle = FlatStyle.Flat;
            xpToggle.ForeColor = Color.MediumSpringGreen;
            xpToggle.Location = new Point(185, 148);
            xpToggle.Name = "xpToggle";
            xpToggle.Size = new Size(55, 23);
            xpToggle.TabIndex = 15;
            xpToggle.Text = "Start";
            xpToggle.UseVisualStyleBackColor = true;
            xpToggle.Click += ToggleXPTracking;
            // 
            // damageReset
            // 
            damageReset.FlatStyle = FlatStyle.Flat;
            damageReset.ForeColor = Color.LightCyan;
            damageReset.Location = new Point(261, 105);
            damageReset.Name = "damageReset";
            damageReset.Size = new Size(55, 23);
            damageReset.TabIndex = 14;
            damageReset.Text = "Reset";
            damageReset.UseVisualStyleBackColor = true;
            damageReset.Click += ResetDamageTracking;
            // 
            // damageToggle
            // 
            damageToggle.FlatStyle = FlatStyle.Flat;
            damageToggle.ForeColor = Color.MediumSpringGreen;
            damageToggle.Location = new Point(185, 105);
            damageToggle.Name = "damageToggle";
            damageToggle.Size = new Size(55, 23);
            damageToggle.TabIndex = 13;
            damageToggle.Text = "Start";
            damageToggle.UseVisualStyleBackColor = true;
            damageToggle.Click += ToggleDamageTracking;
            // 
            // xpAmount
            // 
            xpAmount.BackColor = Color.FromArgb(95, 81, 115);
            xpAmount.BorderStyle = BorderStyle.None;
            xpAmount.Font = new Font("Verdana", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xpAmount.ForeColor = Color.Yellow;
            xpAmount.Location = new Point(4, 148);
            xpAmount.Name = "xpAmount";
            xpAmount.ReadOnly = true;
            xpAmount.RightToLeft = RightToLeft.Yes;
            xpAmount.ScrollBars = RichTextBoxScrollBars.None;
            xpAmount.Size = new Size(90, 29);
            xpAmount.TabIndex = 12;
            xpAmount.Text = "888.68M";
            xpAmount.TextChanged += xpAmount_TextChanged;
            // 
            // damageAmount
            // 
            damageAmount.BackColor = Color.FromArgb(95, 81, 115);
            damageAmount.BorderStyle = BorderStyle.None;
            damageAmount.Font = new Font("Verdana", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            damageAmount.ForeColor = Color.Cyan;
            damageAmount.Location = new Point(-3, 105);
            damageAmount.Name = "damageAmount";
            damageAmount.ReadOnly = true;
            damageAmount.RightToLeft = RightToLeft.Yes;
            damageAmount.ScrollBars = RichTextBoxScrollBars.None;
            damageAmount.Size = new Size(97, 29);
            damageAmount.TabIndex = 11;
            damageAmount.Text = "444,444";
            // 
            // richTextBox5
            // 
            richTextBox5.BackColor = Color.FromArgb(95, 81, 115);
            richTextBox5.BorderStyle = BorderStyle.None;
            richTextBox5.Font = new Font("Verdana", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox5.ForeColor = Color.Yellow;
            richTextBox5.Location = new Point(113, 152);
            richTextBox5.Name = "richTextBox5";
            richTextBox5.ReadOnly = true;
            richTextBox5.Size = new Size(61, 19);
            richTextBox5.TabIndex = 10;
            richTextBox5.Text = "XP/h";
            // 
            // richTextBox4
            // 
            richTextBox4.BackColor = Color.FromArgb(95, 81, 115);
            richTextBox4.BorderStyle = BorderStyle.None;
            richTextBox4.Font = new Font("Verdana", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox4.ForeColor = Color.Cyan;
            richTextBox4.Location = new Point(113, 109);
            richTextBox4.Name = "richTextBox4";
            richTextBox4.ReadOnly = true;
            richTextBox4.Size = new Size(61, 20);
            richTextBox4.TabIndex = 9;
            richTextBox4.Text = "Dmg/m";
            // 
            // activeCharacter
            // 
            activeCharacter.BackColor = Color.FromArgb(95, 81, 115);
            activeCharacter.BorderStyle = BorderStyle.None;
            activeCharacter.Font = new Font("Verdana", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            activeCharacter.ForeColor = Color.CornflowerBlue;
            activeCharacter.Location = new Point(15, 37);
            activeCharacter.Name = "activeCharacter";
            activeCharacter.ReadOnly = true;
            activeCharacter.Size = new Size(209, 26);
            activeCharacter.TabIndex = 8;
            activeCharacter.Text = "CharacterName";
            // 
            // FormUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 81, 115);
            ClientSize = new Size(329, 341);
            Controls.Add(activeWindow);
            Controls.Add(settingsBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FormUI";
            Text = "PRKHelp";
            settingsBox.ResumeLayout(false);
            settingsBox.PerformLayout();
            activeWindow.ResumeLayout(false);
            activeWindow.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox pathInput;
        private Button getPathBtn;
        private Label folderPathText;
        private Button confirmBtn;
        private RichTextBox richTextBox1;
        private Label filePathText;
        private Button button1;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox3;
        private Label combatPathText;
        private Button button2;
        private ComboBox characterSelect;
        private Button button3;
        private Button button4;
        private Panel settingsBox;
        private Panel activeWindow;
        private RichTextBox activeCharacter;
        private RichTextBox xpAmount;
        private RichTextBox damageAmount;
        private RichTextBox richTextBox5;
        private RichTextBox richTextBox4;
        private Button damageReset;
        private Button damageToggle;
        private Button selectCharacterButton;
        private Button xpReset;
        private Button xpToggle;
        private Button exitButton;
        private RichTextBox richTextBox6;
        private TextBox petNameInput;
        //private FileSystemWatcher Watcher;
    }
}
