using Common.General;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// AssetBundle资源加载管理
/// 注意：
/// 目前为同步加载资源版本，
/// 以后再补全异步加载版本
/// 异步加载版本需要添加 引用计数，
/// 并在合适的时机卸载AB资源
/// ljs
/// 2020.05.15
/// </summary>
namespace MGame.Resource
{
    public enum AbUnLoadType
    {
        UnLoadNone = 0,//永不卸载
        UnLoadImmediately = 1,//立即卸载:只卸载AB，不卸载Asset，也不卸载依赖AB
        UnloadLate = 2,//晚点再卸载 ：即卸载AB，也卸载Asset
    }
    public class AssetBundleInfo
    {
        public AssetBundle m_AssetBundle;
        public int m_ReferencedCount;

        public AssetBundleInfo(AssetBundle assetBundle)
        {
            m_AssetBundle = assetBundle;
            m_ReferencedCount = 0;
        }
    }

    public class AssetBundlesManager : TSingletonMono<AssetBundlesManager>
    {

        private AssetBundleManifest m_manifest;
        private Dictionary<string, AssetBundle> m_bundles;
        private Dictionary<string, AssetBundle> m_mLateUnLoadBundles;//记录暂时的AB，用于合适的时机进行释放
        private Dictionary<string, string> m_abMd5Dic;
        private AssetBundle assetbundle;
        private string[] m_Variants = { };
        private bool isInit = false;


        public void InitDataM()//启动的时候调用
        {
#if USE_PACKAGE
            if (isInit) return;

            isInit = true;
            byte[] stream = null;
            string uri = string.Empty;
            m_bundles = new Dictionary<string, AssetBundle>();
            m_mLateUnLoadBundles = new Dictionary<string, AssetBundle>();
            uri = GameUtility.DataPath + AppConst.AssetDir;
            //if (!File.Exists(uri))
            //{
            //    Debug.LogError("------AB资源初始化异常，缺少核心文件------");
            //    return;
            //}
            AssetBundle.SetAssetBundleDecryptKey("yulong1688yulong");
            assetbundle = AssetBundle.LoadFromFile(uri);
            //stream = File.ReadAllBytes(uri);
            //assetbundle = AssetBundle.LoadFromMemory(stream);
            m_manifest = assetbundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            //InitABMd5Info();

#endif
        }

        private void InitABMd5Info()
        {
            string tempFilesPath = GameUtility.DataPath + "files.txt";//沙盒目录
            m_abMd5Dic = GameUtility.ReadMD5FileWithOutSize(tempFilesPath);
        }

        string _tempAbName;
        private string GetMd5AbFileName(string fileName_)
        {
            if (m_abMd5Dic.ContainsKey(fileName_))
            {
                string[] _itempSplit = fileName_.Split('.');
                _tempAbName = _itempSplit[0] + "_" + m_abMd5Dic[fileName_] + "." + _itempSplit[1];
                //Debug.Log("------------GetMd5AbFileName--------------"+ _tempAbName);
                return _tempAbName;
            }
            else
                return fileName_;

            
        }

        Dictionary<string, Object> mAssetDic = new Dictionary<string, Object>();
        Dictionary<string, Object> mLateUnloadAssetDic = new Dictionary<string, Object>();//合适时机需要清空的资源


        public void UnLoadAllAssetM()//卸载所有所有资源
        {
            foreach ( var tempGo in mAssetDic)
            {
                Resources.UnloadAsset(tempGo.Value);//卸载接口定义
            }
            mAssetDic.Clear();
        }

        public void UnloadUnUsedAbAndAssetM()//清除AB包以及相应的资源Asset，注意：这个接口需要在GameObject 已经销毁的情况下调用
        {
#if USE_PACKAGE
            List<string> tempNameList = m_mLateUnLoadBundles.Keys.ToList();
            foreach (var tempABName in tempNameList)
            {
                Debug.Log("-------ljs---UnloadAllAbM-------------------" + tempABName);
                if (m_mLateUnLoadBundles[tempABName])
                {
                    m_mLateUnLoadBundles[tempABName].Unload(true);
                }
            }
            //清空AB容器
            m_mLateUnLoadBundles.Clear();
            //清空Asset容器
            mLateUnloadAssetDic.Clear();
#endif

        }
       public void UnloadAllAbAndAssetM()//清除AB包以及相应的资源Asset，注意：这个接口需要在GameObject 已经销毁的情况下调用
        {
#if USE_PACKAGE
            this.UnloadUnUsedAbAndAssetM();

           List<string> tempNameList = m_bundles.Keys.ToList();
            foreach (var tempABName in tempNameList)
            {
                Debug.Log("-------ljs---UnloadAllAbM-------------------"+ tempABName);
                if (m_bundles[tempABName])
                {
                    m_bundles[tempABName].Unload(true);
                }
                
            }
            m_bundles.Clear();
            mAssetDic.Clear();
#endif
        }

