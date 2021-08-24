using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common.Msic
{
    public class GeneralMsic
    {
        private static Encoding encode = Encoding.UTF8;
        private static PointerEventData cursorEventData;
        private static List<RaycastResult> objectsHit;
        public static T CallNonPublicConstructor<T>(object[] param) where T : class
        {
            Type type = typeof(T);
            Type[] emptyTypes = new Type[0];
            ConstructorInfo ci = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, emptyTypes, null);
            if (ci == null)
                throw new InvalidOperationException(string.Format("class [{0}] must contain a private constructor", type.ToString()));
            var instance = ci.Invoke(param);
            T res = instance as T;
            return res;
        }


        public static object CallNonPublicMethod(object instance, string methodName, object[] param)
        {
            Type type = instance.GetType();
            MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);

            object result = null;
            try
            {
                result = method.Invoke(instance, param);
            }
            catch (TargetInvocationException ex)
            {
                Debug.LogException(ex);
            }
            return result;
        }

        #region unity


        public static void SetRectTransformParent(GameObject child, GameObject parent)
        {
            RectTransform rec = child.GetComponent<RectTransform>();
            if (rec != null)
            {
                rec.SetParent(parent.transform, false);
            }
            else
            {
                child.AddComponent<RectTransform>();
                child.GetComponent<RectTransform>().SetParent(parent.transform, false);
            }
        }
        public static void SetParent(GameObject go, GameObject parentGo, bool worldPositionStays)
        {
            if (go && parentGo)
            {
                go.transform.SetParent(parentGo.transform, worldPositionStays);
            }
        }

        public static GameObject NewGameObject(Vector3 Pos, GameObject parentGo, string name)
        {
            GameObject go = new GameObject(name);
            if (parentGo)
            {
                go.transform.parent = parentGo.transform;
            }
            go.transform.localPosition = Pos;
            return go;
        }

        public static Vector3 TransformLocalPosToWorldPos(Transform t, Vector3 localPos)
        {
            if (!t)
            {
                return localPos;
            }
            return t.TransformPoint(localPos);
        }

        public static Transform FindChild(Transform root, string name, bool includeinactive = false)
        {
            var childs = root.GetComponentsInChildren<Transform>(includeinactive);
            Transform trans = null;
            for (int i = 0; i < childs.Length; ++i)
            {
                trans = childs[i];
                if (trans.name == name)
                    return childs[i];
            }
            return null;
        }


        public static Transform[] FindChilds(Transform root, bool includeInactive)
        {
            Transform[] childs = new Transform[root.childCount];
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i] = root.GetChild(i);
            }
            return childs;
        }

        public static GameObject FindGameObject(string name)
        {
            return GameObject.Find(name);
        }

        public static void DestroyAllChilds(GameObject obj)
        {
            int childCount = obj.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject.Destroy(obj.transform.GetChild(0).gameObject);
            }
        }

        // 通过完全路径获取
        static public GameObject FindChildByPath(GameObject pObject, string path)
        {
            Transform trans = null;
            trans = pObject.transform.Find(path);
            if (trans != null)
            {
                return trans.gameObject;
            }

            return null;
        }

        public static GameObject FindByPath(string absolutePathInScene)
        {
            string rootName = GeneralMsic.StringTrim(absolutePathInScene, "", "/");
            string subPath = GeneralMsic.StringTrim(absolutePathInScene, "/", "");
            GameObject root = GameObject.Find(rootName);
            if (root)
            {
                Transform tr = root.transform.Find(subPath);
                if (tr)
                {
                    return tr.gameObject;
                }
                else
                {
                    Debug.LogError(string.Format("file \"{0}\" not found ", absolutePathInScene));
                }
            }
            else
            {
                Debug.LogError(string.Format("root file \"{0}\" not found with file Path: \"{1}\" ", rootName, absolutePathInScene));
            }
            return null;
        }

        public static void SetObjectToLayer(GameObject go, string layerName, bool allChildsAlso)
        {
            if (go)
            {
                if (!allChildsAlso)
                {
                    go.layer = LayerMask.NameToLayer(layerName);
                    return;
                }

                Transform[] trans = go.transform.GetComponentsInChildren<Transform>(true);
                for (int i = 0; i < trans.Length; i++)
                {
                    trans[i].gameObject.layer = LayerMask.NameToLayer(layerName);
                }
            }
        }

        public static GameObject CopyRectTransformToParent(GameObject go, Transform toParent)
        {
            GameObject newGo = null;
            if (go && toParent)
            {
                RectTransform goRT = go.GetComponent<RectTransform>();
                newGo = GameObject.Instantiate(go);
                newGo.transform.SetParent(toParent, false);
                RectTransform newGoRT = newGo.GetComponent<RectTransform>();
                newGoRT.anchorMin = Vector2.one * 0.5f;
                newGoRT.anchorMax = Vector2.one * 0.5f;
                newGoRT.sizeDelta = new Vector2(goRT.rect.width, goRT.rect.height);
                newGoRT.localScale = goRT.localScale;

            }
            return newGo;
        }


        public static Texture2D ScaleTexture(Texture2D source, int targetWigth, int targetheight)
        {
            if (source.width == targetWigth && source.height == targetheight)
            {
                return source;
            }
            Texture2D scaleTexture = new Texture2D(targetWigth, targetheight, source.format, false);

            for (int i = 0; i < scaleTexture.height; ++i)
            {
                for (int j = 0; j < scaleTexture.width; ++j)
                {
                    Color newColor = source.GetPixelBilinear((float)j / (float)scaleTexture.width, (float)i / (float)scaleTexture.height);
                    scaleTexture.SetPixel(j, i, newColor);
                }
            }
            scaleTexture.Apply();
            return scaleTexture;
        }

        public static Texture2D ScaleTexture(Texture2D source, float scale)
        {
            int width = Mathf.Max(1, (int)(source.width * scale));
            int height = Mathf.Max(1, (int)(source.height * scale));
            return ScaleTexture(source, width, height);
        }

        public static void UnloadAssets(UnityEngine.Object o)
        {
            Resources.UnloadAsset(o);
        }

        public static void UnloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
        }

        public static void GCRelease()
        {
            GC.Collect();
        }

        #endregion

        #region IO
        public static void WriteFileAtEnd(string fileFullPathName, string content, bool reCreate = false)
        {
            string path = Path.GetDirectoryName(fileFullPathName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Encoding encoder = encode;
            byte[] bytes = encoder.GetBytes(content);
            FileStream fs = null;
            try
            {
                fs = File.Open(fileFullPathName, reCreate ? FileMode.Create : FileMode.Append);
            }
            catch (Exception ex)
            {
                Debug.Log("open file: " + fileFullPathName + " filed: " + ex.ToString());
            }

            try
            {
                if (fs != null)
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.Log("write file: " + fileFullPathName + " filed: " + ex.ToString());
            }
        }


        public static string ReadFileTrim(string fileFullPathName, string start, string end)
        {
            string path = Path.GetDirectoryName(fileFullPathName);
            if (!Directory.Exists(path))
            {
                return "";
            }
            if (!File.Exists(fileFullPathName))
            {
                return "";
            }
            string txt = File.ReadAllText(fileFullPathName);
            return StringTrim(txt, start, end);
        }


        public static string[] ReadFileLinesTrim(string fileFullPathName, string start, string end)
        {
            string path = Path.GetDirectoryName(fileFullPathName);
            if (!Directory.Exists(path))
            {
                return null;
            }
            if (!File.Exists(fileFullPathName))
            {
                return null;
            }

            string[] txts = File.ReadAllLines(fileFullPathName);
            if (txts == null)
            {
                return null;
            }
            for (int i = 0; i < txts.Length; i++)
            {
                txts[i] = StringTrim(txts[i], start, end);
            }
            return txts;
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public static string StringTrim(string txt, string start, string end, bool lastIndex = false)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return "";
            }

            int idxStart = 0;

            if (!string.IsNullOrEmpty(start))
            {
                idxStart = (lastIndex ? txt.LastIndexOf(start) : txt.IndexOf(start)) + start.Length;
                if (idxStart < 0)
                {
                    return "";
                }
            }
            txt = txt.Substring(idxStart, txt.Length - idxStart);
            int idxEnd = txt.Length;
            if (!string.IsNullOrEmpty(end))
            {

                idxEnd = (lastIndex ? txt.LastIndexOf(end) : txt.IndexOf(end));
                if (idxEnd < 0)
                {
                    return "";
                }
            }
            return txt.Substring(0, idxEnd);
        }

        public static string StringInsert(string str, string insertStr, int index)
        {
            if (string.IsNullOrEmpty(str))
            {
                return insertStr;
            }

            if (index < 0)
            {
                return insertStr + str;
            }

            if (index > str.Length)
            {
                return str + insertStr;
            }
            string leftStr = str.Substring(0, index);
            string rightStr = str.Substring(index, str.Length - index);
            return leftStr + insertStr + rightStr;
        }


        public static byte[] ReadBinaryFileOffset(string filePathName, int offset, int length)
        {
            FileStream fs = null;
            BinaryReader br = null;
            if (File.Exists(filePathName) == false)
            {
                return null;
            }

            try
            {
                fs = new FileStream(filePathName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                byte[] b = new byte[1024];
                MemoryStream ms = new MemoryStream();
                int count = 0;
                int remainLength = length;
                fs.Position = offset;
                while (count < length && count < fs.Length)
                {
                    int toReadLength = Mathf.Min(remainLength, b.Length);

                    int read = br.Read(b, 0, toReadLength);
                    count += read;
                    remainLength -= read;
                    ms.Write(b, 0, read);
                }
                br.Close();
                fs.Close();
                br = null;
                fs = null;
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
            return null;
        }
        public static byte[] ReadBinaryFile(string filePathName)
        {
            FileStream fs = null;
            BinaryReader br = null;
            if (File.Exists(filePathName) == false)
            {
                return null;
            }

            try
            {
                fs = new FileStream(filePathName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                byte[] b = new byte[1024];
                MemoryStream ms = new MemoryStream();
                int count = 0;
                while (count < fs.Length)
                {
                    int read = br.Read(b, 0, b.Length);
                    count += read;
                    ms.Write(b, 0, read);
                }
                br.Close();
                fs.Close();
                br = null;
                fs = null;
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
            return null;
        }

        public static bool WriteBinaryFile(string filePathName, byte[] buff, FileMode fileMode = FileMode.Create)
        {
            bool success = true;
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                string path = Path.GetDirectoryName(filePathName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fs = new FileStream(filePathName, fileMode, FileAccess.Write);
                bw = new BinaryWriter(fs);
                bw.Write(buff);
                bw.Close();
                fs.Close();
                bw = null;
                fs = null;
            }
            catch (Exception ex)
            {
                success = false;
                Debug.LogError(ex.ToString());
            }
            finally
            {
                if (bw != null)
                {
                    try
                    {
                        bw.Close();
                        bw = null;
                    }
                    catch
                    {
                    }
                }
                if (fs != null)
                {
                    try
                    {
                        fs.Close();
                        fs = null;
                    }
                    catch
                    {
                    }
                }
            }

            return success;
        }

        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        public static string StringToMD5Hash(string str)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        #endregion

        #region Encrypt

        public static string EncryptString(string str, string key, bool handLastPoint = false)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            int lastPointIndex = -1;
            if (handLastPoint)
            {
                lastPointIndex = str.LastIndexOf(".");
                if (lastPointIndex >= 0)
                {
                    str = StringTrim(str, "", ".", true) + StringTrim(str, ".", "", true);
                }
            }

            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] encryptData = Encrypt(data, key);
            str = System.Text.Encoding.Default.GetString(encryptData);
            if (lastPointIndex >= 0)
            {
                str = StringInsert(str, ".", lastPointIndex);
            }
            return str;
        }

        public static string DecryptString(string str, string key, bool handLastPoint = false)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            int lastPointIndex = -1;
            if (handLastPoint)
            {
                lastPointIndex = str.LastIndexOf(".");
                if (lastPointIndex >= 0)
                {
                    str = StringTrim(str, "", ".", true) + StringTrim(str, ".", "", true);
                }
            }

            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] decryptData = Decrypt(data, key);
            str = System.Text.Encoding.Default.GetString(decryptData);
            if (lastPointIndex >= 0)
            {
                str = StringInsert(str, ".", lastPointIndex);
            }
            return str;
        }


        public static void EncryptFile(string filePathName, string fileKey, string nameKey, bool handNameLastPoint = false)
        {
            if (!File.Exists(filePathName))
            {
                return;
            }

            string fileName = Path.GetFileName(filePathName);
            string encryName = GeneralMsic.EncryptString(fileName, nameKey, handNameLastPoint);
            string toFilePathName = filePathName.Replace(fileName, encryName);

            byte[] data = ReadBinaryFile(filePathName);
            byte[] encryptData = Encrypt(data, fileKey);
            if (WriteBinaryFile(toFilePathName, encryptData))
            {
                File.Delete(filePathName);
            }
        }

        public static void DecryptFile(string filePathName, string fileKey, string nameKey, bool handNameLastPoint = false)
        {
            if (!File.Exists(filePathName))
            {
                return;
            }
            string fileName = Path.GetFileName(filePathName);
            string decryName = GeneralMsic.DecryptString(fileName, nameKey, handNameLastPoint);
            string toFilePathName = filePathName.Replace(fileName, decryName);

            byte[] data = ReadBinaryFile(filePathName);
            byte[] decryptData = Decrypt(data, fileKey);
            if (WriteBinaryFile(toFilePathName, decryptData))
            {
                File.Delete(filePathName);
            }
        }


        public static byte[] Encrypt(byte[] data, string key)
        {
            try
            {
                int keyLen = key.Length;
                int[] keyNums = new int[keyLen];
                for (int i = 0; i < key.Length; i++)
                {
                    keyNums[i] = key[i] % keyLen;
                }

                int index = 0;
                int preIndex;
                int excIndex;
                byte tmpByte;
                int dataLeftLength;
                while (index < data.Length)
                {
                    for (int i = 0; i < keyLen; i++)
                    {
                        preIndex = i + index;
                        if (preIndex >= data.Length)
                        {
                            break;
                        }

                        dataLeftLength = data.Length - index;
                        excIndex = keyNums[i] + index;
                        if (excIndex >= data.Length)
                        {
                            excIndex = keyNums[i] % dataLeftLength + index;
                        }

                        if (preIndex != excIndex)
                        {
                            tmpByte = data[preIndex];
                            data[preIndex] = data[excIndex];
                            data[excIndex] = tmpByte;
                        }
                    }

                    index += keyLen;
                }

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error:" + ex.Message);
            }

        }


        public static byte[] Decrypt(byte[] data, string key)
        {
            try
            {
                int keyLen = key.Length;
                int[] keyNums = new int[keyLen];
                for (int i = 0; i < key.Length; i++)
                {
                    keyNums[i] = key[i] % keyLen;
                }

                int index = 0;
                int preIndex;
                int excIndex;
                byte tmpByte;
                int dataLeftLength;
                while (index < data.Length)
                {
                    for (int i = keyLen - 1; i >= 0; i--)
                    {
                        preIndex = i + index;
                        if (preIndex >= data.Length)
                        {
                            continue;
                        }

                        dataLeftLength = data.Length - index;
                        excIndex = keyNums[i] + index;
                        if (excIndex >= data.Length)
                        {
                            excIndex = keyNums[i] % dataLeftLength + index;
                        }

                        if (preIndex != excIndex)
                        {
                            tmpByte = data[preIndex];
                            data[preIndex] = data[excIndex];
                            data[excIndex] = tmpByte;
                        }
                    }

                    index += keyLen;
                }

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error:" + ex.Message);
            }
        }

        #endregion

        #region UI
        public static bool CheckUIOp(Vector3 touchPosition)
        {
            if (cursorEventData == null)
                cursorEventData = new PointerEventData(EventSystem.current);
            cursorEventData.position = touchPosition;

            if (objectsHit == null)
            {
                objectsHit = new List<RaycastResult>();
            }
            objectsHit.Clear();
            EventSystem.current.RaycastAll(cursorEventData, objectsHit);

            if (objectsHit.Count > 0)
                return true;
            return false;
        }

        #endregion

        #region c#
        public static System.DateTime TimeStampsToDataTime(long timeStamps)
        {
            double d = (double)timeStamps;
            System.DateTime startTime = new System.DateTime(1970, 1, 1);
            System.DateTime dt = startTime.AddMilliseconds(d);
            return dt;
        }

        public static bool IsValidMailAddress(string str)
        {
            if (str == "")
            {
                return true;
            }

            if (string.IsNullOrEmpty(str) || str.Length < 5)
            {
                return false;
            }

            if (!str.Contains("@") || !str.Contains("."))
            {
                return false;
            }

            int atIdx = str.IndexOf("@");
            int ptIdx = str.IndexOf(".");
            if (atIdx > ptIdx || atIdx == 0 || ptIdx == str.Length - 1)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region msic
        public static string GetGameObjectPath(Transform t)
        {
            if (t == null)
            {
                return "GetGameObjectPath transform is null";
            }
            string s = t.name;
            Transform p = t;
            while (p.parent != null)
            {
                p = p.parent;
                s = p.name + "/" + s;
            }
            return s;
        }

        #endregion

    }
}