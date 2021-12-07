
using System.Collections;
using MGame.Resource;
using UnityEngine;

/// <summary>
/// 资源加载器
/// by lh
/// 2021-12-4
/// </summary>
namespace MGame.ResourceLoader
{
    public sealed class AssetRefDependCache 
    {
        public string AssetBundleName;          //ab资源名称
        public AssetBundle AssetBundle;      // ab资源
        //public Object AssetObject;          // 资源
        public int RefCount;                // 引用数
        public AssetRefDependCache() { }

        public AssetRefDependCache(string assetBundleName, AssetBundle assetBundle)
        {
            AssetBundleName = assetBundleName;
            AssetBundle = assetBundle;
        }
        public void Clear()
        {
            AssetBundleName = string.Empty;
            AssetBundle = null;
            RefCount = 0;
        }
    }

    public sealed class AssetLoadRequest<T> : YieldInstruction where T : UnityEngine.Object
    {

        AssetAsyncLoader<T> loader_;
        internal AssetLoadRequest(AssetAsyncLoader<T> loader)
        {
            loader_ = loader;
        }
        /// <summary>
        /// 是否加载完成? (Read Only)
        /// </summary>
        public bool IsDone
        {
            get { return loader_.IsDone; }
        }
        /// <summary>
        /// 加载进度. (Read Only)
        /// </summary>
        public float Progress
        {
            get { return loader_.Progress; }
        }

        /// <summary>
        /// 已加载的资源 (Read Only).
        /// </summary>
        public T TAsset
        {
            get { return loader_.TAsset; }
        }
        /// <summary>
        /// 资源名(Read Only)
        /// </summary>
        public string AssetName
        {
            get { return loader_.AssetName; }
        }

        /// <summary>
        /// 资源原始名称
        /// </summary>
        public string OrignalAssetName
        {
            get { return loader_.OrignalAssetName; }
        }

        /// <summary>
        /// 已加载的子资源 (Read Only)
        /// </summary>
        public T[] AllAssets
        {
            get { return loader_.AllAssets; }
        }

        /// <summary>
        /// AssetBundle名称
        /// </summary>
        public string AssetBundleName
        {
            get { return loader_.AssetBundleName; }
        }
    }
    public class AssetAsyncLoader<T> where T : UnityEngine.Object
    {
        #region 属性
        /// <summary>
        /// AssetBundle名称
        /// </summary>
        public string AssetBundleName { get; private set; }

        /// <summary>
        /// 资源名称(全部小写)
        /// </summary>
        public string AssetName { get; private set; }

        /// <summary>
        /// 资源名称（原始名称）
        /// </summary>
        public string OrignalAssetName { get; private set; }
        /// <summary>
        /// 是否加载完成?
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        /// 加载进度.
        /// </summary>
        public float Progress { get; private set; }

        /// <summary>
        /// 已加载的资源
        /// </summary>
        public T TAsset { get; private set; }

        /// <summary>
        /// 已加载的子资源
        /// </summary>
        public T[] AllAssets { get; private set; }
        #endregion
        public void Clear()
        {
            AssetBundleName = string.Empty;
            AssetName = string.Empty;
            OrignalAssetName = string.Empty;
            IsDone = false;
            Progress = 0f;
            TAsset = null;
            AllAssets = null;
        }
        public AssetAsyncLoader(string assetbundlename, string asset_name, string orignal_asset_name)
        {
            AssetBundleName = assetbundlename;
            AssetName = asset_name;
            OrignalAssetName = orignal_asset_name;
            IsDone = false;
            Progress = 0f;
            TAsset = null;
            AllAssets = null;
        }
        //public static AssetAsyncLoader<T> GetAssetRefDependCacheClass()
        //{
        //    return ReferencePool.ReferencePool.Acquire<AssetAsyncLoader>();
        //}

        /// <summary>
        ///   加载一个资源
        /// </summary>
        public IEnumerator StartLoadAssetAsync(AssetBundlesManager mgr)
        {
            if (string.IsNullOrEmpty(AssetBundleName))
            {
                IsDone = true;
                yield break;
            }
            ///标记未完成
            IsDone = false;
            Progress = 0f;
            /// 加载依赖的assetbundle
            float current = 0;
            float count = 2;
            string[] deps = mgr.GetAbAllDependencies(AssetBundleName);
            if (deps != null)
            {
                count += deps.Length;
                for (int index = 0; index < deps.Length; index++)
                {
                    yield return mgr.LoadAssetBundleAsync(deps[index],true);
                    Progress = ++current / count;
                }
            }
            /// 加载assetbundle
            {
                yield return mgr.LoadAssetBundleAsync(AssetBundleName);
                Progress = ++current / count;
            }
            /// 加载资源
            {
                var assetRefDependCache = mgr.GetRefCacheAssetBundleDictByName(AssetBundleName);
                if (assetRefDependCache == null || assetRefDependCache.AssetBundle == null)
                {
                    IsDone = true; // 资源加载异常！
                    yield break;
                }
                var req = assetRefDependCache.AssetBundle.LoadAssetAsync(AssetName);
                while (!req.isDone)
                {
                    yield return req;
                }
                TAsset = req.asset as T;
                AllAssets = req.allAssets as T[];
                Progress = ++current / count;
            }
            //if (UnloadAssetBundle)
            //{
            //    mgr.DisposeAssetBundleCache(deps, false);
            //    mgr.DisposeAssetBundleCache(AssetBundleName, false);
            //}
            //else
            //{
            //    mgr.SaveAssetDependency(AssetName, AssetBundleName);
            //}

            IsDone = true;
            Progress = 1;
            yield break;
        }
    }
}
