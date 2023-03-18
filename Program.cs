using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSSaveManager
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            VSSaveManager.Main.LoadSettingFile();
            VSSaveManager.Main.InitializeDirectories();
            if (!VSSaveManager.Main.SavesDirectoryExist)
            {
                MessageBox.Show("Saves directory doesn't exist..", "Uh oh.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.Run(new SavesFolderSelection());
            if (VSSaveManager.Main.SavesDirectoryExist && VSSaveManager.Main.UserSaveFolder != "")
            {
                VSSaveManager.Main.LoadSavesAndProfiles();
                if (VSSaveManager.Main.MainSaveFileExists)
                    Application.Run(new SavesManager());
                else
                    MessageBox.Show("No save file exist. Did you tried to start a new playthrough already?", "Uh oh.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