        /// <summary>
        /// 从AB包中加载一个指定类型的资源
        /// </summary>
        public T LoadAsset<T>(string AbName_, string AssetName_,AbUnLoadType unLoadType_ = AbUnLoadType.UnLoadNone) where T : UnityEngine.Object
        {
            if (0 == AbName_.Length || 0 == AssetName_.Length)
                return null;

            T temp = null;
            AbName_ = AbName_.ToLower();

            if (mAssetDic.ContainsKey(AssetName_) && mAssetDic[AssetName_] != null )//Asset资源缓存池 
            {
              //  
                temp = (T)mAssetDic[AssetName_];
                return temp;
            }

            if (mLateUnloadAssetDic.ContainsKey(AssetName_) && mLateUnloadAssetDic[AssetName_] != null)//Asset资源缓存池 
            {
                //  
                temp = (T)mLateUnloadAssetDic[AssetName_];
                return temp;
            }

            AssetBundle bundle = LoadAssetBundle(AbName_, unLoadType_);
            if (null != bundle)
            {
                temp = bundle.LoadAsset<T>(AssetName_);
                //if( !mAssetDic.ContainsKey(AssetName_))
                //{
                //    mAssetDic[AssetName_] = temp;
                //}

                if (unLoadType_ == AbUnLoadType.UnLoadNone)//不卸载
                {
                    if (!mAssetDic.ContainsKey(AssetName_))
                    {
                        mAssetDic[AssetName_] = temp;
                    }
                }
                else if (unLoadType_ == AbUnLoadType.UnLoadImmediately)//立即卸载: 只卸载 AB，不卸载资源
                {
                    if (!mAssetDic.ContainsKey(AssetName_))
                    {
                        mAssetDic[AssetName_] = temp;
                    }
                    bundle.Unload(false);
                }
                else if (unLoadType_ == AbUnLoadType.UnloadLate)//晚些时候卸载
                {
                    if (!mLateUnloadAssetDic.ContainsKey(AssetName_))
                    {
                        mLateUnloadAssetDic[AssetName_] = temp;
                    }
                }

                //if (unLoadAssetBundle_)//把AB卸载出内存
                //{
                //    bundle.Unload(false);
                //}
                //return temp;
            }
            return temp;
        }

