using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

/// <summary>
/// AB打包处理模块
/// </summary>
public class AssetBundleEditor : EditorWindow
{


    /// <summary>
    /// 数据目录
    /// </summary>
    static string AppDataPath
    {
        get
        {
            return Application.dataPath.ToLower();
        }
    }

    //记录版本信息的文件目录位置
    private static int s_firstVerNum = 10000;
    private static string s_VerLogPath = AppDataPath + "/GamePackLog/VerLog/V";
    private static string s_StreamingAssetsFiles = AppDataPath + "/StreamingAssets/files.txt";
    private static string s_StreamingAssetsFiles2 = AppDataPath + "/StreamingAssets/md5files.txt";
    //最终打包好的AB资源目录的位置
    private static string GetABPath
    {
        get
        {
#if UNITY_IOS
        return GetMD5FilePath()+"/AssetsBundles/ios/V"+GameUtility.ReadVerFile(GameUtility.AppBuildABPath() + AppConst.AssetVerFileName);
#elif UNITY_ANDROID
            return GetMD5FilePath() + "/AssetsBundles/android/V" + GameUtility.ReadVerFile(GameUtility.AppBuildABPath() + AppConst.AssetVerFileName);
#elif UNITY_STANDALONE_WIN
            return GetMD5FilePath() + "/AssetsBundles/windows/V" + GameUtility.ReadVerFile(GameUtility.AppBuildABPath() + AppConst.AssetVerFileName);
#endif
        }
    }

    private static string GetABMD5Path
    {
        get
        {
#if UNITY_IOS
        return GetMD5FilePath()+"/AssetsBundles/ios/MD5/V"+GameUtility.ReadVerFile(GameUtility.AppBuildABPath() + AppConst.AssetVerFileName);
#elif UNITY_ANDROID
            return GetMD5FilePath() + "/AssetsBundles/android/MD5/V" + GameUtility.ReadVerFile(GameUtility.AppBuildABPath() + AppConst.AssetVerFileName);
#elif UNITY_STANDALONE_WIN
            return GetMD5FilePath()+ "/AssetsBundles/windows/MD5/V" + GameUtility.ReadVerFile(GameUtility.AppBuildABPath() + AppConst.AssetVerFileName);
#endif
        }
    }

    private static string GetMD5FilePath()
    {
        string tempFileName = Application.dataPath;
        string item = tempFileName.Substring(0, tempFileName.LastIndexOf('/')).Replace(AppDataPath, "");
        return item;
    }

    //打包本地测试资源
    //本地资源发生变化，在StreamingAssets目录下重新生成AB文件
    [MenuItem("AssetsBundle/BuildABDebug")]
    static void BuildABDebug()
    {
        BuildTarget tempTarget = BuildTarget.Android ;

        string tempFileName = Application.dataPath;
        string item = tempFileName.Substring(0, tempFileName.LastIndexOf('/')).Replace(AppDataPath + "/", "");
        Debug.Log("----------Application.dataPath-----------"+ Application.dataPath);
        Debug.Log("----------Application.persistentDataPath-----------" + Application.persistentDataPath);
        Debug.Log("-----------Application.NewPaht-------------------------------"+ item);
        Debug.Log("-----------Application.streamingAssetsPath-------------------------------" + Application.streamingAssetsPath);

        //  BuildTestResource(tempTarget, true);
        CopyVideoFilesM();

    }



    //static void TestFindMD5()
    //{
    //    string tempUpdateFileName = GetVerLogFileName();
    //    {
    //        string tempOldFileName = GetSafeLastVerNameM();
    //        if (tempOldFileName != null)
    //        {
    //            List<string> tempList = GameUtility.GetDifferentVerFiles(tempUpdateFileName, tempOldFileName);
    //            //return tempMD5;
    //        }
    //    }
    //}
    //打包本地测试资源
    //本地资源发生变化，在StreamingAssets目录下重新生成AB文件
    [MenuItem("AssetsBundle/BuildLuaAb")]
    static void BuildLuaAb()
    {
        BuildTarget tempTarget = BuildTarget.Android;
        BuildLuaResources(tempTarget, false);
        //EditorUtility.DisplayDialog("更新提示", "本地资源打包完成！", "知道了");
    }

    //打包Windows正式资源
    //会生成升级资源包
    [MenuItem("AssetsBundle/Build Windows Resource")]
    static void BuildWindowsResource()
    {
        BuildTarget tempTarget = BuildTarget.StandaloneWindows;
        BuildAssetResource(tempTarget, true);
        //EditorUtility.DisplayDialog("更新提示", "资源升级包已生成！", "知道了");
    }

    //打包Android正式资源
    //会生成升级资源包
    [MenuItem("AssetsBundle/Build Android Resource")]
    static void BuildAndroidResource()
    {
        BuildTarget tempTarget = BuildTarget.Android;
        BuildAssetResource(tempTarget, true);
        //EditorUtility.DisplayDialog("更新提示", "资源升级包已生成！", "知道了");
    }

    //打包iPhone正式资源
    //会生成升级资源包
    [MenuItem("AssetsBundle/Build iPhone Resource")]
    static void BuildiPhoneResource()
    {
        BuildTarget tempTarget;
#if UNITY_5
        tempTarget = BuildTarget.iOS;
#else
        tempTarget = BuildTarget.iOS;
#endif

        BuildAssetResource(tempTarget, false);
        EditorUtility.DisplayDialog("更新提示", "资源升级包已生成！", "知道了");
    }

