using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VSSaveManager
{
    public partial class SavesManager : Form
    {
        private readonly Dictionary<string, string> CharacterNames = new Dictionary<string, string>();

        private SaveFile CurrentSaveFile;
        private NameInputBox nameinput;
        private int lastSelectedIndex = 0;
        private bool NgPlusAble = false;

        public SavesManager()
        {
            PopulateNameList();
            InitializeComponent();
        }

        private void PopulateNameList()
        {
            AddCharacterName("GENNARO", "Gennaro Belpaese");
            AddCharacterName("IMELDA", "Imelda Belpaese");
            AddCharacterName("PASQUALINA", "Pasqualina Belpaese");
            AddCharacterName("CIRO", "MissingN");
            AddCharacterName("PORTA", "Porta Ladonna");
            AddCharacterName("CAMILLO", "Poe Ratcho");
            AddCharacterName("GERMANA", "Suor Clerici");
            AddCharacterName("CAVALLO", "Yatta Cavallo");
            AddCharacterName("CROCI", "Krochi Freetto");
            AddCharacterName("DOMMARIO", "Dommario");
            AddCharacterName("CRISTINA", "Cristine Davain");
            AddCharacterName("GIOVANNA", "Giovanna Grana");
            AddCharacterName("MORTACCIO", "Mortaccio");
            AddCharacterName("LAMA", "Lama Ladonna");
            AddCharacterName("PUGNALA", "Pugnala Provola");
            AddCharacterName("POPPEA", "Poppea Pecorina");
            AddCharacterName("MARIA", "Bianca Ramba");
            AddCharacterName("CONCETTA", "Concetta Caciotta");
            AddCharacterName("TATANKA", "O'Sole Meeo");
            AddCharacterName("VERANDA", "Leda");
            AddCharacterName("EXDASH", "Exdash Exiviiq");
            AddCharacterName("PEPPINO", "Peppino");
            AddCharacterName("PINO", "Iguana Gallo Valletto");
            AddCharacterName("FEBBRA", "Divano Thelma");
            AddCharacterName("NOSTRO", "Mask of the Red Death");
            AddCharacterName("GRAZIELLA", "Minnah Mannarah");
            AddCharacterName("ASSUNTA", "Zi'Assunta Belpaese");
            AddCharacterName("DRAGOGION", "Gyorunton");
            AddCharacterName("AMBROGIO", "Sir Ambrojoe");
            AddCharacterName("PANINI", "Toastie");
            AddCharacterName("BOROS", "Gains Boros");
            AddCharacterName("SMITH", "Smith");
            AddCharacterName("NEO", "Boon Marrabbio");
            AddCharacterName("PANTALONE", "Big Trouser");
            AddCharacterName("PAVONE", "Cosmo Pavone");
            AddCharacterName("SIGMA", "Queen Sigma");
            AddCharacterName("ANTONIO", "Antonio Belpaese");
            AddCharacterName("ARENGIJUS", "Random");
            AddCharacterName("FINO", "MissingN");
            AddCharacterName("AVATAR", "Avatar Infernas");
            AddCharacterName("SCOREJ", "Scorej-Oni");
            AddCharacterName("MIANG", "Miang Moonspell");
            AddCharacterName("MENYA", "Menya Moonspell");
            AddCharacterName("TONY", "Gav'Et-Oni");
            AddCharacterName("SYUUTO", "Syuuto Moonspell");
            AddCharacterName("ONNA", "Babi-Onna");
            AddCharacterName("MCCOY", "McCoy-Oni");
            AddCharacterName("MEGAMENYA", "Megalo Menya Moonspell");
            AddCharacterName("MEGASYUUTO", "Megalo Syuuto Moonspell");
        }

        private string GetCharacterName(string ID)
        {
            if (CharacterNames.ContainsKey(ID)) return CharacterNames[ID];
            return ID;
        }

        private void AddCharacterName(string ID, string Name)
        {
            CharacterNames.Add(ID, Name);
        }

        /*private void dobtn_Click(object sender, EventArgs e)
        {
            SaveFile save = new SaveFile(saveBox.Text);
            goldBox.Text = ((int)(double)save.SaveObject["Coins"]).ToString();
            characterList.Items.Clear();
            foreach (string s in save.SaveObject["BoughtCharacters"].Values<string>())
                characterList.Items.Add(s);
            powerUpsList.Items.Clear();
            Dictionary<string, byte> PowerUpLevels = new Dictionary<string, byte>();
            foreach (string s in save.SaveObject["BoughtPowerups"].Values<string>())
            {
                if (PowerUpLevels.ContainsKey(s))
                    PowerUpLevels[s]++;
                else
                    PowerUpLevels.Add(s, 1);
            }
            foreach (string s in PowerUpLevels.Keys)
            {
                powerUpsList.Items.Add(s + " Lv: " + PowerUpLevels[s]);
            }
            weaponsList.Items.Clear();
            foreach (string s in save.SaveObject["CollectedWeapons"].Values<string>())
            {
                weaponsList.Items.Add(s);
            }
            stagesList.Items.Clear();
            foreach (string s in save.SaveObject["UnlockedStages"].Values<string>())
            {
                stagesList.Items.Add(s);
            }
            secretsList.Items.Clear();
            foreach (string s in save.SaveObject["Secrets"].Values<string>())
            {
                secretsList.Items.Add(s);
            }
            achievementsList.Items.Clear();
            foreach (string s in save.SaveObject["Achievements"].Values<string>())
            {
                achievementsList.Items.Add(s);
            }
            unlockedStagesList.Items.Clear();
            foreach (string s in save.SaveObject["UnlockedStages"].Values<string>())
            {
                unlockedStagesList.Items.Add(s);
            }
        }*/

        private void UpdateInformation(SaveFile save)
        {
            goldBox.Text = ((int)(double)save.SaveObject["Coins"]).ToString();
            characterList.Items.Clear();
            foreach (string s in save.SaveObject["BoughtCharacters"].Values<string>())
                characterList.Items.Add(GetCharacterName(s));
            powerUpsList.Items.Clear();
            Dictionary<string, byte> PowerUpLevels = new Dictionary<string, byte>();
            foreach (string s in save.SaveObject["BoughtPowerups"].Values<string>())
            {
                if (PowerUpLevels.ContainsKey(s))
                    PowerUpLevels[s]++;
                else
                    PowerUpLevels.Add(s, 1);
            }
            foreach (string s in PowerUpLevels.Keys)
            {
                powerUpsList.Items.Add(s + " Lv: " + PowerUpLevels[s]);
            }
            weaponsList.Items.Clear();
            foreach (string s in save.SaveObject["CollectedWeapons"].Values<string>())
            {
                weaponsList.Items.Add(s);
            }
            stagesList.Items.Clear();
            foreach (string s in save.SaveObject["UnlockedStages"].Values<string>())
            {
                stagesList.Items.Add(s);
            }
            secretsList.Items.Clear();
            foreach (string s in save.SaveObject["Secrets"].Values<string>())
            {
                secretsList.Items.Add(s);
            }
            achievementsList.Items.Clear();
            foreach (string s in save.SaveObject["Achievements"].Values<string>())
            {
                achievementsList.Items.Add(s);
            }
            unlockedStagesList.Items.Clear();
            foreach (string s in save.SaveObject["UnlockedStages"].Values<string>())
            {
                unlockedStagesList.Items.Add(s);
            }
            //
            characterNameBox.Text = GetCharacterName(save.SelectedCharacter);
            lastMapNameBox.Text = save.SelectedStage;
            CheckForNewGamePlus();
        }

        private void saveFileSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedProfile();
        }

        private void UpdateSelectedProfile()
        {
            deleteprofilebtn.Enabled = saveFileSelector.SelectedIndex != 0;
            newprofilebtn.Enabled = Main.ProfileSaveFiles.Count > 0;
            if (saveFileSelector.SelectedIndex == 0)
            {
                CurrentSaveFile = Main.MainSaveFile;
                saveprofilebtn.Text = "Save";
            }
            else
            {
                CurrentSaveFile = Main.ProfileSaveFiles[saveFileSelector.SelectedIndex - 1];
                saveprofilebtn.Text = "Get Save Data";
            }
            UpdateInformation(CurrentSaveFile);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateProfilesList();
        }

        private void UpdateProfilesList()
        {
            saveFileSelector.Items.Clear();
            saveFileSelector.Items.Add("* Main Save File");
            foreach (SaveFile save in Main.ProfileSaveFiles)
            {
                saveFileSelector.Items.Add(save.Profile);
            }
            saveFileSelector.SelectedIndex = 0;
        }

        private void saveprofilebtn_Click(object sender, EventArgs e)
        {
            if (saveFileSelector.SelectedIndex == 0)
            {
                TrySavingProfile();
            }
            else
            {
                if(lastSelectedIndex == 0 && saveFileSelector.SelectedIndex > 0)
                {
                    switch (MessageBox.Show("Would you like to save the changes on the current loaded save file?", "Warning.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            if (!TrySavingProfile()) return;
                            break;
                        case DialogResult.No:
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                    Main.TrySavingToDesktop(CurrentSaveFile);
                    Main.LastProfile = CurrentSaveFile.Profile;
                    Main.SaveSettingFile();
                    Main.OpenSavesFolder();
                }
            }
            lastSelectedIndex = saveFileSelector.SelectedIndex;
        }

        private bool TrySavingProfile()
        {
            if (CurrentSaveFile.Profile == "")
            {
                //Create
                ShowNameInput();
                if (nameinput.DialogResult != DialogResult.OK)
                {
                    return false;
                }
                string NewProfileName = nameinput.GetPickedName;
                CurrentSaveFile.Profile = NewProfileName;
                Main.CreateProfile(NewProfileName);
                SaveFile NewSave = new SaveFile(CurrentSaveFile.SaveObject);
                NewSave.Profile = NewProfileName;
                Main.SaveSaveFileInformation(NewSave, saveFileSelector.SelectedIndex == 0);
                Main.LoadSavesAndProfiles();
                UpdateProfilesList();
                UpdateSelectedProfile();
                Main.LastProfile = NewProfileName;
                Main.SaveSettingFile();
            }
            else
            {
                Main.SaveSaveFileInformation(CurrentSaveFile, saveFileSelector.SelectedIndex == 0);
                Main.OpenSavesFolder();
            }
            return true;
        }

        private void ShowNameInput()
        {
            nameinput = new NameInputBox();
            nameinput.ShowDialog();
        }

        private void newprofilebtn_Click(object sender, EventArgs e)
        {
            ShowNameInput();
            if(nameinput.DialogResult != DialogResult.OK)
            {
                return;
            }
            string NewProfileName = nameinput.GetPickedName;
            Main.CreateProfile(NewProfileName);
            JObject NewSave = JObject.Parse(ClearSaveFile.ClearSave);
            NewSave["saveDate"] = DateTime.Now;
            SaveFile NewSaveFile = new SaveFile(NewSave);
            NewSaveFile.Profile = NewProfileName;
            NewSaveFile.UpdateChecksum();
            Main.SaveSaveFileInformation(NewSaveFile, false);
            Main.LoadSavesAndProfiles();
            UpdateProfilesList();
            MessageBox.Show("New profile created.");
        }

        private void deleteprofilebtn_Click(object sender, EventArgs e)
        {
            if (saveFileSelector.SelectedIndex > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete " + CurrentSaveFile.Profile + " profile?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Main.DeleteProfile(CurrentSaveFile.Profile);
                    if (Main.LastProfile == CurrentSaveFile.Profile)
                        Main.LastProfile = "";
                    Main.LoadSavesAndProfiles();
                    UpdateProfilesList();
                }
            }
        }

        private void CheckForNewGamePlus()
        {
            NgPlusAble = CurrentSaveFile.SaveObject.Value<bool>("HasSeenFinalFireworks"); //("GreatestJubilee");
            ngpluselligibilitybox.Text = NgPlusAble ? "YES" : "NO";
            newGamePlusCharacterComboBox.Items.Clear();
            newGamePlusCharacterComboBox.Items.Add("No Unlocked Character");
            foreach (string s in CurrentSaveFile.SaveObject["BoughtCharacters"].Values<string>())
                newGamePlusCharacterComboBox.Items.Add(GetCharacterName(s));
            newGamePlusCharacterComboBox.SelectedIndex = 0;
            startNewGamePlusbtn.Enabled = NgPlusAble;
        }

        private void StartNewGamePlus()
        {
            JObject Save = CurrentSaveFile.SaveObject;
            List<string> HandyArray = new List<string>();
            string NgPlusCharacter = newGamePlusCharacterComboBox.SelectedIndex <= 0 ? "" : Save["BoughtCharacters"].Values<string>().ToArray()[newGamePlusCharacterComboBox.SelectedIndex - 1];
            Save["SelectedCharacter"] = NgPlusCharacter != "" ? NgPlusCharacter : "ANTONIO";
            Save["SelectedStage"] = "FOREST";
            Save["SelectedHyper"] = false;
            Save["SelectedHurry"] = false;
            Save["SelectedMazzo"] = false;
            Save["SelectedLimitBreak"] = false;
            Save["SelectedInverse"] = false;
            Save["SelectedReapers"] = false;
            Save["SelectedGoldenEggs"] = false;
            Save["SelectedMaxWeapons"] = 6;
            Save["itemInCollection"] = 0;
            Save["itemInUnlocks"] = 0;
            Save["itemInSecrets"] = 0;
            Save["Coins"] = 0;
            Save["HasUsedMirror"] = false;
            Save["HasUsedTrumpet"] = false;
            Save["RunCoins"] = 0;
            Save["RunEnemies"] = 0;
            Save["RunPickups"] = JArray.FromObject(new string[]
                {

                });
            Save["RunPickups_Coins"] = 0;
            Save["OwO"] = 0;
            Save["CompletedHurries"] = 0;
            Save["HasKilledFinalBoss"] = false;
            Save["HasSeenFinalFireworks"] = false;
            Save["ShowPickups"] = false;
            Save["ShowSmallMapIcons"] = false;
            Save["LongestFever"] = 0;
            Save["HighestFever"] = 0;
            if (NgPlusCharacter != "")
            {
                Save["BoughtCharacters"] = JArray.FromObject(new string[]
                    {
                    "ANTONIO",
                    "IMELDA",
                    "PASQUALINA",
                    "GENNARO",
                    NgPlusCharacter
                    });
            }
            else
            {
                Save["BoughtCharacters"] = JArray.FromObject(new string[]
                    {
                    "ANTONIO",
                    "IMELDA",
                    "PASQUALINA",
                    "GENNARO"
                    });
            }
            Save["BoughtPowerups"] = JArray.FromObject(new string[]
                {
                });
            Save["DisabledPowerups"] = JArray.FromObject(new string[]
                {
                });
            Save["CollectedWeapons"] = JArray.FromObject(new string[]
                {
                    "WHIP",
                    "HOLYBOOK",
                    "MAGIC_MISSILE",
                    "KNIFE",
                    "AXE",
                    "HOLYWATER",
                    "LAUREL",
                    "POWER",
                    "ARMOR",
                    "JUBILEE"
                });
            Save["UnlockedWeapons"] = JArray.FromObject(new string[]
                {
                });
            Save["UnlockedCharacters"] = JArray.FromObject(new string[]
                {
                });
            {
                string[] ItemsArray = Save["CollectedItems"].Values<string>().ToArray();
                if (ItemsArray.Contains("RELIC_GRIMOIRE"))
                    HandyArray.Add("RELIC_GRIMOIRE");
                if (ItemsArray.Contains("RELIC_GOUDA"))
                    HandyArray.Add("RELIC_GOUDA");
                if (ItemsArray.Contains("RELIC_MAP"))
                    HandyArray.Add("RELIC_MAP");
                if (ItemsArray.Contains("RELIC_BANGER"))
                    HandyArray.Add("RELIC_BANGER");
                if (ItemsArray.Contains("RELIC_NOSEGLASSES"))
                    HandyArray.Add("RELIC_NOSEGLASSES");
                if (ItemsArray.Contains("RELIC_SECRETS"))
                    HandyArray.Add("RELIC_SECRETS");
                if (ItemsArray.Contains("RELIC_RANDOMAZZO"))
                    HandyArray.Add("RELIC_RANDOMAZZO");
                Save["CollectedItems"] = JArray.FromObject(HandyArray.ToArray());
                HandyArray.Clear();
            }
            Save["Achievements"] = JArray.FromObject(new string[]
            {

            });
            Save["Secrets"] = JArray.FromObject(new string[]
            {

            });
            Save["UnlockedStages"] = JArray.FromObject(new string[]
            {
                "FOREST"
            });
            Save["UnlockedHypers"] = JArray.FromObject(new string[]
            {

            });
            Save["UnlockedPowerUpRanks"] = JArray.FromObject(new string[]
            {

            });
            {
                List<int> HandyIntArray = new List<int>();
                if (unlockGameKillerCheck.Checked)
                {
                    HandyIntArray.Add(0);
                }
                Save["UnlockedArcanas"] = JArray.FromObject(HandyIntArray.ToArray());
            }
            Save["KillCount"] = JObject.Parse("{}");
            Save["PickupCount"] = JObject.Parse("{}");
            Save["DestroyedCount"] = JObject.Parse("{}");
            Save["StageCompletionLog"] = JObject.Parse("{}");
            Save["CharacterStageData"] = JObject.Parse("{}");
            Save["CharacterEnemiesKilled"] = JObject.Parse("{}");
            Save["CharacterSurvivedMinutes"] = JObject.Parse("{}");
            Save["EggData"] = JObject.Parse("{}");
            Save["SealedItems"] = JArray.FromObject(new string[] { });
            Save["SealedWeapons"] = JArray.FromObject(new string[] { });
            UpdateInformation(CurrentSaveFile);
        }

        private void startNewGamePlusbtn_Click(object sender, EventArgs e)
        {
            StartNewGamePlus();
        }
    }
}
