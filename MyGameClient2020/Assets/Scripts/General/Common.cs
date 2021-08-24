
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Common.General
{
    public class Common
    {
        public static T GetGameAsset<T>(string asset_, string path_) where T : UnityEngine.Object
        {
            if (asset_.Length == 0)
                return null;
#if USE_PACKAGE_MODE
            return ResourceManager.m_Instance.LoadAsset<T>(asset_, asset_);
#else
            string tempFileName = path_ + asset_;
            return Resources.Load<T>(tempFileName);
#endif
        }

        public static GameObject GetGameObjectFromPoolM(string objectName_, string resPath_, Transform parent_)
        {
            //if (null == ObjectPoolManager.Instance.GetPool(objectName_))
            //{
            //    GameObject tempObject = GetGameAsset<GameObject>(objectName_, resPath_);
            //    if (null == tempObject)
            //        Debug.LogError("---------NotFind Res ----------------" + objectName_ + " resPath = " + resPath_);
            //    ObjectPoolManager.Instance.CreatePool(objectName_, 10, 20, tempObject);
            //}
            GameObject tempPrefabObject = null; //ObjectPoolManager.Instance.Get(objectName_);
            if (null != parent_)
                tempPrefabObject.transform.SetParent(parent_);
            return tempPrefabObject;
        }


        public static void SaveData<T>(T data, string fullPathName) where T : class
        {
            if (data != null)
            {
                Serialize(data, fullPathName);
            }
        }

        public static T LoadDatas<T>(string path) where T : class
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                return Deserialize<T>(path);
            }
            return null;
        }

        public static void Serialize(object o, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, o);
                byte[] buffer = ms.ToArray();
                File.WriteAllBytes(path, buffer);
                ms.Close();
            }
        }

        public static T Deserialize<T>(byte[] buffers) where T : class
        {
            byte[] newBuffers;
            newBuffers = buffers;
            using (MemoryStream ms = new MemoryStream(newBuffers))
            {
                BinaryFormatter bf = new BinaryFormatter();
                T obj = bf.Deserialize(ms) as T;
                ms.Close();
                return obj;
            }
        }

        public static T Deserialize<T>(string path) where T : class
        {
            //string directory = Path.GetDirectoryName(path);
            //if (!Directory.Exists(directory))
            //{
            //    return null;
            //}
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                T obj = bf.Deserialize(fs) as T;
                fs.Close();
                return obj;
            }
        }
        public static T DeserializeTwo<T>(string path) where T : class
        {
            //string directory = Path.GetDirectoryName(path);
            //if (!Directory.Exists(directory))
            //{
            //    return null;
            //}
            //using (FileStream fs = new FileStream(path, FileMode.Open))
            //{
            //    BinaryFormatter bf = new BinaryFormatter();
            //    T obj = bf.Deserialize(fs) as T;
            //    fs.Close();
            //    return obj;
            //}
#if UNITY_EDITOR
            return null;
#else
            return null;
            //var fs = BetterStreamingAssets.OpenRead(path);
            //BinaryFormatter bf = new BinaryFormatter();
            //T obj = bf.Deserialize(fs) as T;
            //fs.Close();
            //return obj;
#endif
        }


        //public static Coroutine StartCoroutine(IEnumerator routine_)
        //{
        //    return GameApp.Instance.mainMonoBehaviour.StartCoroutine(routine_);
        //}

        /// <summary>
        /// 协程
        /// </summary>
        //public static Coroutine StartCoroutine(IEnumerator routine_, MonoBehaviour monoBehaviour_)
        //{
        //    if (monoBehaviour_ != null)
        //        return monoBehaviour_.StartCoroutine(routine_);
        //    return StartCoroutine(routine_);
        //}

        /// 关闭协程
        public static void StopCoroutine(IEnumerator routine_, MonoBehaviour monoBehaviour_)
        {
            if (monoBehaviour_ != null)
                monoBehaviour_.StopCoroutine(routine_);
            StopCoroutine(routine_);
        }

        /// 关闭协程
        public static void StopCoroutine(IEnumerator routine_)
        {
            //GameApp.Instance.mainMonoBehaviour.StopCoroutine(routine_);
        }

        /// <summary>
        /// 关闭协程
        /// </summary>
        public static void StopCoroutine(string ActionName)
        {
            //GameApp.Instance.mainMonoBehaviour.StopCoroutine(ActionName);
        }

        /// <summary>
        /// 定时调用方法
        /// </summary>
        public static void InvokeFun(string funName_, float time_)
        {
           // GameApp.Instance.mainMonoBehaviour.Invoke(funName_, time_);
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        public static void InvokeFun(string funName_, float time_, float repeatRate)
        {
            //GameApp.Instance.mainMonoBehaviour.InvokeRepeating(funName_, time_, repeatRate);
        }

        /// <summary>
        /// 退出索敌爱用的方法
        /// </summary>
        /// <param name="funName_"></param>
        public static void CancelInvokeFun(string funName_)
        {
            //GameApp.Instance.mainMonoBehaviour.CancelInvoke(funName_);
        }
        public static T GetComponent<T>(GameObject obj) where T : Component
        {
            if (obj == null)
                return null;
            T commponent = obj.GetComponent<T>();
            if (commponent == null)
                commponent = obj.AddComponent<T>();
            return commponent;
        }

        public static T AddComponent<T>(GameObject obj) where T : Component
        {
            return GetComponent<T>(obj);
        }

        public static void AddChild(Transform parent, Transform child)
        {
            if (parent == null || child == null) return;
            child.parent = parent;
            child.localScale = Vector3.one;
            child.localPosition = Vector3.zero;
            child.localEulerAngles = Vector3.zero;
        }

        public static void AddChild(GameObject parent, GameObject child)
        {
            if (parent == null || child == null) return;
            AddChild(parent.transform, child.transform);
        }

        public static void ClearChildren(GameObject obj)
        {
            if (obj == null) return;
            for (int i = 0; i < obj.transform.childCount; ++i)
                UnityEngine.Object.Destroy(obj.transform.GetChild(i).gameObject);
            obj.transform.DetachChildren();
        }

        public static void ClearChildren(Component com)
        {
            if (com == null) return;
            ClearChildren(com.gameObject);
        }

        public static void HideChildren<T>(GameObject obj)
        {
            if (obj == null) return;
            for (int i = 0; i < obj.transform.childCount; ++i)
            {
                if (obj.transform.GetChild(i).GetComponent<T>() == null) continue;
                obj.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public static void HideChildren<T>(Component com)
        {
            if (com == null) return;
            HideChildren<T>(com.gameObject);
        }

        public static string SplitContentByIdxM(string soruce_,int idx_)
        {
            string[] tempList = soruce_.Split(';');
            if (tempList.Length >= idx_ && idx_ > 0)
                return tempList[idx_ - 1];
            return null;
        }
    }
}