    [MenuItem("AssetsBundle/Clear PersionDataPath (沙盒目录)")]
    static void ClearPersionDataPath()
    {
        if (Directory.Exists(GameUtility.DataPath))
            Directory.Delete(GameUtility.DataPath, true);
    }
    [MenuItem("AssetsBundle/CopyLuaToData")]
    static void CopyLuaToData()
    {
        //Lua文件复制到打包目录
        CopyLuaFilesToGameAssets();
    }

    //检查AB文件名是否安全
    [MenuItem("AssetsBundle/CheckSafeAbName")]
    static void CheckSafeAbName()
    {
        tempABNameDic.Clear();
        List<AssetBundleBuild> tempAbBuildList = new List<AssetBundleBuild>();
        InitAbDataM(ref tempAbBuildList);
    }

    //只生成MD5文件
    [MenuItem("AssetsBundle/ReBuildMD5File")]
    static void ReBuildMD5File()
    {
        string tempStreamPath = Application.streamingAssetsPath;
        //资源目录下生成MD5文件信息： files.txt
        BuildFileIndexOld(tempStreamPath, s_StreamingAssetsFiles, false);
        //Editor目录下生成MD5文件信息：files.txt.
        SaveVerLogFileFromStreamFiles();
        ////生成升级包AB文件
        BuildUpdatePackage(GetABPath);
    }

    [MenuItem("AssetsBundle/TestBuildScene")]
    static void TestBuildScene()
    {
        //场景打包
        string tempScenePath = "Assets/GameAssets/Scenes";
        string tempOutPath = "StreamingAssets";
        BuildTarget tempTarget = BuildTarget.Android;
        BuildScene(tempScenePath, AppConst.ExtName, tempOutPath, tempTarget);
    }


    [MenuItem("AssetsBundle/FindAudioListener")]
    static void FindAudioListener()
    {
        GameObject tempObj = Selection.activeGameObject;
        AudioListener temp = tempObj.transform.GetComponentInChildren<AudioListener>(true);
        if (temp)
        {
            Debug.Log(temp.transform.name);
        }
        else
        {
            Debug.Log("---------NotFind------");
        }
    }

    [MenuItem("AssetsBundle/FindMeshRenderer")]
    static void FindMeshRenderer()
    {
        GameObject tempObj = Selection.activeGameObject;
        MeshRenderer temp = tempObj.transform.GetComponentInChildren<MeshRenderer>(true);
        if (temp)
        {
            Debug.Log(temp.transform.name);
        }
        else
        {
            Debug.Log("---------NotFind------");
        }
    }

    static void InitAbDataM(ref List<AssetBundleBuild> fileList_)
    {
        //生成新的文件
        string tempAssetPath = Path.Combine(AppDataPath, AppConst.GameAssetsFolderName);
        tempAssetPath = GameUtility.FormatToUnityPath(tempAssetPath);

        //遍历资源目录，计算当前目录下需要打包的文件信息
        List<string> tempFileList = new List<string>();
        CalcAbFileList(tempAssetPath, ref tempFileList);

        //构建剩余目录AB包信息
        BuildAbByFiles(tempFileList, ref fileList_);

    }

    static Dictionary<string, string> tempABNameDic = new Dictionary<string, string>();
    static void CheckSameName(string newName_, string path_, List<AssetBundleBuild> AbBuildList_)
    {
        foreach (var item in AbBuildList_)
        {
            if (item.assetBundleName == newName_)
            {
                // Debug.Log("----------begin------------" + newName_);
                // Debug.Log("--------存在同名的AB包-----------" + path_ +"/"+ newName_+"---new---");
                //Debug.Log("--------存在同名的AB包-----------" + tempABNameDic[newName_] + "/" + newName_ + "---old---");
                // Debug.Log("----------end------------" + newName_);
                Debug.Log(path_ + "@" + path_ + "/" + newName_);
                Debug.Log(tempABNameDic[newName_] + "@" + tempABNameDic[newName_] + "/" + newName_);
            }
        }
        if (!tempABNameDic.ContainsKey(newName_))
        {
            tempABNameDic[newName_] = path_;
        }
    }


    static void BuildLuaResources(BuildTarget target_, bool isRelease_)
    {
#if USE_PACKAGE
        string tempStreamPath = Application.streamingAssetsPath;
        //Lua文件复制到打包目录
        CopyLuaFilesToGameAssets();

        //根据需要打包的文件信息，计算出AB文件信息
        List<AssetBundleBuild> tempAbBuildList = new List<AssetBundleBuild>();

        //lua文件只打一个包
        string tempLuaPath = AppConst.resPath_Asset + AppConst.LuaAbAssetName;
        BuildAbByPath(AppConst.LuaAbAssetName, tempLuaPath, "*.bytes", ref tempAbBuildList);
        BuildPipeline.SetAssetBundleEncryptKey("yulong1688yulong");
        //打包打包
        //BuildPipeline.BuildAssetBundles(tempStreamPath, tempAbBuildList.ToArray(), BuildAssetBundleOptions.None, target_);//打包AB EditorUserBuildSettings.activeBuildTarget
        BuildPipeline.BuildAssetBundles(tempStreamPath, tempAbBuildList.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.EnableProtection, target_);
        //如果是Release版本，则需要生成新的AB升级包
        if (isRelease_)
        {
            //资源目录下生成MD5文件信息： files.txt
            BuildFileIndexOld(tempStreamPath, s_StreamingAssetsFiles, false);
            //Editor目录下生成MD5文件信息：files.txt
            SaveVerLogFileFromStreamFiles();
            //生成升级包AB文件
            BuildUpdatePackage(GetABPath);
        }
        else
        {
            EditorUtility.DisplayDialog("更新提示", "资源已经完成打包！", "知道了");
        }
#endif
    }

