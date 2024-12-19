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
        private readonly Dictionary<string, string> RelicNames = new Dictionary<string, string>();
        private readonly Dictionary<string, string> WeaponNames = new Dictionary<string, string>();
        private readonly Dictionary<string, string> StageNames = new Dictionary<string, string>();

        private SaveFile CurrentSaveFile;
        private bool NgPlusAble = false;
        bool BuyBackCharacters
        {
            get
            {
                return buyStartersCheck.Checked;
            }
        }

        public SavesManager()
        {
            PopulateNameList();
            PopulateWeaponList();
            PopulateRelicList();
            PopulateStageList();
            InitializeComponent();
            UpdateSelectedProfile();
        }

        static List<int> NgPlusCharactersIndex = new List<int>();

        bool HasMoonspellDlc => CurrentSaveFile.SaveObject["UnlockedStages"].Values<string>().Contains("MOONSPELL");
        bool HasFoscariDlc => CurrentSaveFile.SaveObject["UnlockedStages"].Values<string>().Contains("FOSCARI");
        bool HasEmergencyMeetingDlc => CurrentSaveFile.SaveObject["UnlockedStages"].Values<string>().Contains("POLUS");

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
            AddCharacterName("SHEMOONITA", "She-Moon Eeta");
            AddCharacterName("SPACEDUDE", "Space Dude");
            AddCharacterName("TUPU", "Bat Robbert");
            AddCharacterName("SANTA", "Santa Ladonna");
            AddCharacterName("BATSBATSBATS", "Bats Bats Bats");
            AddCharacterName("ROSE", "Rose De Infernas");
            AddCharacterName("GYORUNTIN", "Gyoruntin");
            //Moonspell
            AddCharacterName("MIANG", "Miang Moonspell");
            AddCharacterName("MENYA", "Menya Moonspell");
            AddCharacterName("TONY", "Gav'Et-Oni");
            AddCharacterName("SYUUTO", "Syuuto Moonspell");
            AddCharacterName("ONNA", "Babi-Onna");
            AddCharacterName("MCCOY", "McCoy-Oni");
            AddCharacterName("MEGAMENYA", "Megalo Menya Moonspell");
            AddCharacterName("MEGASYUUTO", "Megalo Syuuto Moonspell");
            //Foscari
            AddCharacterName("ELEANOR", "Eleanor Uziron");
            AddCharacterName("VICTOR", "Maruto Cuts");
            AddCharacterName("KEIRA", "Keitha Muort");
            AddCharacterName("LUMINAIRE", "Luminaire Foscari");
            AddCharacterName("GENEVIEVE", "Genevieve Gruyère");
            AddCharacterName("MEGAGENEVIEVE", "Je-Ne-Viv");
            AddCharacterName("CTRPCAKE", "Sammy");
            AddCharacterName("GHOUL", "Rottin'Ghoul");
            // Emergency Meeting
            AddCharacterName("C1_CREWMATE", "Crewmate Dino");
            AddCharacterName("C1_ENGINEER", "Engineer Gino");
            AddCharacterName("C1_GHOST", "Ghost Lino");
            AddCharacterName("C1_SHAPESHIFTER", "Shapeshifter Nino");
            AddCharacterName("C1_GUARDIAN", "Guardian Pina");
            AddCharacterName("C1_IMPOSTOR", "Impostor Rina");
            AddCharacterName("C1_SCIENTIST", "Scientist Mina");
            AddCharacterName("C1_HORSE", "Horse");
            AddCharacterName("C1_MEGAIMPOSTOR", "Megalo Impostor Rina");
            // Operation Guns
            AddCharacterName("FB_BILLRIZER", "Bill Rizer");
            AddCharacterName("FB_LANCE", "Lance Bean");
            AddCharacterName("FB_ARIANA", "Ariana");
            AddCharacterName("FB_LUCIA", "Lucia Zero");
            AddCharacterName("FB_BRADFANG", "Brad Fang");
            AddCharacterName("FB_BROWNY", "Browny");
            AddCharacterName("FB_SHEENA", "Sheena Etranzi");
            AddCharacterName("FB_PROBO", "Probotector");
            AddCharacterName("FB_STANLEY", "Stanley");
            AddCharacterName("FB_NEWT", "Newt Plissken");
            AddCharacterName("FB_COLONEL", "Colonel Bahamut");
            AddCharacterName("FB_SIMONDO", "Simondo Belmont");
            // Ode to Castlevania
            AddCharacterName("TP_LEON", "Leon Belmont");
            AddCharacterName("TP_SONIA", "Sonia Belmont");
            AddCharacterName("TP_TREVOR", "Trevor Belmont");
            AddCharacterName("TP_CHRISTOPHER", "Christopher Belmont");
            AddCharacterName("TP_SIMON", "Simon Belmont");
            AddCharacterName("TP_JUSTE", "Juste Belmont");
            AddCharacterName("TP_RICHTER", "Richter Belmont");
            AddCharacterName("TP_JULIUS", "Julius Belmont");
            AddCharacterName("TP_GRANT", "Grant Belmont");
            AddCharacterName("TP_JOHN", "John Morris");
            AddCharacterName("TP_JONATHAN", "Jonathan Morris");
            AddCharacterName("TP_SOMA", "Soma Cruz");
            AddCharacterName("TP_CHARLOTTE", "Charlotte Aulin");
            AddCharacterName("TP_SYPHA", "Sypha Belnades");
            AddCharacterName("TP_YOKO", "Yoko Belnades");
            AddCharacterName("TP_ALUCARD", "Alucard");
            AddCharacterName("TP_ERIC", "Eric Lecarde");
            AddCharacterName("TP_HECTOR", "Hector");
            AddCharacterName("TP_MARIAA", "Maria Renard (Adult)");
            AddCharacterName("TP_SHANOA", "Shanoa");
            AddCharacterName("TP_DRACULA", "Vlad Tepes Dracula");
            AddCharacterName("TP_LISA", "Lisa Tepes");
            AddCharacterName("TP_MINA", "Mina Hakuba");
            AddCharacterName("TP_CORNELL", "Cornell");
            AddCharacterName("TP_RINALDO", "Rinaldo Gandolfi");
            AddCharacterName("TP_VINCENT", "Vincent Dorin");
            AddCharacterName("TP_NATHAN", "Nathan Graves");
            AddCharacterName("TP_JULIA", "Julia Laforeze");
            AddCharacterName("TP_SARA", "Sara Trantoul");
            AddCharacterName("TP_QUINCY", "Quincy Morris");
            AddCharacterName("TP_MAXIM", "Maxim Kischine");
            AddCharacterName("TP_REINHARDT", "Reinhardt Schneider");
            AddCharacterName("TP_HENRY", "Henry");
            AddCharacterName("TP_CARRIE", "Carrie Fernandez");
            AddCharacterName("TP_STGERMAIN", "Saint Germain");
            AddCharacterName("TP_ALBUS", "Albus");
            AddCharacterName("TP_ISAAC", "Isaac");
            AddCharacterName("TP_ELIZABETH", "Elizabeth Bartley");
            AddCharacterName("TP_SHAFT", "Shaft");
            AddCharacterName("TP_BARLOWE", "Barlowe");
            AddCharacterName("TP_MARIAB", "Young Maria Renard");
            AddCharacterName("TP_FAMILIARS", "Familiar");
            AddCharacterName("TP_INNOCENT_DEVILS", "Innocent Devil");
            AddCharacterName("TP_CORNELL_BCM", "Blue Crescent Moon Cornell");
            AddCharacterName("TP_FERRYMAN", "Ferryman");
            AddCharacterName("TP_LIBRARIAN", "Master Librarian");
            AddCharacterName("TP_HAMMER", "Hammer");
            AddCharacterName("TP_WIND", "Wind");
            AddCharacterName("TP_JONATHAN_AND_CHARLOTTE", "Jonathan & Charlotte");
            AddCharacterName("TP_CHARLOTTE_AND_JONATHAN", "Charlotte & Jonathan");
            AddCharacterName("TP_STELLA_AND_LORETTA", "Stella & Loretta Lecarde");
            AddCharacterName("TP_LORETTA_AND_STELLA", "Loretta & Stella Lecarde");
            AddCharacterName("TP_STELLA", "Stella Lecarde");
            AddCharacterName("TP_LORETTA", "Loretta Lecarde");
            AddCharacterName("TP_BRAUNER", "Brauner");
            AddCharacterName("TP_SOLEIL", "Soleil Belmont");
            AddCharacterName("TP_DARIO", "Dario Bossi");
            AddCharacterName("TP_DMITRI", "Dmitrii Blinov");
            AddCharacterName("TP_CELIA", "Celia Fortner");
            AddCharacterName("TP_GRAHAM", "Graham Jones");
            AddCharacterName("TP_JOACHIM", "Joachim Armster");
            AddCharacterName("TP_WALTER", "Walter Bernhard");
            AddCharacterName("TP_CARMILLA", "Carmilla");
            AddCharacterName("TP_OLROX", "Count Olrox");
            AddCharacterName("TP_CAVETROLL", "Cave Troll");
            AddCharacterName("TP_FLEAMAN", "Fleaman");
            AddCharacterName("TP_AXEARMOR", "Axe Armor");
            AddCharacterName("TP_FROZENSHADE", "Frozen Shade");
            AddCharacterName("TP_SUCCUBUS", "Succubus");
            AddCharacterName("TP_KEREMET", "Keremet");
            AddCharacterName("TP_SNIPER", "Alamaric Sniper");
            AddCharacterName("TP_BLACKMORE", "Blackmore");
            AddCharacterName("TP_MALPHAS", "Malphas");
            AddCharacterName("TP_DEATH", "Death");
            AddCharacterName("TP_GALAMOTH", "Galamoth");
            AddCharacterName("TP_ELIZABETH_MEGA", "Megalo Elizabeth Bartley");
            AddCharacterName("TP_OLROX_MEGA", "Megalo Olrox");
            AddCharacterName("TP_DEATH_MEGA", "Megalo Death");
            AddCharacterName("TP_DRACULA_MEGA", "Megalo Dracula");
            AddCharacterName("TP_CHAOS", "Chaos");
        }

        void PopulateStageList()
        {
            AddStageName("FOREST", "Mad Forest");
            AddStageName("BATCOUNTRY", "Bat Country");
            AddStageName("BONEZONE", "Bone Zone");
            AddStageName("CHAPEL", "Cappella Magna");
            AddStageName("GREENACRES", "Green Acres");
            AddStageName("LIBRARY", "Inlaid Library");
            AddStageName("MACHINE", "Eudaimonia Machine");
            AddStageName("MOLISE", "Il Molise");
            AddStageName("MOONSPELL", "Mt. Moonspell");
            AddStageName("RASH", "Boss Rash");
            AddStageName("SINKING", "Moongolow");
            //AddStageName("STAGEX", ""); //Unknown
            AddStageName("TOWER", "Gallo Tower");
            AddStageName("TOWERBRIDGE", "Tiny Bridge");
            //AddStageName("WAREHOUSE", ""); //Unknown
            //Foscari
            AddStageName("FOSCARI", "Lake Foscari");
            AddStageName("FOSCARI2", "Abyss Foscari");
        }

        void PopulateWeaponList()
        {

        }

        void PopulateRelicList()
        {

        }

        private string GetStageName(string ID)
        {
            if (StageNames.ContainsKey(ID)) return StageNames[ID];
            return ID;
        }

        private void AddStageName(string ID, string Name)
        {
            StageNames.Add(ID, Name);
        }

        private string GetRelicName(string ID)
        {
            if (RelicNames.ContainsKey(ID)) return RelicNames[ID];
            return ID;
        }

        private void AddRelicName(string ID, string Name)
        {
            RelicNames.Add(ID, Name);
        }

        private string GetWeaponName(string ID)
        {
            if (WeaponNames.ContainsKey(ID)) return WeaponNames[ID];
            return ID;
        }

        private void AddWeaponName(string ID, string Name)
        {
            WeaponNames.Add(ID, Name);
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
            CurrentSaveFile = Main.MainSaveFile;
            UpdateInformation(CurrentSaveFile);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void saveprofilebtn_Click(object sender, EventArgs e)
        {
            TrySavingProfile();
        }

        private bool TrySavingProfile()
        {
            Main.SaveSaveFileInformation(CurrentSaveFile);
            Main.OpenSavesFolder();
            MessageBox.Show("Altered save file placed on the Desktop.\nUse a Vampire Survivors Save Editor to update the hashcode of the save file before placing on the save folder.");
            return true;
        }

        private void newprofilebtn_Click(object sender, EventArgs e)
        {
            JObject NewSave = JObject.Parse(ClearSaveFile.ClearSave);
            NewSave["saveDate"] = DateTime.Now;
            SaveFile NewSaveFile = new SaveFile(NewSave);
            NewSaveFile.UpdateChecksum();
            Main.SaveSaveFileInformation(NewSaveFile);
            Main.LoadSavesAndProfiles();
            MessageBox.Show("New save file created and placed on the Desktop.\nUse a Vampire Survivor Save Editor to change the hash code before placing the save file in the folder.");
        }

        private void CheckForNewGamePlus()
        {
            NgPlusAble = CurrentSaveFile.SaveObject.Value<bool>("HasSeenFinalFireworks"); //("GreatestJubilee");
            ngpluselligibilitybox.Text = NgPlusAble ? "YES" : "NO";
            newGamePlusCharacterComboBox.Items.Clear();
            newGamePlusCharacterComboBox2.Items.Clear();
            newGamePlusCharacterComboBox3.Items.Clear();
            newGamePlusCharacterComboBox4.Items.Clear();
            newGamePlusCharacterComboBox.Items.Add("1st: No Unlocked Character");
            newGamePlusCharacterComboBox2.Items.Add("2nd: No Unlocked Character");
            newGamePlusCharacterComboBox3.Items.Add("3rd: No Unlocked Character");
            newGamePlusCharacterComboBox4.Items.Add("4th: No Unlocked Character");
            NgPlusCharactersIndex.Clear();
            int Index = 0;
            foreach (string s in CurrentSaveFile.SaveObject["BoughtCharacters"].Values<string>())
            {
                if (s != "ANTONIO")
                {
                    newGamePlusCharacterComboBox.Items.Add(GetCharacterName(s));
                    newGamePlusCharacterComboBox2.Items.Add(GetCharacterName(s));
                    newGamePlusCharacterComboBox3.Items.Add(GetCharacterName(s));
                    newGamePlusCharacterComboBox4.Items.Add(GetCharacterName(s));
                    NgPlusCharactersIndex.Add(Index);
                }
                Index++;
            }
            newGamePlusCharacterComboBox.SelectedIndex = 0;
            newGamePlusCharacterComboBox2.SelectedIndex = 0;
            newGamePlusCharacterComboBox3.SelectedIndex = 0;
            newGamePlusCharacterComboBox4.SelectedIndex = 0;
            newGamePlusCharacterComboBox2.Enabled = false;
            newGamePlusCharacterComboBox3.Enabled = false;
            newGamePlusCharacterComboBox4.Enabled = false;
            startNewGamePlusbtn.Enabled = NgPlusAble;
            bool HasTrisection = false, HasAtlas = false, HasRandomLevelUp = false, HasArcana = false, HasDarkana = false;
            foreach(string relic in CurrentSaveFile.SaveObject["CollectedItems"].Values<string>().ToArray())
            {
                if (relic == "RELIC_TRISECTION") HasTrisection = true;
                if (relic == "RELIC_ATLAS") HasAtlas = true;
                if (relic == "RELIC_BRAVESTORY") HasRandomLevelUp = true;
                if (relic == "RELIC_RANDOMAZZO") HasArcana = true;
                if (relic == "RELIC_DARKASSO") HasDarkana = true;
            }
            rouletteActiveCheckBox.Enabled = HasTrisection;
            rouletteActiveCheckBox.Checked = HasTrisection && CurrentSaveFile.SaveObject["SelectedRandomEvents"].Value<bool>();
            adventureUnlockCheckbox.Enabled = adventureUnlockCheckbox.Checked = HasAtlas;
            randomLevelUpCheckBox.Enabled = HasRandomLevelUp;
            randomLevelUpCheckBox.Checked = HasRandomLevelUp && CurrentSaveFile.SaveObject["SelectedRandomLevels"].Value<bool>();
            arcanaUnlockCheck.Enabled = arcanaUnlockCheck.Checked = HasArcana;
            darkanaUnlockCheck.Enabled = darkanaUnlockCheck.Checked = HasDarkana;
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
            Save["SelectedRandomEvents"] = rouletteActiveCheckBox.Checked;
            Save["SelectedLimitBreak"] = false;
            Save["SelectedInverse"] = false;
            Save["SelectedReapers"] = false;
            Save["SelectedGoldenEggs"] = false;
            Save["SelectedBGMSave"] = false;
            Save["SelectedBGMMod"] = "Normal";
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
            Save["LifetimeSurvived"] = 0;
            Save["LifetimeHeal"] = 0;
            Save["LifetimeCoins"] = 0;
            Save["RunPickups_Coins"] = 0;
            Save["OwO"] = 0;
            Save["CompletedHurries"] = 0;
            Save["HasKilledTheFinalBoss"] = false;
            Save["HasSeenFinalFireworks"] = false;
            Save["LongestFever"] = 0;
            Save["HighestFever"] = 0;
            Save["PlayedRNJ"] = 0;
            Save["TopLapsCarlo"] = 0;
            Save["TotalLapsCarlo"] = 0;
            Save["TopLapsHighway"] = 0;
            Save["TotalLapsHighway"] = 0;
            Save["TrainHazardEnemiesHit"] = 0;
            {
                string[] BoughtCharacters = Save["BoughtCharacters"].Values<string>().ToArray();
                List<string> Characters = new List<string>();
                if (BuyBackCharacters)
                {
                    Characters.Add("ANTONIO");
                }
                else
                {
                    Characters.Add("ANTONIO");
                    Characters.Add("IMELDA");
                    Characters.Add("PASQUALINA");
                    Characters.Add("GENNARO");
                }
                if (newGamePlusCharacterComboBox.SelectedIndex > 0)
                {
                    Characters.Add(BoughtCharacters[NgPlusCharactersIndex[newGamePlusCharacterComboBox.SelectedIndex - 1]]);
                }
                if (newGamePlusCharacterComboBox2.SelectedIndex > 0 && newGamePlusCharacterComboBox2.Enabled)
                {
                    Characters.Add(BoughtCharacters[NgPlusCharactersIndex[newGamePlusCharacterComboBox2.SelectedIndex - 1]]);
                }
                if (newGamePlusCharacterComboBox3.SelectedIndex > 0 && newGamePlusCharacterComboBox3.Enabled)
                {
                    Characters.Add(BoughtCharacters[NgPlusCharactersIndex[newGamePlusCharacterComboBox3.SelectedIndex - 1]]);
                }
                if (newGamePlusCharacterComboBox4.SelectedIndex > 0 && newGamePlusCharacterComboBox4.Enabled)
                {
                    Characters.Add(BoughtCharacters[NgPlusCharactersIndex[newGamePlusCharacterComboBox4.SelectedIndex - 1]]);
                }
                Save["BoughtCharacters"] = JArray.FromObject(Characters.ToArray());
                Save["UnlockedCharacters"] = JArray.FromObject(Characters.ToArray());
                /*if (BuyBackCharacters)
                {
                    Save["UnlockedCharacters"] = JArray.FromObject(new string[]
                        {
                        "IMELDA",
                        "PASQUALINA",
                        "GENNARO"
                        });
                }
                else
                {
                    Save["UnlockedCharacters"] = JArray.FromObject(new string[]
                        {
                        });
                }*/
                Characters.Clear();
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
            Save["SelectedSkins"] = JObject.Parse("{}");
            Save["UnlockedSkins"] = JObject.Parse("{}");
            Save["UnlockedSkinsV2"] = JObject.Parse("{}");
            Save["SelectedSkinsV2"] = JObject.Parse("{}");
            Save["BoughtSkins"] = JArray.FromObject(new string[]
                {
                });
            {
                if (!noArtifactsCheck.Checked)
                {
                    string[] ItemsArray = Save["CollectedItems"].Values<string>().ToArray();
                    if (!removeProgressionArtsCheck.Checked)
                    {
                        if (ItemsArray.Contains("RELIC_GRIMOIRE"))
                            HandyArray.Add("RELIC_GRIMOIRE");
                        if (ItemsArray.Contains("RELIC_GOUDA"))
                            HandyArray.Add("RELIC_GOUDA");
                        if (ItemsArray.Contains("RELIC_MAP"))
                            HandyArray.Add("RELIC_MAP");
                        if (arcanaUnlockCheck.Checked && ItemsArray.Contains("RELIC_RANDOMAZZO"))
                            HandyArray.Add("RELIC_RANDOMAZZO");
                        if (darkanaUnlockCheck.Checked && ItemsArray.Contains("RELIC_DARKASSO"))
                            HandyArray.Add("RELIC_DARKASSO");
                    }
                    if (ItemsArray.Contains("RELIC_NOSEGLASSES"))
                        HandyArray.Add("RELIC_NOSEGLASSES");
                    if (ItemsArray.Contains("RELIC_SECRETS"))
                        HandyArray.Add("RELIC_SECRETS");
                    if (ItemsArray.Contains("RELIC_BANGER"))
                        HandyArray.Add("RELIC_BANGER");
                }
                if (rouletteActiveCheckBox.Checked)
                    HandyArray.Add("RELIC_TRISECTION");
                if (adventureUnlockCheckbox.Checked)
                    HandyArray.Add("RELIC_ATLAS");
                if (randomLevelUpCheckBox.Checked)
                    HandyArray.Add("RELIC_BRAVESTORY");
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
            Save["OpenedCoffins"] = JArray.FromObject(new string[]
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
            Save["MusicSelectionPerStage"] = JObject.Parse("{}");
            if (!keepGoldenEggsCheck.Checked)
                Save["EggData"] = JObject.Parse("{}");
            Save["SealedItems"] = JArray.FromObject(new string[] { });
            Save["SealedWeapons"] = JArray.FromObject(new string[] { });
            Save["SelectedRandomEvents"] = false;
            Save["SelectedRandomLevels"] = false;
            Save["HasSeenAdventureReveal"] = false;
            Save["HasPlayedStage3"] = false;
            Save["HasSeenDarkanaTransition"] = false;
            if (!adventureProgressKeepCheck.Checked)
            {
                Save["AdventureProgress"] = JArray.FromObject(new string[] { });
                Save["AdventuresSaveData"] = JObject.Parse("{}");
                Save["AdventureCompletionCount"] = 0;
                Save["TotalAdventurePlaytime"] = 0;
                Save["AllTimeAdventurePlaytime"] = 0;
                Save["AscensionPointsAllocation"] = JObject.Parse("{}");
                Save["AdventureStars"] = 0;
                Save["CompletedAdventures"] = JArray.FromObject(new string[] { });
                Save["SeenAscensionPopups"] = JArray.FromObject(new string[] { });
            }
            UpdateInformation(CurrentSaveFile);
        }

        private void startNewGamePlusbtn_Click(object sender, EventArgs e)
        {
            StartNewGamePlus();
        }

        private void openSaveFolderbtn_Click(object sender, EventArgs e)
        {
            Main.OpenSavesFolder();
        }

        private void helpbtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Clicking any button inside this box will create a save file on your desktop, so don't worry.");
            MessageBox.Show("Save Changes will create a save file with the changes done here.");
            MessageBox.Show("Create Clean Save will create a totally new game save that you can use on desktop.");
            MessageBox.Show("Open Save Folder will open the folder where the game save file is located. Very important for altering save files.");
            MessageBox.Show("Every time you want to change your current save file, use a Vampire Survivors Save Editor to change the hashcode of it.");
            MessageBox.Show("Be sure to disable Steam Cloud Save before opening the game.");
        }

        private void WriteSaveStory()
        {
            string Story = "";
            if (HasMoonspellDlc)
            {

            }
            saveStoryBox.Text = Story.Trim();
        }

        private void buyStartersCheck_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNgPlusNotifications();
        }

        void UpdateNgPlusNotifications()
        {
            string Text = "";
            if (!NgPlusAble)
            {
                Text = "You must receive the gift from the Directer before you can start New Game Plus.";
            }
            else
            {
                if (buyStartersCheck.Checked)
                {
                    Text = "Need to buy starters option is not Coop mode friendly. Disable it if you intend to play with friends.";
                }
            }
            ngplusnotificationtext.Text = Text;
        }

        private void newGamePlusCharacterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newGamePlusCharacterComboBox.SelectedIndex > 0)
            {
                if (newGamePlusCharacterComboBox.SelectedIndex == newGamePlusCharacterComboBox2.SelectedIndex ||
                    newGamePlusCharacterComboBox.SelectedIndex == newGamePlusCharacterComboBox3.SelectedIndex ||
                    newGamePlusCharacterComboBox.SelectedIndex == newGamePlusCharacterComboBox4.SelectedIndex)
                {
                    newGamePlusCharacterComboBox.SelectedIndex = 0;
                }
            }
            newGamePlusCharacterComboBox2.Enabled = newGamePlusCharacterComboBox.SelectedIndex > 0;
            if (!newGamePlusCharacterComboBox2.Enabled)
            {
                newGamePlusCharacterComboBox2.SelectedIndex = newGamePlusCharacterComboBox3.SelectedIndex = newGamePlusCharacterComboBox4.SelectedIndex = 0;
                newGamePlusCharacterComboBox3.Enabled = newGamePlusCharacterComboBox4.Enabled = false;
            }
        }

        private void newGamePlusCharacterComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newGamePlusCharacterComboBox2.SelectedIndex > 0)
            {
                if (newGamePlusCharacterComboBox2.SelectedIndex == newGamePlusCharacterComboBox.SelectedIndex ||
                    newGamePlusCharacterComboBox2.SelectedIndex == newGamePlusCharacterComboBox3.SelectedIndex ||
                    newGamePlusCharacterComboBox2.SelectedIndex == newGamePlusCharacterComboBox4.SelectedIndex)
                {
                    newGamePlusCharacterComboBox2.SelectedIndex = 0;
                }
            }
            newGamePlusCharacterComboBox3.Enabled = newGamePlusCharacterComboBox2.SelectedIndex > 0;
            if (!newGamePlusCharacterComboBox3.Enabled)
            {
                newGamePlusCharacterComboBox3.SelectedIndex = newGamePlusCharacterComboBox4.SelectedIndex = 0;
                newGamePlusCharacterComboBox4.Enabled = false;
            }
        }

        private void newGamePlusCharacterComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newGamePlusCharacterComboBox3.SelectedIndex > 0)
            {
                if (newGamePlusCharacterComboBox3.SelectedIndex == newGamePlusCharacterComboBox.SelectedIndex ||
                    newGamePlusCharacterComboBox3.SelectedIndex == newGamePlusCharacterComboBox2.SelectedIndex ||
                    newGamePlusCharacterComboBox3.SelectedIndex == newGamePlusCharacterComboBox4.SelectedIndex)
                {
                    newGamePlusCharacterComboBox3.SelectedIndex = 0;
                }
            }
            newGamePlusCharacterComboBox4.Enabled = newGamePlusCharacterComboBox3.SelectedIndex > 0;
            if (!newGamePlusCharacterComboBox4.Enabled)
                newGamePlusCharacterComboBox4.SelectedIndex = 0;
        }

        private void newGamePlusCharacterComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newGamePlusCharacterComboBox4.SelectedIndex > 0)
            {
                if (newGamePlusCharacterComboBox4.SelectedIndex == newGamePlusCharacterComboBox.SelectedIndex ||
                    newGamePlusCharacterComboBox4.SelectedIndex == newGamePlusCharacterComboBox3.SelectedIndex ||
                    newGamePlusCharacterComboBox4.SelectedIndex == newGamePlusCharacterComboBox2.SelectedIndex)
                {
                    newGamePlusCharacterComboBox4.SelectedIndex = 0;
                }
            }
        }
    }
}
