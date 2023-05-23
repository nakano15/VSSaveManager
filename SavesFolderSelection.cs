using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSSaveManager
{
    public partial class SavesFolderSelection : Form
    {
        public SavesFolderSelection()
        {
            InitializeComponent();
        }

        private void SavesFolderSelection_Load(object sender, EventArgs e)
        {
            /*if (!Main.SavesDirectoryExist)
            {
                savesFolderList.Visible = false;
                actionBtn.Text = "Close";
                infolbl.Text = "Couldn't find Steam directory of the game.";
            }
            else
            {
                savesFolderList.Items.Clear();
                foreach(string s in Main.FoldersFound)
                {
                    savesFolderList.Items.Add(s);
                }
                savesFolderList.SelectedIndex = 0;
            }*/
            directoryBox.Text = Main.SteamFolderDirectory;
            UpdateButtonState();
            UpdateSavesList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Main.SavesDirectoryExist)
            {
                Main.UserSaveFolder = Main.FoldersFound[savesFolderList.SelectedIndex];
                Main.SaveSettingFile();
            }
            Close();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            folderBrowser.ShowDialog();
            directoryBox.Text = folderBrowser.SelectedPath;
            Main.SteamFolderDirectory = directoryBox.Text;
            Main.InitializeDirectories();
            UpdateButtonState();
            UpdateSavesList();
        }

        private void UpdateButtonState()
        {
            actionBtn.Enabled = Main.SavesDirectoryExist;
        }

        private void UpdateSavesList()
        {
            savesFolderList.Items.Clear();
            if (Main.SavesDirectoryExist)
            {
                foreach (string s in Main.FoldersFound)
                {
                    savesFolderList.Items.Add(s);
                }
                savesFolderList.SelectedIndex = 0;
            }
            else
            {
                savesFolderList.Text = "";
            }
        }
    }
}