    //测试接口  
    static void BuildTestResource(BuildTarget target_, bool isRelease_)
    {
 #if USE_PACKAGE
        ////删除老的文件
        string tempStreamPath = Application.streamingAssetsPath;
        if (Directory.Exists(tempStreamPath))
            Directory.Delete(tempStreamPath, true);
        Directory.CreateDirectory(tempStreamPath);
        AssetDatabase.Refresh();
        string path = Application.dataPath + "/ver.txt";
        string psth2 = tempStreamPath + "/ver.txt";
        if (!System.IO.Directory.Exists(path))
        {
            File.Copy(path, psth2, true);
        }
        else
        {
            Debug.LogError("<<=======ver 文件不存在=========>>");
        }

        //生成新的文件
        string tempAssetPath = Path.Combine(AppDataPath, AppConst.GameAssetsFolderName);
        tempAssetPath = GameUtility.FormatToUnityPath(tempAssetPath);

        //遍历资源目录，计算当前目录下需要打包的文件信息
        List<string> tempFileList = new List<string>();
        CalcAbFileList(tempAssetPath, ref tempFileList);


        //根据需要打包的文件信息，计算出AB文件信息
        List<AssetBundleBuild> tempAbBuildList = new List<AssetBundleBuild>();

        //构建剩余目录AB包信息
        BuildAbTestByFiles(tempFileList, ref tempAbBuildList);

        BuildPipeline.SetAssetBundleEncryptKey("yulong1688yulong");
        //打包打包
        BuildPipeline.BuildAssetBundles(tempStreamPath, tempAbBuildList.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.EnableProtection | BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.AppendHashToAssetBundleName, target_);

        //如果是Release版本，则需要生成新的AB升级包
        if (isRelease_)
        {
            //资源目录下生成MD5文件信息： files.txt
            BuildFileIndexOld(tempStreamPath, s_StreamingAssetsFiles,false);
            //Editor目录下生成MD5文件信息：files.txt
            // BuildFileIndex(tempStreamPath, GetUpdateFileName());
            SaveVerLogFileFromStreamFiles();
            //生成升级包AB文件
            BuildUpdatePackage(GetABPath);
        }
        else
        {
            EditorUtility.DisplayDialog("更新提示", "资源已经完成打包！", "知道了");
        }
#endif
    }

    static void SaveVerLogFileFromStreamFiles()
    {
        string tempLogFileName = GetVerLogFileName();
        if (File.Exists(tempLogFileName)) File.Delete(tempLogFileName);

       File.Copy(s_StreamingAssetsFiles, tempLogFileName);
    }
    //
    static void BuildAssetResource(BuildTarget target_, bool isRelease_)
    {
#if USE_PACKAGE
        ////删除老的文件
        string tempStreamPath = Application.streamingAssetsPath;
        if (Directory.Exists(tempStreamPath))
            Directory.Delete(tempStreamPath, true);
        Directory.CreateDirectory(tempStreamPath);
        AssetDatabase.Refresh();
        string path = Application.dataPath + "/ver.txt";
        string psth2 = tempStreamPath + "/ver.txt";
        if (!System.IO.Directory.Exists(path))
        {
            File.Copy(path, psth2, true);
        }
        else
        {
            Debug.LogError("<<=======ver 文件不存在=========>>");
        }
        //Lua文件复制到打包目录
        CopyLuaFilesToGameAssets();

        //生成新的文件
        string tempAssetPath = Path.Combine(AppDataPath, AppConst.GameAssetsFolderName);
        tempAssetPath = GameUtility.FormatToUnityPath(tempAssetPath);

        //遍历资源目录，计算当前目录下需要打包的文件信息
        List<string> tempFileList = new List<string>();
        CalcAbFileList(tempAssetPath, ref tempFileList);


        //根据需要打包的文件信息，计算出AB文件信息
        List<AssetBundleBuild> tempAbBuildList = new List<AssetBundleBuild>();

        //lua文件只打一个包
        string tempLuaPath = AppConst.resPath_Asset + AppConst.LuaAbAssetName;
        BuildAbByPath(AppConst.LuaAbAssetName, tempLuaPath, "*.bytes", ref tempAbBuildList);

        //shader文件只打一个包
        string tempShaderPath = AppConst.resPath_Asset + AppConst.ShaderAbAssetName;
        BuildAbByPath(AppConst.ShaderAbAssetName, tempShaderPath, "*.shader", ref tempAbBuildList);

        //构建剩余目录AB包信息
        BuildAbByFiles(tempFileList, ref tempAbBuildList);
        BuildPipeline.SetAssetBundleEncryptKey("yulong1688yulong");

        //先生成 有MD5码的AB文件
        string tempMD5Path = GetABMD5Path;
        if (Directory.Exists(tempMD5Path))
        {
            Directory.Delete(tempMD5Path, true);
        }
        Directory.CreateDirectory(tempMD5Path);

        BuildPipeline.BuildAssetBundles(tempMD5Path, tempAbBuildList.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.EnableProtection | BuildAssetBundleOptions.AppendHashToAssetBundleName, target_);

        if (isRelease_)//生成MD5文件
        {
            BuildFileIndex(tempMD5Path, s_StreamingAssetsFiles2);
            Directory.Delete(tempMD5Path, true);//用完删除
        }

         //再生成正式的AB文件
       BuildPipeline.BuildAssetBundles(tempStreamPath, tempAbBuildList.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.EnableProtection, target_);

        //场景打包 暂时注释 ljs
        string tempScenePath = "Assets/GameAssets/Scenes";
        string tempOutPath = "StreamingAssets";
        BuildScene(tempScenePath, AppConst.ExtName, tempOutPath, target_);

        CopyVideoFilesM();
        //如果是Release版本，则需要生成新的AB升级包
        if (isRelease_)
        {
            //资源目录下生成MD5文件信息： files.txt
            //BuildFileIndexOld(tempStreamPath, s_StreamingAssetsFiles, false);//老方案，切勿删除
            BuildFileIndexFromMd5(tempStreamPath, s_StreamingAssetsFiles, s_StreamingAssetsFiles2, false);

            //GameUtility.GetDifferentVerFiles_Test(s_StreamingAssetsFiles, s_StreamingAssetsFiles2);
            //Files.txt保存到升级目录
            SaveVerLogFileFromStreamFiles();
            //生成升级包AB文件
            BuildUpdatePackage(GetABPath);
        }
        else
        {
            EditorUtility.DisplayDialog("更新提示", "资源已经完成打包！", "知道了");
        }
#endif
    }