        /// <summary>
        /// 载入AssetBundle
        /// <returns></returns>
//        public AssetBundle LoadAssetBundle(string abname_)
//        {
//#if USE_PACKAGE
//            Debug.Log("----------------LoadAssetBundle----------------" + abname_);
//            if (!abname_.EndsWith(AppConst.ExtName))
//            {
//                abname_ += AppConst.ExtName;
//            }

//            AssetBundle bundle = null;
//            if (!m_bundles.ContainsKey(abname_))
//            {
//                string tempAbName = CalcAbNameFromFileName(abname_);
//                string tempSaveKeyName = abname_;
//                if (m_bundles.ContainsKey(tempAbName))
//                {
//                    m_bundles.TryGetValue(tempAbName, out bundle);
//                    return bundle;
//                }
//                string uri = GameUtility.DataPath + abname_;//从DataPath目录读取数据

//                if (false == File.Exists(uri))//文件不存在则返回为空
//                {
//                    //Debug.Log("--------------LoadAssetBundle-------AB文件不存在--------"+ uri);
//                    //string tempAbName = CalcAbNameFromFileName(abname_);
//                    tempSaveKeyName = tempAbName;
//                    uri = GameUtility.DataPath + tempAbName;
//                    //Debug.Log("--------------LoadAssetBundle-------AB文件名称重新计算--------" + uri);
//                    if (false == File.Exists(uri))
//                    {
//                        return bundle;
//                    }

//                }

//                Debug.LogWarning("LoadFile::>> " + uri);
//                LoadDependencies(abname_);

//                //stream = File.ReadAllBytes(uri);
//                //if (null == stream)
//                //    return null;
//                AssetBundle.SetAssetBundleDecryptKey("yulong1688yulong");
//                //AssetBundle.SetAssetBundleDecryptKey(null);
//                bundle = AssetBundle.LoadFromFile(uri);
//                //bundle = AssetBundle.LoadFromMemory(stream); //关联数据的素材绑定
//                //AssetBundle.LoadFromMemoryAndDecrypt();
//                //m_bundles.Add(abname_, bundle);
//                m_bundles.Add(tempSaveKeyName, bundle);

//            }
//            else
//            {
//                m_bundles.TryGetValue(abname_, out bundle);
//            }
//            return bundle;
//#else
//            return null;
//#endif
//        }
        string CalcAbNameFromFileName(string fileName_)
        {
            //Debug.Log("--------------计算ABName---------------------" + fileName_);
            int tempStartMD5Idx = fileName_.LastIndexOf('_');
            if (-1 == tempStartMD5Idx)
            {
                return fileName_;
            }
            string tempNewName = fileName_.Substring(0, tempStartMD5Idx) + AppConst.ExtName;
            return tempNewName;

        }
        /// <summary>
        /// 载入依赖
        /// </summary>
        //void LoadDependencies(string name)
        //{
        //    if (m_manifest == null)
        //    {
        //        Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
        //        return;
        //    }
        //    // Get dependecies from the AssetBundleManifest object..
        //    string[] dependencies = m_manifest.GetAllDependencies(name);
        //    if (dependencies.Length == 0)
        //    {
        //      //  Debug.Log("-------------LoadDependencies--is null--------------");
        //        string tempMd5FileName = GetMd5AbFileName(name);
        //        if (tempMd5FileName != name)
        //        {
        //            dependencies = m_manifest.GetAllDependencies(tempMd5FileName);
        //        }
        //        if (dependencies.Length == 0)
        //        {
        //            //Debug.Log("--------------最终还是没找到依赖文件-----------------");
        //            return;
        //        }

        //    }

        //    for (int i = 0; i < dependencies.Length; i++)
        //        dependencies[i] = RemapVariantName(dependencies[i]);

        //    // Record and load all dependencies.
        //    for (int i = 0; i < dependencies.Length; i++)
        //    {
        //        Debug.Log("------------LoadDependencies-----------------------"+ dependencies[i]);
        //        LoadAssetBundle(dependencies[i]);
        //    }
        //}

        // Remaps the asset bundle name to the best fitting asset bundle variant.
        string RemapVariantName(string assetBundleName)
        {
            string[] bundlesWithVariant = m_manifest.GetAllAssetBundlesWithVariant();

            // If the asset bundle doesn't have variant, simply return.
            if (System.Array.IndexOf(bundlesWithVariant, assetBundleName) < 0)
                return assetBundleName;

            string[] split = assetBundleName.Split('.');

            int bestFit = int.MaxValue;
            int bestFitIndex = -1;
            // Loop all the assetBundles with variant to find the best fit variant assetBundle.
            for (int i = 0; i < bundlesWithVariant.Length; i++)
            {
                string[] curSplit = bundlesWithVariant[i].Split('.');
                if (curSplit[0] != split[0])
                    continue;

                int found = System.Array.IndexOf(m_Variants, curSplit[1]);
                if (found != -1 && found < bestFit)
                {
                    bestFit = found;
                    bestFitIndex = i;
                }
            }
            if (bestFitIndex != -1)
                return bundlesWithVariant[bestFitIndex];
            else
                return assetBundleName;
        }

        /// <summary>
        /// 卸载指定AB文件
        /// </summary>
        /// <returns></returns>
        public bool UnLoadAssetBundle(string abName_, bool thorough_)
        {
            AssetBundle tempAbData;
            if (m_bundles.TryGetValue(abName_, out tempAbData))
            {
                tempAbData.Unload(thorough_);
                m_bundles.Remove(abName_);
                //这里以后需要卸载依赖AB
                //...
                return true;
            }
            return false;
        }
        /// <summary>
        ///  卸载所有AB文件
        /// </summary>

