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
    public class MGameMsicGeneralMsicWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(MGame.Msic.GeneralMsic);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 39, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CallNonPublicMethod", _m_CallNonPublicMethod_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetRectTransformParent", _m_SetRectTransformParent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetParent", _m_SetParent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "NewGameObject", _m_NewGameObject_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TransformLocalPosToWorldPos", _m_TransformLocalPosToWorldPos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindChild", _m_FindChild_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindChilds", _m_FindChilds_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindGameObject", _m_FindGameObject_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DestroyAllChilds", _m_DestroyAllChilds_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindChildByPath", _m_FindChildByPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindByPath", _m_FindByPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetObjectToLayer", _m_SetObjectToLayer_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyRectTransformToParent", _m_CopyRectTransformToParent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ScaleTexture", _m_ScaleTexture_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnloadAssets", _m_UnloadAssets_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnloadUnusedAssets", _m_UnloadUnusedAssets_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GCRelease", _m_GCRelease_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteFileAtEnd", _m_WriteFileAtEnd_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFileTrim", _m_ReadFileTrim_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFileLinesTrim", _m_ReadFileLinesTrim_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DeleteFile", _m_DeleteFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StringTrim", _m_StringTrim_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StringInsert", _m_StringInsert_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadBinaryFileOffset", _m_ReadBinaryFileOffset_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadBinaryFile", _m_ReadBinaryFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteBinaryFile", _m_WriteBinaryFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetMD5HashFromFile", _m_GetMD5HashFromFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StringToMD5Hash", _m_StringToMD5Hash_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EncryptString", _m_EncryptString_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DecryptString", _m_DecryptString_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EncryptFile", _m_EncryptFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DecryptFile", _m_DecryptFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Encrypt", _m_Encrypt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Decrypt", _m_Decrypt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CheckUIOp", _m_CheckUIOp_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TimeStampsToDataTime", _m_TimeStampsToDataTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsValidMailAddress", _m_IsValidMailAddress_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetGameObjectPath", _m_GetGameObjectPath_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					MGame.Msic.GeneralMsic gen_ret = new MGame.Msic.GeneralMsic();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CallNonPublicMethod_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    object _instance = translator.GetObject(L, 1, typeof(object));
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    object[] _param = (object[])translator.GetObject(L, 3, typeof(object[]));
                    
                        object gen_ret = MGame.Msic.GeneralMsic.CallNonPublicMethod( _instance, _methodName, _param );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRectTransformParent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _child = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    UnityEngine.GameObject _parent = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    MGame.Msic.GeneralMsic.SetRectTransformParent( _child, _parent );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetParent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    UnityEngine.GameObject _parentGo = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    bool _worldPositionStays = LuaAPI.lua_toboolean(L, 3);
                    
                    MGame.Msic.GeneralMsic.SetParent( _go, _parentGo, _worldPositionStays );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NewGameObject_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Vector3 _Pos;translator.Get(L, 1, out _Pos);
                    UnityEngine.GameObject _parentGo = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    string _name = LuaAPI.lua_tostring(L, 3);
                    
                        UnityEngine.GameObject gen_ret = MGame.Msic.GeneralMsic.NewGameObject( _Pos, _parentGo, _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TransformLocalPosToWorldPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Transform _t = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    UnityEngine.Vector3 _localPos;translator.Get(L, 2, out _localPos);
                    
                        UnityEngine.Vector3 gen_ret = MGame.Msic.GeneralMsic.TransformLocalPosToWorldPos( _t, _localPos );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindChild_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Transform>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Transform _root = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    string _name = LuaAPI.lua_tostring(L, 2);
                    bool _includeinactive = LuaAPI.lua_toboolean(L, 3);
                    
                        UnityEngine.Transform gen_ret = MGame.Msic.GeneralMsic.FindChild( _root, _name, _includeinactive );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityEngine.Transform _root = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Transform gen_ret = MGame.Msic.GeneralMsic.FindChild( _root, _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic.FindChild!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindChilds_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Transform _root = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    bool _includeInactive = LuaAPI.lua_toboolean(L, 2);
                    
                        UnityEngine.Transform[] gen_ret = MGame.Msic.GeneralMsic.FindChilds( _root, _includeInactive );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindGameObject_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.GameObject gen_ret = MGame.Msic.GeneralMsic.FindGameObject( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyAllChilds_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _obj = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    
                    MGame.Msic.GeneralMsic.DestroyAllChilds( _obj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindChildByPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _pObject = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.GameObject gen_ret = MGame.Msic.GeneralMsic.FindChildByPath( _pObject, _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindByPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _absolutePathInScene = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.GameObject gen_ret = MGame.Msic.GeneralMsic.FindByPath( _absolutePathInScene );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetObjectToLayer_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    string _layerName = LuaAPI.lua_tostring(L, 2);
                    bool _allChildsAlso = LuaAPI.lua_toboolean(L, 3);
                    
                    MGame.Msic.GeneralMsic.SetObjectToLayer( _go, _layerName, _allChildsAlso );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyRectTransformToParent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    UnityEngine.Transform _toParent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                        UnityEngine.GameObject gen_ret = MGame.Msic.GeneralMsic.CopyRectTransformToParent( _go, _toParent );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ScaleTexture_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Texture2D>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    UnityEngine.Texture2D _source = (UnityEngine.Texture2D)translator.GetObject(L, 1, typeof(UnityEngine.Texture2D));
                    float _scale = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        UnityEngine.Texture2D gen_ret = MGame.Msic.GeneralMsic.ScaleTexture( _source, _scale );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Texture2D>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Texture2D _source = (UnityEngine.Texture2D)translator.GetObject(L, 1, typeof(UnityEngine.Texture2D));
                    int _targetWigth = LuaAPI.xlua_tointeger(L, 2);
                    int _targetheight = LuaAPI.xlua_tointeger(L, 3);
                    
                        UnityEngine.Texture2D gen_ret = MGame.Msic.GeneralMsic.ScaleTexture( _source, _targetWigth, _targetheight );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic.ScaleTexture!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnloadAssets_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _o = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    MGame.Msic.GeneralMsic.UnloadAssets( _o );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnloadUnusedAssets_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    MGame.Msic.GeneralMsic.UnloadUnusedAssets(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GCRelease_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    MGame.Msic.GeneralMsic.GCRelease(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteFileAtEnd_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _fileFullPathName = LuaAPI.lua_tostring(L, 1);
                    string _content = LuaAPI.lua_tostring(L, 2);
                    bool _reCreate = LuaAPI.lua_toboolean(L, 3);
                    
                    MGame.Msic.GeneralMsic.WriteFileAtEnd( _fileFullPathName, _content, _reCreate );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _fileFullPathName = LuaAPI.lua_tostring(L, 1);
                    string _content = LuaAPI.lua_tostring(L, 2);
                    
                    MGame.Msic.GeneralMsic.WriteFileAtEnd( _fileFullPathName, _content );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic.WriteFileAtEnd!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFileTrim_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _fileFullPathName = LuaAPI.lua_tostring(L, 1);
                    string _start = LuaAPI.lua_tostring(L, 2);
                    string _end = LuaAPI.lua_tostring(L, 3);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.ReadFileTrim( _fileFullPathName, _start, _end );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFileLinesTrim_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _fileFullPathName = LuaAPI.lua_tostring(L, 1);
                    string _start = LuaAPI.lua_tostring(L, 2);
                    string _end = LuaAPI.lua_tostring(L, 3);
                    
                        string[] gen_ret = MGame.Msic.GeneralMsic.ReadFileLinesTrim( _fileFullPathName, _start, _end );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _filePath = LuaAPI.lua_tostring(L, 1);
                    
                    MGame.Msic.GeneralMsic.DeleteFile( _filePath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StringTrim_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _txt = LuaAPI.lua_tostring(L, 1);
                    string _start = LuaAPI.lua_tostring(L, 2);
                    string _end = LuaAPI.lua_tostring(L, 3);
                    bool _lastIndex = LuaAPI.lua_toboolean(L, 4);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.StringTrim( _txt, _start, _end, _lastIndex );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _txt = LuaAPI.lua_tostring(L, 1);
                    string _start = LuaAPI.lua_tostring(L, 2);
                    string _end = LuaAPI.lua_tostring(L, 3);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.StringTrim( _txt, _start, _end );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic.StringTrim!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StringInsert_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    string _insertStr = LuaAPI.lua_tostring(L, 2);
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.StringInsert( _str, _insertStr, _index );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadBinaryFileOffset_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _filePathName = LuaAPI.lua_tostring(L, 1);
                    int _offset = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    
                        byte[] gen_ret = MGame.Msic.GeneralMsic.ReadBinaryFileOffset( _filePathName, _offset, _length );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadBinaryFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _filePathName = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] gen_ret = MGame.Msic.GeneralMsic.ReadBinaryFile( _filePathName );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteBinaryFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.FileMode>(L, 3)) 
                {
                    string _filePathName = LuaAPI.lua_tostring(L, 1);
                    byte[] _buff = LuaAPI.lua_tobytes(L, 2);
                    System.IO.FileMode _fileMode;translator.Get(L, 3, out _fileMode);
                    
                        bool gen_ret = MGame.Msic.GeneralMsic.WriteBinaryFile( _filePathName, _buff, _fileMode );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _filePathName = LuaAPI.lua_tostring(L, 1);
                    byte[] _buff = LuaAPI.lua_tobytes(L, 2);
                    
                        bool gen_ret = MGame.Msic.GeneralMsic.WriteBinaryFile( _filePathName, _buff );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic.WriteBinaryFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMD5HashFromFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _fileName = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.GetMD5HashFromFile( _fileName );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StringToMD5Hash_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.StringToMD5Hash( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EncryptString_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    string _key = LuaAPI.lua_tostring(L, 2);
                    bool _handLastPoint = LuaAPI.lua_toboolean(L, 3);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.EncryptString( _str, _key, _handLastPoint );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.EncryptString( _str, _key );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic.EncryptString!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DecryptString_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    string _key = LuaAPI.lua_tostring(L, 2);
                    bool _handLastPoint = LuaAPI.lua_toboolean(L, 3);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.DecryptString( _str, _key, _handLastPoint );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = MGame.Msic.GeneralMsic.DecryptString( _str, _key );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic.DecryptString!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EncryptFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _filePathName = LuaAPI.lua_tostring(L, 1);
                    string _fileKey = LuaAPI.lua_tostring(L, 2);
                    string _nameKey = LuaAPI.lua_tostring(L, 3);
                    bool _handNameLastPoint = LuaAPI.lua_toboolean(L, 4);
                    
                    MGame.Msic.GeneralMsic.EncryptFile( _filePathName, _fileKey, _nameKey, _handNameLastPoint );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _filePathName = LuaAPI.lua_tostring(L, 1);
                    string _fileKey = LuaAPI.lua_tostring(L, 2);
                    string _nameKey = LuaAPI.lua_tostring(L, 3);
                    
                    MGame.Msic.GeneralMsic.EncryptFile( _filePathName, _fileKey, _nameKey );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic.EncryptFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DecryptFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _filePathName = LuaAPI.lua_tostring(L, 1);
                    string _fileKey = LuaAPI.lua_tostring(L, 2);
                    string _nameKey = LuaAPI.lua_tostring(L, 3);
                    bool _handNameLastPoint = LuaAPI.lua_toboolean(L, 4);
                    
                    MGame.Msic.GeneralMsic.DecryptFile( _filePathName, _fileKey, _nameKey, _handNameLastPoint );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _filePathName = LuaAPI.lua_tostring(L, 1);
                    string _fileKey = LuaAPI.lua_tostring(L, 2);
                    string _nameKey = LuaAPI.lua_tostring(L, 3);
                    
                    MGame.Msic.GeneralMsic.DecryptFile( _filePathName, _fileKey, _nameKey );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to MGame.Msic.GeneralMsic.DecryptFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Encrypt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _data = LuaAPI.lua_tobytes(L, 1);
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        byte[] gen_ret = MGame.Msic.GeneralMsic.Encrypt( _data, _key );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Decrypt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _data = LuaAPI.lua_tobytes(L, 1);
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        byte[] gen_ret = MGame.Msic.GeneralMsic.Decrypt( _data, _key );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CheckUIOp_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Vector3 _touchPosition;translator.Get(L, 1, out _touchPosition);
                    
                        bool gen_ret = MGame.Msic.GeneralMsic.CheckUIOp( _touchPosition );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TimeStampsToDataTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    long _timeStamps = LuaAPI.lua_toint64(L, 1);
                    
                        System.DateTime gen_ret = MGame.Msic.GeneralMsic.TimeStampsToDataTime( _timeStamps );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsValidMailAddress_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = MGame.Msic.GeneralMsic.IsValidMailAddress( _str );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGameObjectPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Transform _t = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    
                        string gen_ret = MGame.Msic.GeneralMsic.GetGameObjectPath( _t );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