    static void CopyVideoFilesM()
    {
        string tempSourcesPath = AppDataPath + "/"+ AppConst.GameAssetsFolderName+ "/" + AppConst.resPath_Video;
        string tempDestPath = AppDataPath + "/" + AppConst.AssetDir;
        string[] subFiles = Directory.GetFiles(tempSourcesPath);
        foreach (string subFile in subFiles)
        {
            //Debug.Log("-------文件名称---------------"+ subFile);
            string ext = Path.GetExtension(subFile);
            if (ext.Equals(".meta"))
                continue;
           // string tempFile = tempSourcesPath + subFile;
          
            string tempFileName = subFile.Replace('\\', '/');
            string item = tempFileName.Substring(tempFileName.LastIndexOf('/')+1, tempFileName.Length- tempFileName.LastIndexOf('/')-1);
            string tempNewFilePath = tempDestPath +"/" +item;
            //Debug.Log("---------------item---------------------" + item);
            //Debug.Log("---------------OldFileName---------------------"+ subFile);
           // Debug.Log("---------------NewFileName---------------------" + tempNewFilePath);
            File.Copy(subFile, tempNewFilePath);
        }
    }

    static int GetCurVerNumber()
    {
        return GameUtility.ReadVerFile(GameUtility.AppContentPath() + AppConst.AssetVerFileName);
    }

    static string GetVerLogFileName()
    {
        int tempCurVerNumber = GetCurVerNumber();
        return s_VerLogPath + tempCurVerNumber.ToString() + ".txt";
    }

