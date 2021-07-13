#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class MGameGeneralUnityDefineToLuaWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(MGame.General.UnityDefineToLua);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 4, 0);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UNITY_EDITOR", _g_get_UNITY_EDITOR);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UNITY_STANDALONE", _g_get_UNITY_STANDALONE);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UNITY_ANDROID", _g_get_UNITY_ANDROID);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UNITY_IOS", _g_get_UNITY_IOS);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "MGame.General.UnityDefineToLua does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UNITY_EDITOR(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, MGame.General.UnityDefineToLua.UNITY_EDITOR);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UNITY_STANDALONE(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, MGame.General.UnityDefineToLua.UNITY_STANDALONE);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UNITY_ANDROID(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, MGame.General.UnityDefineToLua.UNITY_ANDROID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UNITY_IOS(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, MGame.General.UnityDefineToLua.UNITY_IOS);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
