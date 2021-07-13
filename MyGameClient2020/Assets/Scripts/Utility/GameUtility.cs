using UnityEngine;
using System.IO;
using System.Linq;
using XLua;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;


//[Hotfix]
public class GameUtility
{
    public const string AssetsFolderName = "Assets";

    public static string FormatToUnityPath(string path)
    {
        return path.Replace("\\", "/");
    }

    public static string FormatToSysFilePath(string path)
    {
        return path.Replace("/", "\\");
    }

    public static string FullPathToAssetPath(string full_path)
    {
        full_path = FormatToUnityPath(full_path);
        if (!full_path.StartsWith(Application.dataPath))
        {
            return null;
        }
        string ret_path = full_path.Replace(Application.dataPath, "");
        return AssetsFolderName + ret_path;
    }

    public static string GetFileExtension(string path)
    {
        return Path.GetExtension(path).ToLower();
    }

    public static string[] GetSpecifyFilesInFolder(string path, string[] extensions = null, bool exclude = false)
    {
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        if (extensions == null)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        }
        else if (exclude)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(f => !extensions.Contains(GetFileExtension(f))).ToArray();
        }
        else
        {
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(f => extensions.Contains(GetFileExtension(f))).ToArray();
        }
    }

    public static string[] GetSpecifyFilesInFolder(string path, string pattern)
    {
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        return Directory.GetFiles(path, pattern, SearchOption.AllDirectories);
    }

    public static string[] GetAllFilesInFolder(string path)
    {
        return GetSpecifyFilesInFolder(path);
    }

    public static string[] GetAllDirsInFolder(string path)
    {
        return Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
    }

    public static void CheckFileAndCreateDirWhenNeeded(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        FileInfo file_info = new FileInfo(filePath);
        DirectoryInfo dir_info = file_info.Directory;
        if (!dir_info.Exists)
        {
            Directory.CreateDirectory(dir_info.FullName);
        }
    }

    public static void CheckDirAndCreateWhenNeeded(string folderPath)
    {
        if (string.IsNullOrEmpty(folderPath))
        {
            return;
        }

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public static bool SafeWriteAllBytes(string outFile, byte[] outBytes)
    {
        try
        {
            if (string.IsNullOrEmpty(outFile))
            {
                return false;
            }

            CheckFileAndCreateDirWhenNeeded(outFile);
            if (File.Exists(outFile))
            {
                File.SetAttributes(outFile, FileAttributes.Normal);
            }
            File.WriteAllBytes(outFile, outBytes);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeWriteAllBytes failed! path = {0} with err = {1}", outFile, ex.Message));
            return false;
        }
    }

    public static bool SafeWriteAllLines(string outFile, string[] outLines)
    {
        try
        {
            if (string.IsNullOrEmpty(outFile))
            {
                return false;
            }

            CheckFileAndCreateDirWhenNeeded(outFile);
            if (File.Exists(outFile))
            {
                File.SetAttributes(outFile, FileAttributes.Normal);
            }
            File.WriteAllLines(outFile, outLines);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeWriteAllLines failed! path = {0} with err = {1}", outFile, ex.Message));
            return false;
        }
    }

    public static bool SafeWriteAllText(string outFile, string text)
    {
        try
        {
            if (string.IsNullOrEmpty(outFile))
            {
                return false;
            }

            CheckFileAndCreateDirWhenNeeded(outFile);
            if (File.Exists(outFile))
            {
                File.SetAttributes(outFile, FileAttributes.Normal);
            }
            File.WriteAllText(outFile, text);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeWriteAllText failed! path = {0} with err = {1}", outFile, ex.Message));
            return false;
        }
    }

    public static byte[] SafeReadAllBytes(string inFile)
    {
        try
        {
            if (string.IsNullOrEmpty(inFile))
            {
                return null;
            }

            if (!File.Exists(inFile))
            {
                return null;
            }

            File.SetAttributes(inFile, FileAttributes.Normal);
           // File.SetAttributes(inFile, FileAttributes.ReadOnly);
            return File.ReadAllBytes(inFile);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeReadAllBytes failed! path = {0} with err = {1}", inFile, ex.Message));
            return null;
        }
    }

    public static string[] SafeReadAllLines(string inFile)
    {
        try
        {
            if (string.IsNullOrEmpty(inFile))
            {
                return null;
            }

            if (!File.Exists(inFile))
            {
                return null;
            }

            File.SetAttributes(inFile, FileAttributes.Normal);
            return File.ReadAllLines(inFile);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeReadAllLines failed! path = {0} with err = {1}", inFile, ex.Message));
            return null;
        }
    }

    public static string SafeReadAllText(string inFile)
    {
        try
        {
            if (string.IsNullOrEmpty(inFile))
            {
                return null;
            }

            if (!File.Exists(inFile))
            {
                return null;
            }

            File.SetAttributes(inFile, FileAttributes.Normal);
            return File.ReadAllText(inFile);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeReadAllText failed! path = {0} with err = {1}", inFile, ex.Message));
            return null;
        }
    }

    public static void DeleteDirectory(string dirPath)
    {
        string[] files = Directory.GetFiles(dirPath);
        string[] dirs = Directory.GetDirectories(dirPath);

        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        foreach (string dir in dirs)
        {
            DeleteDirectory(dir);
        }

        Directory.Delete(dirPath, false);
    }

    public static bool SafeClearDir(string folderPath)
    {
        try
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                return true;
            }

            if (Directory.Exists(folderPath))
            {
                DeleteDirectory(folderPath);
            }
            Directory.CreateDirectory(folderPath);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeClearDir failed! path = {0} with err = {1}", folderPath, ex.Message));
            return false;
        }
    }

    public static bool SafeDeleteDir(string folderPath)
    {
        try
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                return true;
            }

            if (Directory.Exists(folderPath))
            {
                DeleteDirectory(folderPath);
            }
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeDeleteDir failed! path = {0} with err: {1}", folderPath, ex.Message));
            return false;
        }
    }

    public static bool SafeDeleteFile(string filePath)
    {
        try
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return true;
            }

            if (!File.Exists(filePath))
            {
                return true;
            }
            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeDeleteFile failed! path = {0} with err: {1}", filePath, ex.Message));
            return false;
        }
    }

    public static bool SafeRenameFile(string sourceFileName, string destFileName)
    {
        try
        {
            if (string.IsNullOrEmpty(sourceFileName))
            {
                return false;
            }

            if (!File.Exists(sourceFileName))
            {
                return true;
            }
            SafeDeleteFile(destFileName);
            File.SetAttributes(sourceFileName, FileAttributes.Normal);
            File.Move(sourceFileName, destFileName);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeRenameFile failed! path = {0} with err: {1}", sourceFileName, ex.Message));
            return false;
        }
    }

    public static bool SafeCopyFile(string fromFile, string toFile)
    {
        try
        {
            if (string.IsNullOrEmpty(fromFile))
            {
                return false;
            }

            if (!File.Exists(fromFile))
            {
                return false;
            }
            CheckFileAndCreateDirWhenNeeded(toFile);
            SafeDeleteFile(toFile);
            File.Copy(fromFile, toFile, true);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("SafeCopyFile failed! formFile = {0}, toFile = {1}, with err = {2}",
                fromFile, toFile, ex.Message));
            return false;
        }
    }


    public static int Int(object o)
    {
        return Convert.ToInt32(o);
    }

    public static float Float(object o)
    {
        return (float)Math.Round(Convert.ToSingle(o), 2);
    }

    public static long Long(object o)
    {
        return Convert.ToInt64(o);
    }

    public static int Random(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static float Random(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static string Uid(string uid)
    {
        int position = uid.LastIndexOf('_');
        return uid.Remove(0, position + 1);
    }

    public static long GetTime()
    {
        TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
        return (long)ts.TotalMilliseconds;
    }

    /// <summary>
    /// 搜索子物体组件-GameObject版
    /// </summary>
    public static T Get<T>(GameObject go, string subnode) where T : Component
    {
        if (go != null)
        {
            Transform sub = go.transform.Find(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 搜索子物体组件-Transform版
    /// </summary>
    public static T Get<T>(Transform go, string subnode) where T : Component
    {
        if (go != null)
        {
            Transform sub = go.Find(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 搜索子物体组件-Component版
    /// </summary>
    public static T Get<T>(Component go, string subnode) where T : Component
    {
        return go.transform.Find(subnode).GetComponent<T>();
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    public static T Add<T>(GameObject go) where T : Component
    {
        if (go != null)
        {
            T[] ts = go.GetComponents<T>();
            for (int i = 0; i < ts.Length; i++)
            {
                if (ts[i] != null) GameObject.Destroy(ts[i]);
            }
            return go.gameObject.AddComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    public static T Add<T>(Transform go) where T : Component
    {
        return Add<T>(go.gameObject);
    }

    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(GameObject go, string subnode)
    {
        return Child(go.transform, subnode);
    }

    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(Transform go, string subnode)
    {
        Transform tran = go.Find(subnode);
        if (tran == null) return null;
        return tran.gameObject;
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(GameObject go, string subnode)
    {
        return Peer(go.transform, subnode);
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(Transform go, string subnode)
    {
        Transform tran = go.parent.Find(subnode);
        if (tran == null) return null;
        return tran.gameObject;
    }

    /// <summary>
    /// 计算字符串的MD5值
    /// </summary>
    public static string md5(string source)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
        byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
        md5.Clear();

        string destString = "";
        for (int i = 0; i < md5Data.Length; i++)
        {
            destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
        }
        destString = destString.PadLeft(32, '0');
        return destString;
    }

    /// <summary>
    /// 计算文件的MD5值
    /// </summary>
    public static string md5file(string file)
    {
        try
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("md5file() fail, error:" + ex.Message);
        }
    }

    /// <summary>
    /// 清除所有子节点
    /// </summary>
    public static void ClearChild(Transform go)
    {
        if (go == null) return;
        for (int i = go.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(go.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 清理内存
    /// </summary>
    public static void ClearMemory()
    {
        GC.Collect(); Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 取得数据存放目录
    /// </summary>
    public static string DataPath
    {
        get
        {
          string game = AppConst.AppName.ToLower();
#if UNITY_EDITOR
            return "c:/" + game + "/";
#endif
        
            if (Application.isMobilePlatform)
            {
                return Application.persistentDataPath + "/" + game + "/";
            }
            //if (AppConst.DebugMode)
            //{
            //    return Application.dataPath + "/" + AppConst.AssetDir + "/";
            //}
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                int i = Application.dataPath.LastIndexOf('/');
                return Application.dataPath.Substring(0, i + 1) + game + "/";
            }
            return "c:/" + game + "/";
        }
    }

    public static string GetRelativePath()
    {
        if (Application.isEditor)
            return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/") + "/Assets/" + AppConst.AssetDir + "/";
        else if (Application.isMobilePlatform || Application.isConsolePlatform)
            return "file:///" + DataPath;
        else // For standalone player.
            return "file://" + Application.streamingAssetsPath + "/";
    }

    /// <summary>
    /// 取得行文本
    /// </summary>
    public static string GetFileText(string path)
    {
        return File.ReadAllText(path);
    }

    /// <summary>
    /// 网络可用
    /// </summary>
    public static bool NetAvailable
    {
        get
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }

    /// <summary>
    /// 是否是无线
    /// </summary>
    public static bool IsWifi
    {
        get
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }

    /// <summary>
    /// 应用程序内容路径
    /// </summary>
    public static string AppContentPath()
    {
        string path = string.Empty;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                path = "jar:file://" + Application.dataPath + "!/assets/";
                break;
            case RuntimePlatform.IPhonePlayer:
                path = Application.dataPath + "/Raw/";
                break;
            default:
                path = Application.dataPath + "/" + AppConst.AssetDir + "/";
                break;
        }
        return path;
    }

    public static string AppBuildABPath()
    {
        string path = string.Empty;
        path = Application.dataPath + "/" + AppConst.AssetDir + "/";
        return path;
    }

    public static void Log(string str)
    {
        Debug.Log(str);
    }

    public static void LogWarning(string str)
    {
        Debug.LogWarning(str);
    }

    public static void LogError(string str)
    {
        Debug.LogError(str);
    }

    /// <summary>
    /// 读取MD5文件
    /// </summary>
    public static Dictionary<string, string> ReadMD5File(string FilePath_)
    {
        Dictionary<string, string> _itemDictionary = new Dictionary<string, string>();
        FileStream _fs = new FileStream(FilePath_, FileMode.Open, FileAccess.Read);
        StreamReader _sr = new StreamReader(_fs);
        string[] itempStrings = _sr.ReadToEnd().Split('\n');
        for (int i = 0; i < itempStrings.Length - 1; i++)
        {
            if (itempStrings[i] == null)
                continue;
            string[] _itempSplit = itempStrings[i].Split('|');
            _itemDictionary.Add(_itempSplit[0], _itempSplit[1].Trim());
        }
        _sr.Close();
        _fs.Close();
        return _itemDictionary;
    }

    public static Dictionary<string, string> ReadMD5FileWithOutSize(string FilePath_)
    {
        Dictionary<string, string> _itemDictionary = new Dictionary<string, string>();
        FileStream _fs = new FileStream(FilePath_, FileMode.Open, FileAccess.Read);
        StreamReader _sr = new StreamReader(_fs);
        string[] itempStrings = _sr.ReadToEnd().Split('\n');
        for (int i = 0; i < itempStrings.Length - 1; i++)
        {
            if (itempStrings[i] == null)
                continue;
            string[] _itempSplit = itempStrings[i].Split('|');
            string[] _tempNewList = _itempSplit[1].Trim().Split(';');//把MD5和Lenght数据分割显示
            _itemDictionary.Add(_itempSplit[0], _tempNewList[0]);
            //_itemDictionary.Add(_itempSplit[0], _itempSplit[1].Trim());
        }
        _sr.Close();
        _fs.Close();
        return _itemDictionary;
    }

    //计算两个版本的差异文件大小总和
    public static long GetDifferentVerFilesSizeM(string newPathFile_, string oldPathFile_)
    {
        long tempSize = 0;
        if (!File.Exists(oldPathFile_))//老文件不存在，则返回所有新文件大小
        {
            Dictionary<string, string> _newListDic = ReadMD5File(newPathFile_);
            foreach (var item in _newListDic.Keys)
            {
                string[] _tempNewList = _newListDic[item].Split(';');//把MD5和Lenght数据分割显示
                tempSize += long.Parse(_tempNewList[1]);
            }
        }
        else//老文件存在，则比对两个文件，找出差异的文件，计算差异文件
        {
            Dictionary<string, string> _newListDic = ReadMD5File(newPathFile_);
            Dictionary<string, string> _oldListDic = ReadMD5File(oldPathFile_);

            foreach (string item in _newListDic.Keys)
            {
                if (_oldListDic.ContainsKey(item))//如果当前文件在 新旧版本中都存在，则比对MD5
                {
                    string[] _tempNewList = _newListDic[item].Split(';');//把MD5和Lenght数据分割显示
                    string[] _tempOldList = _oldListDic[item].Split(';');
                    if (_tempNewList[0] != _tempOldList[0])//MD5比对
                    {
                        tempSize += long.Parse(_tempNewList[1]);
                    }
                    else
                        continue;
                }
                else//新增的文件
                {
                    string[] _tempNewList = _newListDic[item].Split(';');
                    tempSize += long.Parse(_tempNewList[1]);
                }
            }
        }
        return tempSize/1024;
    }

    /// <summary>
    /// 获取两个不同版本的差异文件列表
    /// </summary>
    public static List<string> GetDifferentVerFiles(string newPathFile_, string oldPathFile_,bool addMainFest_ = true)
    {
        List<string> difList = new List<string>();
        if (!File.Exists(oldPathFile_))//老文件不存在，则返回所有新文件
        {
            Dictionary<string, string> _newListDic = ReadMD5File(newPathFile_);
            foreach (string item in _newListDic.Keys)
            {
                difList.Add(item);
                //difList.Add(item + ".manifest");
            }
        }
        else//老文件存在，则比对两个文件，找出差异的文件，返回
        {
            Dictionary<string, string> _newListDic = ReadMD5File(newPathFile_);
            Dictionary<string, string> _oldListDic = ReadMD5File(oldPathFile_);
            foreach (string item in _newListDic.Keys)
            {
                if (_oldListDic.ContainsKey(item))
                {
                    string[] _tempNewList = _newListDic[item].Split(';');//把MD5和Lenght数据分割显示
                    string[] _tempOldList = _oldListDic[item].Split(';');
                    if(_tempNewList[0] != _tempOldList[0])//MD5比对
                   // if (_newListDic[item] != _oldListDic[item])
                    {
                        difList.Add(item);
                        if (addMainFest_) difList.Add(item + ".manifest");
                    }
                    else
                        continue;
                }
                else//新增的文件
                {
                    if (!difList.Contains(item))
                    {
                        difList.Add(item);
                        if (addMainFest_) difList.Add(item + ".manifest");
                    }
                }
            }
        }

        difList.Add("files.txt");//最后尾部追加核心文件
        difList.Add("ver.txt");//
        return difList;
    }

    public static List<string> GetDifferentVerFiles_Test(string newPathFile_, string oldPathFile_)
    {
        List<string> difList = new List<string>();
        if (!File.Exists(oldPathFile_))//老文件不存在，则返回所有新文件
        {
            Dictionary<string, string> _newListDic = ReadMD5File(newPathFile_);
            foreach (string item in _newListDic.Keys)
            {
                difList.Add(item);
                //difList.Add(item + ".manifest");
            }
        }
        else//老文件存在，则比对两个文件，找出差异的文件，返回
        {
            Dictionary<string, string> _newListDic = ReadMD5File(newPathFile_);
            Dictionary<string, string> _oldListDic = ReadMD5File(oldPathFile_);
            foreach (string item in _newListDic.Keys)
            {
                if (!_oldListDic.ContainsKey(item))
                {
                    Debug.Log("--------存在差异文件--111------------"+ item);

                }
            }

            foreach (string item in _oldListDic.Keys)
            {
                if (!_newListDic.ContainsKey(item))
                {
                    Debug.Log("--------存在差异文件--222------------" + item);

                }
            }
        }

        difList.Add("files.txt");//最后尾部追加核心文件
        return difList;
    }
    public static int ReadVerFile(string FileName_)
    {
        if (!File.Exists(FileName_))
            return 0;

        // Dictionary<string, string> _itemDictionary = new Dictionary<string, string>();
        FileStream _fs = new FileStream(FileName_, FileMode.Open, FileAccess.Read);
        StreamReader _sr = new StreamReader(_fs);
        string[] itempStrings = _sr.ReadToEnd().Split('\n');
        int tempVer = 0;
        for (int i = 0; i < itempStrings.Length; i++)
        {
            if (itempStrings[i] == null)
                continue;
            string[] _itempSplit = itempStrings[i].Split('|');
            if ("version" == _itempSplit[0])
                tempVer = int.Parse(_itempSplit[1].Trim());
            //  _itemDictionary.Add(_itempSplit[0], _itempSplit[1].Trim());
        }

        _sr.Close();
        _fs.Close();
        return tempVer;

    }

}

//#if UNITY_EDITOR
//public static class GameUtilityExporter
//{
//    [LuaCallCSharp]
//    public static List<Type> LuaCallCSharp = new List<Type>(){
//            typeof(GameUtility),
//        };
//}
//#endif