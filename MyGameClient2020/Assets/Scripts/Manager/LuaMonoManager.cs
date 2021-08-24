using UnityEngine;
using XLua;
using System.IO;
using System.Collections;
using System;
using Common.General;

namespace Manager
{
    public class LuaMonoManager : TSingletonMono<LuaMonoManager>
    {
        public  LuaEnv luaenv = null;

        private DelegateFloat timerUpdate;
        private byte[] CustomLoaderMethod(ref string fileName)
        {
            //GameApp.Instance.UpdateLoading();
#if USE_PACKAGE
            string luaName = fileName.Replace(".", "/");
            luaName = luaName.Substring(luaName.LastIndexOf("/") + 1);
            //Common.Log("-------Load lua from AssetBundls-------------" + luaName);
            TextAsset tempText = ResourcesManager.Instance.LoadLuaFromAssetBundleM(luaName);
            if (null == tempText)
            {
                Common.Log("fileName::" + fileName + " not find!!!");
                return null;
            }
            return tempText.bytes;
#else
            string luaMainString = Application.dataPath + "/LuaScripts/" + fileName.Replace(".", "/") + ".lua";
            //Common.Log("--------Load Lua -----------"+ luaMainString);
            if (File.Exists(luaMainString))
            {
                return File.ReadAllBytes(luaMainString);
            }
            else
            {
                return null;
            }

            //TextAsset ta = Resources.Load<TextAsset>("LuaScripts/" + fileName.Replace(".", "/")+ ".lua");
            //if(ta!=null)
            //{
            //    return ta.bytes;
            //}
            //else
            //{
            //    Common.Log("fileName::"+ fileName+" not find!!!");
            //    return null;
            //}
#endif
           

        }

        private byte[] LoadLuaData(string fileName_)
        {
            string luaName = fileName_.Replace(".", "/");
            luaName = luaName.Substring(luaName.LastIndexOf("/")+1);
            TextAsset tempText = null; //ResourcesManager.Instance.LoadLuaFromAssetBundleM(luaName);
            return tempText.bytes;

        }
        public void Initialize()
        {
            if (null != luaenv)
                return;

            luaenv = new LuaEnv();
            LuaEnv.CustomLoader method = CustomLoaderMethod;
            luaenv.AddLoader(method);
#if USE_PACKAGE
            TextAsset tempText = ResourcesManager.Instance.LoadLuaFromAssetBundleM("GameInit");
            luaenv.DoString(tempText.bytes);
#else
            luaenv.DoString(@" require('myLua/Game/Init/GameInit')");
#endif
            //TextAsset tempText = ResourcesManager.Instance.LoadLuaFromAssetBundleM("GameInit");
            //Common.Log("-----Lua GameInit-----"+tempText.ToString());
            //// luaenv.DoString(@" require('myLua/Game/Init/GameInit')");
            //luaenv.DoString(tempText.bytes);

#if USE_LOG
            LuaMonoManager.Instance.CallLuaFunBool("SetUseLogFlag", true);//
#else
            LuaMonoManager.Instance.CallLuaFunBool("SetUseLogFlag", false);//
#endif
            #region UI 背景自适应问题
            //float tempCurWHValue = (float)Screen.width / (float)Screen.height;
            //int tempUIOffsetValue = -1;
            //if (LocalDBManager.Instance.HasPlayerPerfsKey(PLAYERPERFS.UI_OFFSETVALUE))
            //    tempUIOffsetValue = LocalDBManager.Instance.GetPlayerIntPerfs(PLAYERPERFS.UI_OFFSETVALUE);
            //if (tempUIOffsetValue == -1)//没有记录，则根据实际分辨率自动调整
            //{
            //    Debug.Log("------------------tempUIOffsetValue-------111-----------");
            //    float tempWHValue = 16f / 9f;
            //    if (Math.Round(tempCurWHValue, 2) != Math.Round(tempWHValue, 2))
            //    {
            //        //Debug.Log("------------------16:9-Error------------------");
            //        LuaMonoManager.Instance.CallLuaFunInt("SetUIOffsetValue", 100);//
            //    }
            //    else
            //    {
            //        //Debug.Log("------------------16:9-OK------------------");
            //        LuaMonoManager.Instance.CallLuaFunInt("SetUIOffsetValue", 0);//
            //    }
            //}
            //else
            //{
            //    Debug.Log("------------------tempUIOffsetValue-------222-----------tempUIOffsetValue = "+ tempUIOffsetValue);
            //    LuaMonoManager.Instance.CallLuaFunInt("SetUIOffsetValue", tempUIOffsetValue);//
            //}
            #endregion
            timerUpdate = luaenv.Global.GetInPath<DelegateFloat>("TimeController.Update");
        }
        public LuaTable CreateLuaTable()
        {
            return luaenv.NewTable();
        }

