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
            this.SuspendLayout();
            // 
            // infolbl
            // 
            this.infolbl.AutoSize = true;
            this.infolbl.Location = new System.Drawing.Point(12, 9);
            this.infolbl.Name = "infolbl";
            this.infolbl.Size = new System.Drawing.Size(152, 13);
            this.infolbl.TabIndex = 0;
            this.infolbl.Text = "Select the save folder to open.";
            // 
            // savesFolderList
            // 
            this.savesFolderList.FormattingEnabled = true;
            this.savesFolderList.Location = new System.Drawing.Point(15, 25);
            this.savesFolderList.Name = "savesFolderList";
            this.savesFolderList.Size = new System.Drawing.Size(149, 21);
            this.savesFolderList.TabIndex = 1;
            // 
            // actionBtn
            // 
            this.actionBtn.Location = new System.Drawing.Point(48, 52);
            this.actionBtn.Name = "actionBtn";
            this.actionBtn.Size = new System.Drawing.Size(75, 23);
            this.actionBtn.TabIndex = 2;
            this.actionBtn.Text = "Open";
            this.actionBtn.UseVisualStyleBackColor = true;
            this.actionBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // SavesFolderSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(183, 91);
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
    }
}