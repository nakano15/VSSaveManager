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
            LoadVersions();
            UpdateDirectoryPath();
            UpdateButtonState();
            UpdateSavesList();
        }

        void UpdateDirectoryPath()
        {
            directoryBox.Text = Main.SaveFolderDirectory;
        }

        void LoadVersions()
        {
            gameverbox.Items.Clear();
            foreach (Main.GameVersions gv in Enum.GetValues(typeof(Main.GameVersions)))
            {
                gameverbox.Items.Add(Main.ReturnGameVersionName(gv));
            }
            gameverbox.SelectedIndex = (int)Main.GameVersion;
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
            Main.SaveFolderDirectory = directoryBox.Text;
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

        private void folderBrowser_HelpRequest(object sender, EventArgs e)
        {

        }

        private void gameverbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.GameVersion = (Main.GameVersions)gameverbox.SelectedIndex;
            Main.SetDefaultGameVersions();
            UpdateDirectoryPath();
            Main.InitializeDirectories();
            UpdateButtonState();
            UpdateSavesList();
        }
    }
}
