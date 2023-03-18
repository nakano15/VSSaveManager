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
        internal static string GameSaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Steam\userdata\";
        internal static string ProfileSaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Vampire Survivors Save Manager\";
        internal static string DesktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\";
        internal static string UserSaveFolder = "";
        internal static string[] FoldersFound = new string[0];
        private static bool _SavesDirectoryExist = false;
        internal static bool SavesDirectoryExist { get { return _SavesDirectoryExist; } }
        internal static SaveFile MainSaveFile;
        internal static List<SaveFile> ProfileSaveFiles = new List<SaveFile>();
        private static bool _MainSaveFileExists = false;
        internal static bool MainSaveFileExists { get { return _MainSaveFileExists; } }
        internal static string GetFullGameSaveDirectory
        {
            get
            {
                return GameSaveDirectory + UserSaveFolder + @"\1794680\remote\";
            }
        }
        internal static string GetFullProfileDirectory
        {
            get
            {
                return ProfileSaveDirectory + UserSaveFolder + @"\";
            }
        }
        const string SaveFileName = "SaveData";
        internal static string LastProfile = "";
        static string SettingsFileName = ProfileSaveDirectory + @"\settings.json";

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
                    }
                }
            }
        }

        internal static void InitializeDirectories()
        {
            if (_SavesDirectoryExist = Directory.Exists(GameSaveDirectory))
            {
                FoldersFound = Directory.GetDirectories(GameSaveDirectory, "*", SearchOption.TopDirectoryOnly).Select(x => x.Substring(GameSaveDirectory.Length)).ToArray();
            }
        }

        internal static void LoadSavesAndProfiles()
        {
            string SaveFilePath = GetFullGameSaveDirectory + SaveFileName;
            if(_MainSaveFileExists = File.Exists(SaveFilePath))
            {
                MainSaveFile = new SaveFile(LoadJsonFile(SaveFilePath));
                MainSaveFile.Profile = LastProfile;
            }
            else
            {
                return;
            }
            ProfileSaveFiles.Clear();
            if (!Directory.Exists(GetFullProfileDirectory))
                Directory.CreateDirectory(GetFullProfileDirectory);
            bool CurrentProfileExists = false;
            foreach(string d in Directory.GetDirectories(GetFullProfileDirectory, "*", SearchOption.TopDirectoryOnly))
            {
                SaveFilePath = d + @"\" + SaveFileName;
                if(File.Exists(SaveFilePath))
                {
                    SaveFile s = new SaveFile(LoadJsonFile(SaveFilePath));
                    s.Profile = d.Substring(GetFullProfileDirectory.Length);
                    if (s.Profile == LastProfile) CurrentProfileExists = true;
                    ProfileSaveFiles.Add(s);
                }
            }
            if (!CurrentProfileExists) LastProfile = "";
        }

        internal static bool ProfilePathExists(string Profile)
        {
            return Directory.Exists(GetFullGameSaveDirectory + Profile);
        }

        internal static void CreateProfile(string Profile)
        {
            string ProfileDirectory = GetFullProfileDirectory + Profile;
            Directory.CreateDirectory(ProfileDirectory);
        }

        internal static void DeleteProfile(string Profile)
        {
            string ProfileDirectory = GetFullProfileDirectory + Profile;
            Directory.Delete(ProfileDirectory, true);
        }

        internal static void SaveSaveFileInformation(SaveFile save, bool IsMainSaveFile)
        {
            if (!Directory.Exists(GetFullProfileDirectory + save.Profile))
                Directory.CreateDirectory(GetFullProfileDirectory + save.Profile);
            string SaveDataPath;
            if (IsMainSaveFile)
            {
                SaveDataPath = DesktopDirectory + SaveFileName;
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
            SaveDataPath = GetFullProfileDirectory + save.Profile + @"\" + SaveFileName;
            if (File.Exists(SaveDataPath)) File.Delete(SaveDataPath);
            using (FileStream stream = new FileStream(SaveDataPath, FileMode.CreateNew))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(save.SaveObject.ToString());
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
            System.Windows.Forms.MessageBox.Show("Use a online Vampire Survivors save editor on the \"SaveData\" in your desktop to update checksum, then download the edited save file.\nDepending on the site you use, it might be downloaded as \"SaveData.sav\", rename it to \"SaveData\".\nMove the SaveData in your desktop to the folder where your save file is at.\nRemember to disable Steam Cloud temporarily.");
        }

        private static string LoadJsonFile(string Path)
        {
            string Text = "";
            using (StreamReader stream = new StreamReader(Path))
            {
                Text = stream.ReadToEnd();
            }
            return Text;
        }
    }
}