    /// <summary>
    /// 以目录名为AB包名，进行整理
    /// </summary>
    static void AddBundleDirBuild(string bundleName_, string pattern_, string path_, ref List<AssetBundleBuild> AbBuildList_, SearchOption option_ = SearchOption.TopDirectoryOnly)
    {
        if ("backyard_anims" == bundleName_ || "referenceassets" == bundleName_)
            return;
        // string[] files = null;
        List<string> tempFiles = new List<string>();

        if ("*" == pattern_)//所有文件
        {
            string[] files = Directory.GetFiles(path_, pattern_, option_);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].EndsWith(".meta") || files[i].Contains(".DS_Store") || files[i].Contains(".tpsheet") || files[i].Contains(".tps"))
                    continue;
                files[i] = files[i].Replace('\\', '/');
                tempFiles.Add(files[i]);
            }
        }
        else//指定扩展名文件
        {
            string[] files = Directory.GetFiles(path_, pattern_, option_);
            //string[] files = Directory.GetFiles(path_, pattern_);
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Replace('\\', '/');
                tempFiles.Add(files[i]);
            }
        }

        if (tempFiles.Count == 0)
            return;

        AssetBundleBuild tempBuild = new AssetBundleBuild();
        tempBuild.assetBundleName = bundleName_ + AppConst.ExtName;
        tempBuild.assetNames = tempFiles.ToArray();

        CheckSameName(tempBuild.assetBundleName, path_, AbBuildList_);
        AbBuildList_.Add(tempBuild);
    }

    /// <summary>
    /// 以文件名为AB包名，进行整理
    /// </summary>
    static void AddBundleFileBuild(string pattern_, string path_, ref List<AssetBundleBuild> AbBuildList_)
    {

        string[] files = Directory.GetFiles(path_, pattern_);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".meta") || files[i].Contains(".DS_Store") || files[i].Contains(".tpsheet") || files[i].Contains(".tps"))
                continue;

            string tempFileName = Path.GetFileNameWithoutExtension(files[i]).ToLower();
            int tempIdx = tempFileName.IndexOf(".");
            if (tempIdx > 0)//如果有两个扩展名：.json.bytes
            {
                tempFileName = tempFileName.Substring(0, tempIdx);
            }
            string tempAbFileName = tempFileName + AppConst.ExtName;
            files[i] = files[i].Replace('\\', '/');
            List<string> tempFiles = new List<string>();
            tempFiles.Add(files[i]);
            AssetBundleBuild tempBuild = new AssetBundleBuild();
            tempBuild.assetBundleName = tempAbFileName;//文件名
            tempBuild.assetNames = tempFiles.ToArray();

            CheckSameName(tempBuild.assetBundleName, path_, AbBuildList_);
            AbBuildList_.Add(tempBuild);
        }
    }
    ///// <summary>
    ///// 遍历目录
    ///// </summary>
    static void GetFilesName(string pathName_, ref List<string> list_)
    {
        string[] subFiles = Directory.GetFiles(pathName_);
        foreach (string subFile in subFiles)
        {
            string ext = Path.GetExtension(subFile);
            if (ext.Equals(".meta"))
                continue;
            list_.Add(subFile.Replace('\\', '/'));
        }

        string[] subDirs = Directory.GetDirectories(pathName_);
        foreach (string subDir in subDirs)
        {
            GetFilesName(subDir, ref list_);
        }
    }

    static void BuildAbTestByFiles(List<string> srcFileList_, ref List<AssetBundleBuild> abFileList_)
    {
        foreach (var tempFileName in srcFileList_)
        {
            string[] tempFileInfo = tempFileName.Split('/');
            string tempPath = "Assets/" + tempFileName;
            if (tempFileInfo.Length >= 2)
            {
                //if (tempFileInfo[1] == "Atlas")//图集文件独立打包
                //{
                //    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                //    //AddBundleDirBuild(tempFileInfo[tempFileInfo.Length - 1].ToLower() + "_assets" + AppConst.ExtName, "*.png", tempPath, ref abFileList_);
                //}
                //else if (tempFileInfo[1] == "Animation")//动画文件独立打包
                //{
                //    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                //}
                //else if (tempFileInfo[1] == "Effect")//特效
                //{
                //    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                //}
                //else
                if (tempFileInfo[1] == "Sounds")//音效文件独立打包
                {
                    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                }
                //else if (tempFileInfo[1] == "Video")
                //{
                //    AddBundleFileBuild("*.mp4", tempPath, ref abFileList_);
                //}
                //else if (tempFileInfo[1] == "Spine")//spine文件以目录为单位进行打包
                //{
                //    AddBundleDirBuild(tempFileInfo[tempFileInfo.Length - 1].ToLower(), "*", tempPath, ref abFileList_);
                //}
                //else if (tempFileInfo[1] == "Effect")
                //{
                //    AddBundleDirBuild(tempFileInfo[tempFileInfo.Length - 1].ToLower(), "*", tempPath, ref abFileList_);
                //}
                //else if (tempFileInfo[1] == "Font")
                //{//字体文件单独打包
                //    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                //}
                //else if (tempFileInfo[1] == "Prefabs")//预设文件独立打包
                //{
                //    AddBundleFileBuild("*.prefab", tempPath, ref abFileList_);
                //}
            }
        }
    }

    /// <summary>
    /// 根据指定的文件列表信息，整理出要打包的AB信息 
    /// </summary>
    static void BuildAbByFiles(List<string> srcFileList_, ref List<AssetBundleBuild> abFileList_)
    {
        foreach (var tempFileName in srcFileList_)
        {
            string[] tempFileInfo = tempFileName.Split('/');
            string tempPath = "Assets/" + tempFileName;
            if (tempFileInfo.Length >= 2)
            {
                //if (tempFileInfo[1] == "Sounds")//音效文件独立打包
                //{
                //    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                //}
                //continue;
                if (tempFileInfo[1] == "Atlas")//图集文件独立打包
                {
                    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                    //AddBundleDirBuild(tempFileInfo[tempFileInfo.Length - 1].ToLower() + "_assets" + AppConst.ExtName, "*.png", tempPath, ref abFileList_);
                }
                else if (tempFileInfo[1] == "Animation")//动画文件独立打包
                {
                    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                }
                //else if (tempFileInfo[1] == "Effect")//特效
                //{
                //    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                //}
                else if (tempFileInfo[1] == "Sounds")//音效文件独立打包
                {
                    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                }
                //else if (tempFileInfo[1] == "Video")
                //{
                //    AddBundleFileBuild("*.mp4", tempPath, ref abFileList_);
                //}
                else if (tempFileInfo[1] == "Spine")//spine文件以目录为单位进行打包
                {
                    AddBundleDirBuild(tempFileInfo[tempFileInfo.Length - 1].ToLower(), "*", tempPath, ref abFileList_);
                }
                //else if (tempFileInfo[1] == "Effect")
                //{
                //    AddBundleDirBuild(tempFileInfo[tempFileInfo.Length - 1].ToLower(), "*", tempPath, ref abFileList_);
                //}
                else if (tempFileInfo[1] == "Font")
                {//字体文件单独打包
                    AddBundleFileBuild("*.*", tempPath, ref abFileList_);
                    //AddBundleFileBuild("*.otf", tempPath, ref abFileList_);
                    //AddBundleFileBuild("*.ttf", tempPath, ref abFileList_);
                }
                else if (tempFileInfo[1] == "Prefabs")//预设文件独立打包
                {
                    AddBundleFileBuild("*.prefab", tempPath, ref abFileList_);
                }
            }
        }
    }

    /// <summary>
    /// 根据指定的目录信息，以目录为一个AB包，整理出要打包的AB信息 
    /// </summary>
    static void BuildAbByPath(string AbName_, string path_, string pattern_, ref List<AssetBundleBuild> abFileList_)
    {
        AddBundleDirBuild(AbName_, pattern_, path_, ref abFileList_, SearchOption.AllDirectories);
    }

    static void BuildLuaAbFile(string luaPath_, ref List<AssetBundleBuild> abFileList_)
    {
        AddBundleDirBuild(AppConst.LuaAbAssetName, "*.bytes", luaPath_, ref abFileList_, SearchOption.AllDirectories);
    }



    //打包指定目录下的场景
    static void BuildScene(string assetPath_, string extName_, string outPath_, BuildTarget target_)
    {
        string[] files = Directory.GetFiles(assetPath_, "*.unity");
        for (int i = 0; i < files.Length; i++)
        {
            string tempFileName = Path.GetFileNameWithoutExtension(files[i]);
            files[i] = files[i].Replace('\\', '/');
            //string[] tempFileInfo = files[i].Split('/');
            //string tempFileName
            List<string> tempSceneList = new List<string>();
            tempSceneList.Add(files[i]);
            string tempOutPathName = string.Format("{0}/{1}/{2}{3}", Application.dataPath, outPath_, tempFileName, extName_);
            //string tempPutPathName = Application.dataPath + "/Scene.unity3d";
            BuildPipeline.BuildPlayer(tempSceneList.ToArray(), tempOutPathName, target_, BuildOptions.BuildAdditionalStreamedScenes);
            AssetDatabase.Refresh();
        }
        //for (int i = 0; i < files.Length; i++)
        //{
        //    files[i] = files[i].Replace('\\', '/');
        //}
        // BuildPipeline.BuildPlayer(files, outPath_, target_, BuildOptions.None);

    }

    /// <summary>
    /// 指定目录，查找出最终打包的AB文件名
    /// 取最后一个文件夹名为目录
    /// </summary>
    static void CalcAbFileList(string filePath_, ref List<string> fileList_)
    {
        string[] subFiles = Directory.GetFiles(filePath_);
        foreach (string subFile in subFiles)
        {
            string ext = Path.GetExtension(subFile);
            if (ext.Equals(".meta"))
                continue;
            string tempFileName = subFile.Replace('\\', '/');
            string item = tempFileName.Substring(0, tempFileName.LastIndexOf('/')).Replace(AppDataPath + "/", "");
            if (fileList_.Contains(item))
                continue;
            fileList_.Add(item);
        }

        string[] subDirs = Directory.GetDirectories(filePath_);
        foreach (string subDir in subDirs)
        {
            CalcAbFileList(subDir, ref fileList_);
        }
    }

    /// <summary>
    /// 生成指定目录下的文件MD5信息
    /// </summary>
    static void BuildFileIndex(string sourceFilePath_, string DestFileName_)
    {
        List<string> tempFileList = new List<string>();
        GetFilesName(sourceFilePath_, ref tempFileList);

        if (File.Exists(DestFileName_)) File.Delete(DestFileName_);

        FileStream fs = new FileStream(DestFileName_, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);

        for (int i = 0; i < tempFileList.Count; i++)
        {
            string tempFileName = tempFileList[i];
            if (tempFileName.EndsWith(".meta") || tempFileName.Contains(".DS_Store") || tempFileName.Contains(".tpsheet") || tempFileName.Contains(".tps")|| tempFileName.Contains(".manifest"))
                continue;

            try
            {
                // string tempMD5 = GameUtility.md5file(tempFileName);
                string tempMD5 = CalcMd5FromFileName(tempFileName);
                if (null == tempMD5) tempMD5 = GameUtility.md5file(tempFileName);

                string value = tempFileName.Replace(sourceFilePath_ + "/", string.Empty);
                string tempSaveFileName = CalcAbNameFromFileName(value);//计算AB包名称
                if (null == tempSaveFileName) tempSaveFileName = value;

                long tempSize = GameUtility.SafeReadAllBytes(tempFileName).Length / 1024;
                //File.Move(tempFileName, sourceFilePath_ + "/" + tempSaveFileName);
                if (tempSize == 0) tempSize = 1;
                sw.WriteLine(tempSaveFileName + "|" + tempMD5 + ";" + tempSize);
            }

            catch (Exception ex) { }
        }
        sw.Close();
        fs.Close();
    }

    static void BuildFileIndexOld(string sourceFilePath_, string DestFilePath_, bool isManifest_)
    {
        List<string> tempFileList = new List<string>();
        GetFilesName(sourceFilePath_, ref tempFileList);

        if (File.Exists(DestFilePath_))
            File.Delete(DestFilePath_);
        FileStream fs = new FileStream(DestFilePath_, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);

        for (int i = 0; i < tempFileList.Count; i++)
        {
            string tempFileName = tempFileList[i];
            if (tempFileName.EndsWith(".meta") || tempFileName.Contains(".DS_Store") || tempFileName.Contains(".tpsheet") || tempFileName.Contains(".tps"))
                continue;
            if (!isManifest_ && tempFileName.EndsWith(".manifest"))
            {
                string tempName = tempFileName.Replace(sourceFilePath_ + "/", string.Empty);
                if (tempName != "StreamingAssets.manifest")//排除主依赖文件
                    continue;
            }
            long tempSize = GameUtility.SafeReadAllBytes(tempFileName).Length / 1024;
            if (tempSize == 0) tempSize = 1;
            string tempMD5 = GameUtility.md5file(tempFileName);
            string value = tempFileName.Replace(sourceFilePath_ + "/", string.Empty);
            sw.WriteLine(value + "|" + tempMD5 + ";" + tempSize);
            
            //sw.WriteLine(tempSaveFileName + "|" + tempMD5 + ";" + tempSize);
        }
        sw.Close();
        fs.Close();
    }

    static void BuildFileIndexFromMd5(string sourceFilePath_, string DestFilePath_, string md5FileName_, bool isManifest_)
    {
        List<string> tempFileList = new List<string>();
        GetFilesName(sourceFilePath_, ref tempFileList);

        if (File.Exists(DestFilePath_))
            File.Delete(DestFilePath_);
        FileStream fs = new FileStream(DestFilePath_, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);

        Dictionary<string, string> tempAbMd5Dic = GameUtility.ReadMD5FileWithOutSize(md5FileName_);

        for (int i = 0; i < tempFileList.Count; i++)
        {
            string tempFileName = tempFileList[i];
            if (tempFileName.EndsWith(".meta") || tempFileName.Contains(".DS_Store") || tempFileName.Contains(".tpsheet") || tempFileName.Contains(".tps"))
                continue;
            if (!isManifest_ && tempFileName.EndsWith(".manifest"))
            {
                string tempName = tempFileName.Replace(sourceFilePath_ + "/", string.Empty);
                if (tempName != "StreamingAssets.manifest")//排除主依赖文件
                    continue;
            }

            //文件大小
            long tempSize = GameUtility.SafeReadAllBytes(tempFileName).Length / 1024;
            if (tempSize == 0) tempSize = 1;

            //文件MD5
            string tempMD5 = GameUtility.md5file(tempFileName);
            string value = tempFileName.Replace(sourceFilePath_ + "/", string.Empty);

            if (value == "ver.txt" || value == "md5files.txt")//把这些文件排除在外 
                continue;

            if (tempAbMd5Dic.ContainsKey(value))
            {
                tempMD5 = tempAbMd5Dic[value];
                //Debug.Log("----------从MD5文件读取---------FileName = " + value + "-----MD5 = " + tempMD5);
            }
            else
            {
               // Debug.LogError("----------自己计算MD5---------FileName = " + value + "-----MD5 = " + tempMD5);
            }

            //写入
            sw.WriteLine(value + "|" + tempMD5 + ";" + tempSize);



        }
        sw.Close();
        fs.Close();
    }

    //根据文件名，截取MD5信息
    static string CalcMd5FromFileName(string fileName_)
    {
        if (fileName_.EndsWith(".unity3d"))
        {
            //md5值在最后一个‘_’和‘.’之间
            int tempStartMD5Idx = fileName_.LastIndexOf('_');
            if (-1 == tempStartMD5Idx)
            {
                return null;
            }
            int tempEndMd5Idx = fileName_.LastIndexOf('.');
            string tempMD5 = fileName_.Substring(tempStartMD5Idx + 1, tempEndMd5Idx - tempStartMD5Idx - 1);
            return tempMD5;
        }
        else
            return null;

    }

    static string CalcAbNameFromFileName(string fileName_)
    {
        if (fileName_.EndsWith(".unity3d"))
        {
            Debug.Log("--------------计算ABName------111---------------" + fileName_);
            int tempStartMD5Idx = fileName_.LastIndexOf('_');
            if (-1 == tempStartMD5Idx)
            {
                return fileName_;
            }
            string tempNewName = fileName_.Substring(0, tempStartMD5Idx) + AppConst.ExtName;
            Debug.Log("--------------计算ABName------222---------------" + tempNewName);
            return tempNewName;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 复制文件
    /// </summary>
    static void CopyFiles(List<string> srcFileList_, string srcFileDir_, string destDir_)
    {
        for (int i = 0; i < srcFileList_.Count; i++)
        {
            string str = string.Format("{0}/{1}", srcFileDir_, srcFileList_[i]);
            if (File.Exists(str))
            {
                string dest = destDir_ + "/" + srcFileList_[i];
                string dir = Path.GetDirectoryName(dest);
                Directory.CreateDirectory(dir);
                File.Copy(str, dest, true);
            }

        }
    }

    //生成升级资源包
    public static void BuildUpdatePackage(string abUpdatePath_)
    {
        string tempStreamPath = Application.streamingAssetsPath;
        if (Directory.Exists(abUpdatePath_))
        {
            EditorUtility.DisplayDialog("更新提示", "你要跟更新版本已经存在，即将重新打包", "知道了");
            Directory.Delete(abUpdatePath_, true);
        }

        int tempLocalVerNum = GameUtility.ReadVerFile(GameUtility.AppContentPath() + AppConst.AssetVerFileName);
        Directory.CreateDirectory(abUpdatePath_);
        string tempUpdateFileName = GetVerLogFileName();
        if (s_firstVerNum == tempLocalVerNum)
        {
            CopyFiles(BuildAbList(tempUpdateFileName), tempStreamPath, abUpdatePath_);
        }
        else
        {
            string tempOldFileName = GetSafeLastVerNameM();
            if (tempOldFileName != null)
            {
                List<string> tempList = GameUtility.GetDifferentVerFiles(tempUpdateFileName, tempOldFileName,false);
                if (tempList.Count == 3)
                    EditorUtility.DisplayDialog("更新提示", "你没有更新任何资源！", "知道了");
                else
                {
                    CopyFiles(tempList, tempStreamPath, abUpdatePath_);
                    AssetDatabase.Refresh();
                    EditorUtility.DisplayDialog("更新提示", "资源升级包已生成！", "知道了");
                }
            }

        }
        //  Directory.ReferenceEquals()
    }

    private static string GetSafeLastVerNameM()
    {
        int tempCurVerNo = GetCurVerNumber();
        for (int i = 1; i < 1000; i++)
        {
            string tempFileName = s_VerLogPath + (tempCurVerNo - i) + ".txt";
            if (File.Exists(tempFileName))
            {
                return tempFileName;
            }
        }
        return null;
    }
    public static List<string> BuildAbList(string fileName_)
    {
        List<string> tempList = new List<string>();
        Dictionary<string, string> tempFileDic = GameUtility.ReadMD5File(fileName_);
        foreach (string tempFileName in tempFileDic.Keys)
        {
            tempList.Add(tempFileName);
            tempList.Add(tempFileName + ".manifest");
        }
        tempList.Add("files.txt");

        return tempList;
    }

    private static void CoypDirData(string sourcePath_, string savePath_)
    {
        string[] tempEffectDirs = Directory.GetDirectories(sourcePath_);
        foreach (string tempSubDir in tempEffectDirs)
        {
            string tempCurDirName = GetLastPathName(tempSubDir);
            string tempNewFilePath = savePath_ + "/" + tempCurDirName;

            CopyDirectory(tempSubDir, tempNewFilePath);
        }
    }

    private static void ClearPath(string path_)
    {
        if (Directory.Exists(path_))
        {
            Directory.Delete(path_, true);
            Directory.CreateDirectory(path_);
        }
    }

    private static string GetLastPathName(string path_)
    {
        string tempFileName = path_.Replace('\\', '/');
        int tempStartLen = tempFileName.LastIndexOf('/') + 1;
        int tempEndLen = tempFileName.Length - tempStartLen;
        string tempLastName = tempFileName.Substring(tempStartLen, tempEndLen);
        return tempLastName;
    }
    // <summary>
    /// 复制文件夹中的所有内容
    /// </summary>
    /// <param name="sourceDirPath">源文件夹目录</param>
    /// <param name="saveDirPath">指定文件夹目录</param>
    private static void CopyDirectory(string sourceDirPath, string saveDirPath)
    {

        if (!Directory.Exists(saveDirPath))
        {
            Directory.CreateDirectory(saveDirPath);
        }
        string[] files = Directory.GetFiles(sourceDirPath);

        m_ProcessTitleText = saveDirPath;
        m_ProcessCurText = "";
        m_totalPorcessValue = (float)files.Length;
        m_curPorcessValue = 0f;
        foreach (string file in files)
        {
            m_curPorcessValue += 1;
            string pFilePath = saveDirPath + "\\" + Path.GetFileName(file);
            if (File.Exists(pFilePath))
                continue;
            if (/*file.EndsWith(".meta") ||*/ file.Contains(".DS_Store") || file.EndsWith(".asset"))
                continue;

            m_ProcessCurText = file;
            File.Copy(file, pFilePath, true);
        }
        m_curPorcessValue = (float)files.Length;

        string[] dirs = Directory.GetDirectories(sourceDirPath);
        foreach (string dir in dirs)
        {
            CopyDirectory(dir, saveDirPath + "\\" + Path.GetFileName(dir));
        }


    }

    //[MenuItem("AssetsBundle/CopyLuaFilesToGameAssets", false, 100)]
    /// <summary>
    /// 复制Lua脚本到打包目录
    /// </summary>
    static void CopyLuaFilesToGameAssets()
    {
        string destination = Path.Combine(Application.dataPath, AppConst.GameAssetsFolderName);
        destination = Path.Combine(destination, AppConst.LuaAbAssetName);
        string source = Path.Combine(Application.dataPath, AppConst.LuaScriptsFolder);
        Debug.Log("------source-----" + source);
        GameUtility.SafeDeleteDir(destination);

        Debug.Log(" Copy lua files Begin");
        FileUtil.CopyFileOrDirectoryFollowSymlinks(source, destination);

        var notLuaFiles = GameUtility.GetSpecifyFilesInFolder(destination, new string[] { ".lua", ".pb" }, true);
        if (notLuaFiles != null && notLuaFiles.Length > 0)
        {
            for (int i = 0; i < notLuaFiles.Length; i++)
            {
                GameUtility.SafeDeleteFile(notLuaFiles[i]);
            }
        }

        var luaFiles = GameUtility.GetSpecifyFilesInFolder(destination, new string[] { ".lua", ".pb" }, false);
        if (luaFiles != null && luaFiles.Length > 0)
        {
            for (int i = 0; i < luaFiles.Length; i++)
            {
                GameUtility.SafeRenameFile(luaFiles[i], luaFiles[i] + AppConst.LuaExtName);
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("Copy lua files over");
    }

    private static float m_totalPorcessValue = 1f;
    private static float m_curPorcessValue = 0f;
    private static bool m_isShowProcess = false;
    private static string m_ProcessTitleText = "你瞅啥？";
    private static string m_ProcessCurText = "瞅你咋地！";
    public void CopyVerToStream(GameObject obj, string path)
    {


    }

    //void OnGUI()
    //{
    //    if (m_isShowProcess)
    //    {
    //        EditorUtility.DisplayProgressBar(m_ProcessTitleText, m_ProcessCurText, (float)(m_curPorcessValue / m_totalPorcessValue));
    //    }
    //}

    //void OnInspectorUpdate() //更新  
    //{
    //    Repaint();  //重新绘制  
    //}

}
