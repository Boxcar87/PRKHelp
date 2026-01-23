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
            runningText = new Label();
            SuspendLayout();
            // 
            // getPathBtn
            // 
            getPathBtn.FlatStyle = FlatStyle.System;
            getPathBtn.Location = new Point(240, 34);
            getPathBtn.Name = "getPathBtn";
            getPathBtn.Size = new Size(77, 29);
            getPathBtn.TabIndex = 1;
            getPathBtn.Text = "Select File";
            getPathBtn.UseVisualStyleBackColor = false;
            getPathBtn.Click += GetLogFile;
            // 
            // folderPathText
            // 
            folderPathText.AutoSize = true;
            folderPathText.ForeColor = SystemColors.ControlDark;
            folderPathText.Location = new Point(21, 151);
            folderPathText.Name = "folderPathText";
            folderPathText.Size = new Size(54, 15);
            folderPathText.TabIndex = 2;
            folderPathText.Text = "textLabel";
            // 
            // confirmBtn
            // 
            confirmBtn.Location = new Point(114, 318);
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
            richTextBox1.Location = new Point(12, 34);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(222, 32);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "Please select a log to monitor";
            // 
            // filePathText
            // 
            filePathText.AutoSize = true;
            filePathText.Font = new Font("Segoe UI", 6F);
            filePathText.ForeColor = SystemColors.ControlDark;
            filePathText.Location = new Point(21, 69);
            filePathText.Name = "filePathText";
            filePathText.Size = new Size(26, 11);
            filePathText.TabIndex = 5;
            filePathText.Text = "label1";
            // 
            // button1
            // 
            button1.Location = new Point(227, 116);
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
            richTextBox2.Location = new Point(12, 116);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(209, 32);
            richTextBox2.TabIndex = 7;
            richTextBox2.Text = "Please select your scripts folder";
            // 
            // runningText
            // 
            runningText.Anchor = AnchorStyles.Bottom;
            runningText.AutoSize = true;
            runningText.Font = new Font("Segoe UI", 13F);
            runningText.ForeColor = Color.Aqua;
            runningText.Location = new Point(121, 327);
            runningText.Name = "runningText";
            runningText.Size = new Size(86, 25);
            runningText.TabIndex = 8;
            runningText.Text = "Running..";
            runningText.TextAlign = ContentAlignment.TopCenter;
            runningText.Visible = false;
            // 
            // FormUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 81, 115);
            ClientSize = new Size(329, 377);
            Controls.Add(runningText);
            Controls.Add(richTextBox2);
            Controls.Add(button1);
            Controls.Add(filePathText);
            Controls.Add(richTextBox1);
            Controls.Add(confirmBtn);
            Controls.Add(folderPathText);
            Controls.Add(getPathBtn);
            Name = "FormUI";
            Text = "PRKHelp";
            ResumeLayout(false);
            PerformLayout();
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
        private Label runningText;
        //private FileSystemWatcher Watcher;
    }
}
