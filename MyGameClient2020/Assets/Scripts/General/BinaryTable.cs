using System.Collections.Generic;

namespace Common.General
{
    [System.Serializable]
    public class BinaryTable
    {
        public string tableName;
        private bool isNumberKey = true;
        private Dictionary<int, Dictionary<string, string>> numberKeyDict;
        private Dictionary<string, Dictionary<string, string>> stringKeyDict;

        public BinaryTable(string tableName, bool isNumberKey)
        {
            this.tableName = tableName;
            this.isNumberKey = isNumberKey;
            if (isNumberKey)
            {
                numberKeyDict = new Dictionary<int, Dictionary<string, string>>();
            }
            else
            {
                stringKeyDict = new Dictionary<string, Dictionary<string, string>>();
            }
        }
        public void ClearM()
        {
            if (null != numberKeyDict)
            {
                numberKeyDict.Clear();
            }
            if (null != stringKeyDict)
            {
                stringKeyDict.Clear();
            }
        }

        public void AddData(int lineKey, string key, string value)
        {
            if (numberKeyDict != null)
            {
                if (!numberKeyDict.ContainsKey(lineKey))
                {
                    numberKeyDict[lineKey] = new Dictionary<string, string>();
                }
                numberKeyDict[lineKey][key] = value;
            }
        }

        public void AddData(string lineKey, string key, string value)
        {
            if (stringKeyDict != null)
            {
                if (!stringKeyDict.ContainsKey(lineKey))
                {
                    stringKeyDict[lineKey] = new Dictionary<string, string>();
                }
                stringKeyDict[lineKey][key] = value;
            }
        }

        public Dictionary<string, string> GetLineData(int lineKey)
        {
            Dictionary<string, string> dict = null;
            if (numberKeyDict != null)
            {
                numberKeyDict.TryGetValue(lineKey, out dict);
            }
            return dict;
        }

        public Dictionary<string, string> GetLineData(string lineKey)
        {
            Dictionary<string, string> dict = null;
            if (stringKeyDict != null)
            {
                stringKeyDict.TryGetValue(lineKey, out dict);
            }
            return dict;
        }

        public string GetDataValue(int lineKey, string key)
        {
            Dictionary<string, string> lineData = GetLineData(lineKey);
            if (lineData != null)
            {
                string value = null;
                if (lineData.TryGetValue(key, out value))
                {
                    return value;
                }
            }
            return null;
        }

        public string GetDataValue(string lineKey, string key)
        {
            Dictionary<string, string> lineData = GetLineData(lineKey);
            if (lineData != null)
            {
                string value = null;
                if (lineData.TryGetValue(key, out value))
                {
                    return value;
                }
            }
            return null;
        }


        public Dictionary<int, Dictionary<string, string>>.KeyCollection GetAllNumKeys()
        {
            if (isNumberKey)
            {
                return numberKeyDict.Keys;
            }
            return null;
        }


        public Dictionary<string, Dictionary<string, string>>.KeyCollection GetAllStringKeys()
        {
            if (!isNumberKey)
            {
                return stringKeyDict.Keys;
            }
            return null;
        }

        public Dictionary<int, Dictionary<string, string>>.ValueCollection GetAllNumValues()
        {
            if (isNumberKey)
            {
                return numberKeyDict.Values;
            }
            return null;
        }


        public Dictionary<string, Dictionary<string, string>>.ValueCollection GetAllStringValues()
        {
            if (!isNumberKey)
            {
                return stringKeyDict.Values;
            }
            return null;
        }
        public Dictionary<int, Dictionary<string, string>>.KeyCollection GetTableKeys()
        {
            
            return numberKeyDict.Keys;
        }

        public static BinaryTable DeserializeTable(string path)
        {
            return Common.Deserialize<BinaryTable>(path);
        }

        public static void SerializeTable(BinaryTable table, string path)
        {
            Common.SaveData<BinaryTable>(table, path);
        }

    }
}