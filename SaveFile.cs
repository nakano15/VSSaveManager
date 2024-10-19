using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace VSSaveManager
{
    public class SaveFile
    {
        public JObject SaveObject = null;

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
            foreach (var sObject in SaveObject)
            {
                string name = sObject.Key;
                JToken value = sObject.Value;
                if (name == "saveDate")
                {
                } else {
                    if(value.Type == JTokenType.Float)
                    {
                        string a = ((string)value) as string;
                        if (a.Split('.')[0].Length > 7)
                        {
                            value = ((float)value).ToString("E08");
                        }
                        SaveObject[name] = value;
                    }
                }
            }
            SaveObject["checksum"] = "";
            using (SHA256 crypto = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(Minify());
                byte[] NewHashByte = crypto.ComputeHash(bytes);
                SaveObject["checksum"] = BitConverter.ToString(NewHashByte).Replace("-", "").ToLower();
                
                /*string Json = SaveObject.ToString();
                using (MemoryStream stream = new MemoryStream())
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(GetJsonString());
                        byte[] NewHashByte = crypto.ComputeHash(stream);
                        SaveObject["checksum"] = BitConverter.ToString(NewHashByte).Replace("-", "").ToLower();
                    }
                }*/
                /*string NewHash = "";
                foreach(byte b in NewHashByte)
                {
                    NewHash += string.Format("{0:x2}", b);
                }
                SaveObject["checksum"] = NewHash;*/
            }
        }

        public string GetJsonString()
        {
            return SaveObject.ToString().Replace("\"checksum\": \"", "\"checksum\":\"");
        }
        public string Minify()
        {
            return JsonConvert.SerializeObject(SaveObject);
        }
    }
}
