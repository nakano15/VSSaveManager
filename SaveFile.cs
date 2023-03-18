using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VSSaveManager
{
    public class SaveFile
    {
        public JObject SaveObject = null;
        public string Profile = "";

        public string SelectedCharacter
        {
            get
            {
                return SaveObject["SelectedCharacter"].Value<string>();
            }
            set
            {
                SaveObject["SelectedCharacter"] = value;
            }
        }
        public string SelectedStage
        {
            get
            {
                return SaveObject["SelectedStage"].Value<string>();
            }
            set
            {
                SaveObject["SelectedStage"] = value;
            }
        }
        public string[] Achievements
        {
            get
            {
                return SaveObject["Achievements"].Values<string>().ToArray();
            }
        }

        public SaveFile(string json)
        {
            SaveObject = JObject.Parse(json);
        }

        public SaveFile(JObject jobject)
        {
            SaveObject = jobject;
        }

        public void UpdateChecksum()
        {
            SaveObject["checksum"] = "";
            using (SHA256 crypto = SHA256.Create())
            {
                byte[] NewHashByte = crypto.ComputeHash(Encoding.ASCII.GetBytes(SaveObject.ToString()));
                string NewHash = "";
                foreach(byte b in NewHashByte)
                {
                    NewHash += string.Format("{0:x2}", b);
                }
                SaveObject["checksum"] = NewHash;
            }
        }
    }
}
