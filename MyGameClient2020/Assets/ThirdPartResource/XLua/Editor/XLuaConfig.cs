using System.Collections.Generic;
using System;
using UnityEngine;
using XLua;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using System.Reflection;
//using System.Linq;

//配置的详细介绍请看Doc下《XLua的配置.doc》
public static class XLuaConfig
{
    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [LuaCallCSharp]
    [ReflectionUse]
    public static List<Type> LuaCallCSharp = new List<Type>() {

        typeof(System.Collections.ArrayList),
        typeof(System.DateTime),
        typeof(System.TimeSpan),
        typeof(System.Object),

        typeof(Application),
        typeof(Time),
        typeof(Screen),
        typeof(SleepTimeout),
        typeof(Input),
        typeof(Resources),
        typeof(Physics),
        typeof(RenderSettings),
        typeof(QualitySettings),
        typeof(GL),
        typeof(Graphics),
        typeof(Component),
        typeof(Transform),
        typeof(Material),
        typeof(Light),
        typeof(Rigidbody),
        typeof(Camera),
        typeof(AudioSource),



        typeof(Debug),
        typeof(Rect),
        typeof(RectTransform),
        typeof(MonoBehaviour),
        typeof(UnityEngine.Object),
        typeof(GameObject),
        typeof(CameraClearFlags),
        typeof(Renderer),
        typeof(MeshFilter),
        typeof(MeshRenderer),
        typeof(SkinnedMeshRenderer),
        typeof(LightType),

        typeof(Physics),
        typeof(Collider),
        typeof(BoxCollider),
        typeof(MeshCollider),
        typeof(SphereCollider),
        typeof(CharacterController),
        typeof(LineRenderer),

        typeof(Animation),
        typeof(AnimationClip),
        typeof(TrackedReference),
        typeof(AnimationState),
        typeof(QueueMode),
        typeof(PlayMode),

        typeof(AudioClip),

        typeof(Application),
        typeof(Input),
        typeof(TouchPhase),
        typeof(KeyCode),
        typeof(Screen),
        typeof(RenderSettings),

        typeof(AsyncOperation),
       // typeof(AssetBundle),
        typeof(SkinWeights),
        typeof(QualitySettings),
        typeof(AnimationBlendMode),
        typeof(Texture),
        typeof(Texture2D),
        typeof(RenderTexture),
        typeof(ParticleSystem),

        typeof(Color),
        typeof(ColorBlock),
        typeof(Vector2),
        typeof(Vector3),
        typeof(Vector4),
        typeof(Quaternion),
        typeof(Animator),
        typeof(WaitForEndOfFrame),
        typeof(WaitForSeconds),
        typeof(WaitForFixedUpdate),

        typeof(Matrix4x4),
        typeof(ScrollRect),
        typeof(UIBehaviour),
        typeof(MaskableGraphic),
        typeof(Graphic),
        typeof(CanvasRenderer),

        typeof(ToggleGroup),
        typeof(Canvas),
        typeof(GraphicRaycaster),
        
        // UnityEngine.Events
        typeof(UnityEngine.Events.UnityEvent),
         typeof(Button),
        typeof(Button.ButtonClickedEvent),
        typeof(Image),
        typeof(RawImage),
        typeof(Image.Type),
        typeof(InputField),
        typeof(Selectable),
        typeof(Slider),
        typeof(Text),
        typeof(Toggle),

        typeof(RectTransformUtility),
        typeof(SpriteRenderer),
        typeof(Sprite),
        typeof(TextMesh),
        typeof(SystemInfo),
        typeof(PlayerPrefs),
        typeof(UnityEngine.RectTransform.Edge),
        typeof(Ray),
        typeof(Bounds),
        typeof(Ray2D),
        typeof(Time),
        typeof(Component),
        typeof(Behaviour),
        typeof(TextAsset),
        typeof(Keyframe),
        typeof(AnimationCurve),
        typeof(Renderer),
        typeof(Mathf),
        typeof(Debug),



        //Game
        typeof(MGame.General.LuaCallCSharpEvent),
        typeof(MGame.General.UnityEngineObjectExtention),
        typeof(MGame.General.CachedMono),
        typeof(MGame.General.UnityDefineToLua),
        typeof(MGame.General.TSingletonMono<>),
         //typeof(MGame.GameBattle.Manager.LogicAvatarManager),
        typeof(MGame.GameBattle.Manager.LogicManager),
        typeof(MGame.Msic.GeneralMsic),
};

    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>() {
                typeof(Action),
                typeof(Func<double, double, double>),
                typeof(Action<string>),
                typeof(Action<double>),
                typeof(UnityEngine.Events.UnityAction),
                typeof(System.Collections.IEnumerator),
                typeof(MGame.General.DelegateObj),
                typeof(MGame.General.DelegateBool),
                typeof(MGame.General.DelegateString),
                typeof(MGame.General.DelegateInt),
                typeof(MGame.General.DelegateFloat),
                typeof(MGame.General.DelegateNone),

                typeof(MGame.General.DelegateObj_s),
                typeof(MGame.General.DelegateBool_s),
                typeof(MGame.General.DelegateString_s),
                typeof(MGame.General.DelegateInt_s),
                typeof(MGame.General.DelegateFloat_s),
                typeof(MGame.General.DelegateNone_s),
                typeof(MGame.General.DelegateNone_ss),

                 typeof(Dictionary<string, int>),
            };


    /// <summary>
    /// 统一配置 自定义需要优化GC 的结构（仅限xlua支持的结构）
    /// </summary>
    [GCOptimize]
    static List<Type> GCOptimize
    {
        get
        {
            return new List<Type>() {
                    //typeof(MGame.Core.GameState.GameStateEnum),
                };
        }
    }


    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
                new List<string>(){"System.Xml.XmlNodeList", "ItemOf"},
                new List<string>(){"UnityEngine.WWW", "movie"},
    #if UNITY_WEBGL
                new List<string>(){"UnityEngine.WWW", "threadPriority"},
    #endif
                new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
                new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
                new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},
                new List<string>(){"UnityEngine.Light", "areaSize"},
                new List<string>(){"UnityEngine.Light", "lightmapBakeType"},
                new List<string>(){"UnityEngine.Light", "SetLightDirty"},
                new List<string>(){"UnityEngine.Light", "shadowRadius"},
                new List<string>(){"UnityEngine.Light", "shadowAngle"},
                new List<string>(){"UnityEngine.QualitySettings", "streamingMipmapsRenderersPerFrame"},
                new List<string>(){"UnityEngine.Texture", "imageContentsHash"},

                new List<string>(){"UnityEngine.WWW", "MovieTexture"},
                new List<string>(){"UnityEngine.WWW", "GetMovieTexture"},
                new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
    #if !UNITY_WEBPLAYER
                new List<string>(){"UnityEngine.Application", "ExternalEval"},
    #endif
                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
                new List<string>(){ "UnityEngine.Input", "IsJoystickPreconfigured","System.String"},
                new List<string>(){ "UnityEngine.UI.Graphic", "OnRebuildRequested"},
                new List<string>(){ "UnityEngine.UI.Text", "OnRebuildRequested"},
            };
}