        public void UnLoadAllAssetBundles(bool thorough_)
        {
#if USE_PACKAGE
            isInit = false;
            foreach (var item in m_bundles)
            {
                item.Value.Unload(thorough_);
            }
            m_bundles.Clear();
            mAssetDic.Clear();
#endif
        }

        private void OnDestroy()
        {
           // UnLoadAllAssetBundles(true);
        }


        public AssetBundle LoadAssetBundle(string abname_,AbUnLoadType unLoadType_ = AbUnLoadType.UnLoadNone)
        {
#if USE_PACKAGE
            //Common.Log("----------------LoadAssetBundle----------------" + abname_);
            if (!abname_.EndsWith(AppConst.ExtName))
            {
                abname_ += AppConst.ExtName;
            }

            AssetBundle bundle = null;
            if (!m_bundles.ContainsKey(abname_) && !m_mLateUnLoadBundles.ContainsKey(abname_))
            {
                //byte[] stream = null;
                string uri = GameUtility.DataPath + abname_;//从DataPath目录读取数据

                //if (false == File.Exists(uri))//文件不存在则返回为空
                //    return bundle;


                //Common.Log("LoadFile::>> " + uri);
                //先加载依赖资源
               //LoadDependencies(abname_, unLoadType_);
                // LoadDependencies(abname_, AbUnLoadType.UnLoadNone);//依赖加载的AB常驻内存？
                if (unLoadType_ == AbUnLoadType.UnLoadImmediately)//立即卸载AB，依赖的AB需要常驻内存
                {
                    LoadDependencies(abname_, AbUnLoadType.UnLoadNone);
                }
                else
                {
                    LoadDependencies(abname_, unLoadType_);
                }
                    //stream = File.ReadAllBytes(uri);
                    //if (null == stream)
                    //    return null;
                    AssetBundle.SetAssetBundleDecryptKey("yulong1688yulong");
                //AssetBundle.SetAssetBundleDecryptKey(null);
                bundle = AssetBundle.LoadFromFile(uri);
                //bundle = AssetBundle.LoadFromMemory(stream); //关联数据的素材绑定
                //AssetBundle.LoadFromMemoryAndDecrypt();
                //  if(!unLoadAb)m_bundles.Add(abname_, bundle);

                if (unLoadType_ == AbUnLoadType.UnLoadNone)//不卸载
                {
                    if(!m_bundles.ContainsKey(abname_))
                    {
                        m_bundles.Add(abname_, bundle);
                    }
                }
                else if (unLoadType_ == AbUnLoadType.UnLoadImmediately)//立即卸载
                {
                }
                else if (unLoadType_ == AbUnLoadType.UnloadLate)//晚些时候卸载
                {
                    if (!m_mLateUnLoadBundles.ContainsKey(abname_))
                    {
                        m_mLateUnLoadBundles.Add(abname_, bundle);
                    }
                }

              //  if (!unLoadAb)//不卸载的AB资源，存入AB缓存字典
                //{
                //    m_bundles.Add(abname_, bundle);
                //}
            }
            else
            {
                if(m_bundles.ContainsKey(abname_))//先从常驻AB列表中查找
                    m_bundles.TryGetValue(abname_, out bundle);
                else if(m_mLateUnLoadBundles.ContainsKey(abname_))//再从临时AB列表中查找
                    m_mLateUnLoadBundles.TryGetValue(abname_, out bundle);
            }
            return bundle;
#else
                return null;
#endif
        }

        /// <summary>
        /// 载入依赖
        /// </summary>
        void LoadDependencies(string name, AbUnLoadType unLoadType_ = AbUnLoadType.UnLoadNone)
        {
            if (m_manifest == null)
            {
                Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
                return;
            }
            // Get dependecies from the AssetBundleManifest object..
            string[] dependencies = m_manifest.GetAllDependencies(name);
            if (dependencies.Length == 0) return;

            for (int i = 0; i < dependencies.Length; i++)
                dependencies[i] = RemapVariantName(dependencies[i]);

            // Record and load all dependencies.
            for (int i = 0; i < dependencies.Length; i++)
            {
                //Common.Log("------------LoadDependencies-----------------------" + dependencies[i]);
                LoadAssetBundle(dependencies[i], unLoadType_);
            }
        }


    }


}
