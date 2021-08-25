
using Manager;
using System.Collections.Generic;

namespace Common.General
{
    public class BinaryTableLoader : TSingleton<BinaryTableLoader>
    {
        private Dictionary<string, BinaryTable> binarytableDict;

        private BinaryTableLoader() { }
        public void Initialize()
        {
            LoadTables();
        }

        public void DestroyM()
        {
            if (null != binarytableDict)
            {
                foreach (var item in binarytableDict)
                {
                    item.Value.ClearM();
                }
                binarytableDict.Clear();
            }
        }

        void LoadTables()
        {
            if (binarytableDict == null)
            {
                binarytableDict = ResourcesManager.Instance.LoadBinaryTables();
            }
        }

        public BinaryTable GetBinaryTable(string tableName)
        {
            if (binarytableDict != null)
            {               
                BinaryTable bt;
                if (binarytableDict.TryGetValue(tableName, out bt))
                {
                    return bt;
                }
            }
            return null;
        }
    }
}
