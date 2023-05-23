namespace VSSaveManager
{
    partial class SavesFolderSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.infolbl = new System.Windows.Forms.Label();
            this.savesFolderList = new System.Windows.Forms.ComboBox();
            this.actionBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.directoryBox = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // infolbl
            // 
            this.infolbl.AutoSize = true;
            this.infolbl.Location = new System.Drawing.Point(112, 91);
            this.infolbl.Name = "infolbl";
            this.infolbl.Size = new System.Drawing.Size(152, 13);
            this.infolbl.TabIndex = 0;
            this.infolbl.Text = "Select the save folder to open.";
            // 
            // savesFolderList
            // 
            this.savesFolderList.FormattingEnabled = true;
            this.savesFolderList.Location = new System.Drawing.Point(115, 107);
            this.savesFolderList.Name = "savesFolderList";
            this.savesFolderList.Size = new System.Drawing.Size(149, 21);
            this.savesFolderList.TabIndex = 1;
            // 
            // actionBtn
            // 
            this.actionBtn.Location = new System.Drawing.Point(148, 134);
            this.actionBtn.Name = "actionBtn";
            this.actionBtn.Size = new System.Drawing.Size(75, 23);
            this.actionBtn.TabIndex = 2;
            this.actionBtn.Text = "Open";
            this.actionBtn.UseVisualStyleBackColor = true;
            this.actionBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Steam Folder Directory:\r\nChange if program can\'t find Steam root folder.";
            // 
            // directoryBox
            // 
            this.directoryBox.Location = new System.Drawing.Point(15, 38);
            this.directoryBox.Name = "directoryBox";
            this.directoryBox.Size = new System.Drawing.Size(279, 20);
            this.directoryBox.TabIndex = 4;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(300, 38);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 5;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // folderBrowser
            // 
            this.folderBrowser.Description = "Select Steam folder.";
            this.folderBrowser.RootFolder = System.Environment.SpecialFolder.ProgramFilesX86;
            this.folderBrowser.ShowNewFolderButton = false;
            // 
            // SavesFolderSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 169);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.directoryBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.savesFolderList);
            this.Controls.Add(this.infolbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SavesFolderSelection";
            this.Text = "Select Folder";
            this.Load += new System.EventHandler(this.SavesFolderSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infolbl;
        private System.Windows.Forms.ComboBox savesFolderList;
        private System.Windows.Forms.Button actionBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox directoryBox;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}