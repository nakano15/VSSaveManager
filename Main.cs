using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VSSaveManager
{
    public class Main
    {
        internal static string SteamFolderDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Steam\userdata";
        internal static string EGSFolderDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Vampire_Survivors_EGS";
        internal static string SaveFolderDirectory = SteamFolderDirectory;
        internal static string DesktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\";
        internal static string AppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Vampire_Survivors_Data\";
        internal static string UserSaveFolder = "";
        internal static string[] FoldersFound = new string[0];
        internal static GameVersions GameVersion = GameVersions.Steam;
        private static bool _SavesDirectoryExist = false;
        internal static bool SavesDirectoryExist { get { return _SavesDirectoryExist; } }
        internal static SaveFile MainSaveFile;
        private static bool _MainSaveFileExists = false;
        internal static bool MainSaveFileExists { get { return _MainSaveFileExists; } }
        internal static string GetFullGameSaveDirectory
        {
            get
            {
                switch (GameVersion)
                {
                    case GameVersions.Steam:
                        return SaveFolderDirectory + "\\" + UserSaveFolder + @"\1794680\remote\";
                    case GameVersions.EGS:
                        return SaveFolderDirectory + "\\" + UserSaveFolder + @"\";
                }
                return "Not Setup Game Version!!";
            }
        }
        internal static string SaveFileName
        {
            get
            {
                switch (GameVersion)
                {
                    case GameVersions.EGS:
                        return "SaveData.sav";
                }
                return "SaveData";
            }
        }
        internal static string LastProfile = "";
        static string SettingsFileName = Environment.CurrentDirectory + @"\settings.json";

        static bool EndsWithCorrectPath(string Path)
        {
            switch (GameVersion)
            {
                case GameVersions.Steam:
                    return Path.EndsWith("userdata");
                case GameVersions.EGS:
                    return Path.EndsWith("Vampire_Survivors_EGS");
            }
            return false;
        }

        internal static void SetDefaultGameVersions()
        {
            switch (GameVersion)
            {
                case GameVersions.Steam:
                    SaveFolderDirectory = SteamFolderDirectory;
                    break;
                case GameVersions.EGS:
                    SaveFolderDirectory = EGSFolderDirectory;
                    break;
            }
        }

        internal static void SaveSettingFile()
        {
            if(File.Exists(SettingsFileName))
            {
                File.Delete(SettingsFileName);
            }
            using (FileStream stream = new FileStream(SettingsFileName, FileMode.CreateNew))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    JObject save = new JObject();
                    save.Add("LastProfile", LastProfile);
                    save.Add("SaveDirectory", SaveFolderDirectory);
                    save.Add("GameVersion", (int)GameVersion);
                    writer.Write(save.ToString());
                }
            }
        }

        internal static void LoadSettingFile()
        {
            if (File.Exists(SettingsFileName))
            {
                using (FileStream stream = new FileStream(SettingsFileName, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string Code = reader.ReadToEnd();
                        JObject save = JObject.Parse(Code);
                        LastProfile = save["LastProfile"].Value<string>();
                        if (save.ContainsKey("SaveDirectory"))
                            SaveFolderDirectory = save["SaveDirectory"].Value<string>();
                        if (save.ContainsKey("GameVersion"))
                            GameVersion = (GameVersions)save["GameVersion"].Value<int>();
                    }
                }
            }
        }

        internal static void InitializeDirectories()
        {
            _SavesDirectoryExist = false;
            FoldersFound = new string[0];
            if (Directory.Exists(SaveFolderDirectory))
            {
                if (EndsWithCorrectPath(SaveFolderDirectory))
                {
                    _SavesDirectoryExist = true;
                    FoldersFound = Directory.GetDirectories(SaveFolderDirectory + "\\", "*", SearchOption.TopDirectoryOnly).Select(x => x.Substring(SaveFolderDirectory.Length + 1)).ToArray();
                }
            }
        }

        internal static void LoadSavesAndProfiles()
        {
            string SaveFilePath = GetFullGameSaveDirectory + SaveFileName;
            if(_MainSaveFileExists = File.Exists(SaveFilePath))
            {
                MainSaveFile = new SaveFile(LoadJsonFile(SaveFilePath));
            }
        }

        internal static void SaveSaveFileInformation(SaveFile save)
        {
            string SaveDataPath = DesktopDirectory + SaveFileName;
            save.UpdateChecksum();
            if (File.Exists(SaveDataPath)) File.Delete(SaveDataPath);
            using (FileStream stream = new FileStream(SaveDataPath, FileMode.CreateNew))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(save.Minify());
                }
            }
        }

        internal static void TrySavingToDesktop(SaveFile save)
        {
            string SaveDataPath = DesktopDirectory + "SaveData";
            if (File.Exists(SaveDataPath))
                File.Delete(SaveDataPath);
            using (FileStream stream = new FileStream(SaveDataPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(save.SaveObject.ToString());
                }
            }
        }

        internal static void OpenSavesFolder()
        {
            System.Diagnostics.Process.Start(GetFullGameSaveDirectory);
            //System.Windows.Forms.MessageBox.Show("Use a online Vampire Survivors save editor on the \"SaveData\" in your desktop to update checksum, then download the edited save file.\nDepending on the site you use, it might be downloaded as \"SaveData.sav\", rename it to \"SaveData\".\nMove the SaveData in your desktop to the folder where your save file is at.\nRemember to disable Steam Cloud temporarily.");
        }

        private static string LoadJsonFile(string Path)
        {
            string Text = "";
            using (StreamReader stream = new StreamReader(Path, Encoding.UTF8))
            {
                Text = stream.ReadToEnd();
            }
            return Text;
        }

        public static string ReturnGameVersionName(GameVersions ver)
        {
            switch (ver)
            {
                case GameVersions.Steam:
                    return "Steam";
                case GameVersions.EGS:
                    return "Epic Games Store";
            }
            return ver.ToString();
        }

        public enum GameVersions : byte
        {
            Steam,
            EGS
        }
    }
}