        public IEnumerable GetIEnumerable(string str)
        {
            return luaenv.Global.GetKeys();
        }
        public object[] CallLuaFunction(string name, params object[] args)
        {
            return luaenv.CallLuaFunction(name, args);
        }
        void Update()
        {
            if (luaenv != null)
            {
                luaenv.Tick();
            }
            if (timerUpdate != null)
            {
                timerUpdate(Time.deltaTime);
            }
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F12))
            {
                ReExeLua();
            }
#endif
        }

        public void Close()
        {
            timerUpdate = null;
            luaenv.Dispose();
        }

        public void CallLuaFuncNoPara(string luaFunPath)  // e.g GlobalNS.UIMain.func   to call func() and the self para is nil
        {
            //Common.Log("---------------CallLuaFuncNoPara---------------" + luaFunPath);
            DelegateNone fun = luaenv.Global.GetInPath<DelegateNone>(luaFunPath);
            fun();
        }

        public string CallLuaFuncNoPara_s(string luaFunPath)
        {
            //Common.Log("---------------CallLuaFuncNoPara_s---------------" + luaFunPath);
            DelegateNone_s fun = luaenv.Global.GetInPath<DelegateNone_s>(luaFunPath);
            return fun();
        }

        public void CallLuaFunBool(string luaFunPath,bool b)
        {
            DelegateBool fun = luaenv.Global.GetInPath<DelegateBool>(luaFunPath);
            fun(b);
        }

        /// <summary>
        /// 调用双字符串参数的委托
        /// </summary>
        /// <param name="luaFunPath"></param>
        /// <param name="parm1"></param>
        /// <param name="parm2"></param>
        public void CallLuaFunDoubleStringParm(string luaFunPath, string parm1, string parm2)
        {
           // Common.Log("---------------CallLuaFunDoubleStringParm---------------" + luaFunPath);
            DelegateDoubleString fun = luaenv.Global.GetInPath<DelegateDoubleString>(luaFunPath);
            fun(parm1,parm2);
        }

        public void CallLuaFunInt(string luaFunPath, int b)
        {
           // Common.Log("---------------CallLuaFunInt---------------" + luaFunPath);
            DelegateInt fun = luaenv.Global.GetInPath<DelegateInt>(luaFunPath);
            fun(b);
        }
        
        public void CallLuaFunString(string luaFunPath, string b)//ljs add
        {
            //Common.Log("---------------CallLuaFunString---------------" + luaFunPath);
            DelegateString fun = luaenv.Global.GetInPath<DelegateString>(luaFunPath);
            fun(b);
        }
        public string CallLuaFuncNoPara_ss(string luaFunPath, out string v)
        {
            //Common.Log("---------------CallLuaFuncNoPara_ss---------------" + luaFunPath);
            DelegateNone_ss fun = luaenv.Global.GetInPath<DelegateNone_ss>(luaFunPath);
            return fun(out v);
        }

        public void LuaForceGC()
        {
            luaenv.FullGc();//  same as call collectgarbage('collect') in lua
        }
        /// <summary>
        /// 重新编译lua脚本
        /// </summary>
        private void ReExeLua()
        {
            LuaMonoManager.Instance.CallLuaFuncNoPara("ReExeLuaByCSharp");
        }
    }
}