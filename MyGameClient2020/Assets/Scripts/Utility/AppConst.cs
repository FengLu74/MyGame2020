using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//namespace LuaFramework {
public class AppConst {
    public const bool DebugMode = false;                       //调试模式-用于内部测试

    public const bool UpdateMode = false;                       //更新模式-默认关闭 
    public bool PackageMode = true;                                   //打包模式
    public const int GameFrameRate = 60;                        //刷新帧率

    public const string AppName = "YuLongMGame";        //
    public const string LuaTempDir = "Lua/";                    //临时目录
    public const string AppPrefix = AppName + "_";          //应用程序前缀
    public const string ExtName = ".unity3d";                   //素材扩展名
    public const string AssetUpdateVerFileName = "updateVer.txt";       //升级包配置文件名
    public const string AssetDir = "StreamingAssets";           //素材目录 
    public const string AssetVerFileName = "ver.txt";       //资源版本号文件名
    public const string AssetAdapterVerFileName = "ResServerConfig.txt";   //资源服务器适配器文件名
    public const string LuaAbAssetName = "Lua";             //Lua的AssetBundle包名称
    public const string ShaderAbAssetName = "Shader";  //Shader的AssetBundle包名称
    public const string LuaExtName = ".bytes";                  //Lua文件的后缀
    public const string LuaAbExtName = ".lua.bytes";         //Lua在AB中的后缀
    public const string ShaderExtName = ".shader";          //shader文件的后缀
    public const string GameAssetsFolderName = "GameAssets";//游戏资源目录
    public const string LuaScriptsFolder = "LuaScripts";             //Lua脚本目录
    public const string ResServerAdapterPath = "Adapter";//资源服务器适配器目录
    public const string ResServerCheckServerPath = "CheckResServer";//提审服资源服务器目录名
    public const string ResServerOfficialServerPath = "OfficialResServer";//正式资源服务器目录名
    public const string ResServerTestServerPath = "TestResServer";//测试资源服务器目录名
    public const string NoticeTxt = "/gonggao.txt";             //公告文件
    

    /// <summary>
    /// Http 中 url 拼接
    /// </summary>
    public const string LoginUrl = "login";// 登录时的url
    public const string LoginBySDK = "loginBySDK";// 登录sdk 时的url
    public const string DeviceOpen = "deviceOpen";// 发送设备信息时需要的url 
    public const string DownLoadURL = "getDownURL";// 
    public const string gameInfo = "gameInfo";// 请求服务器状态接口
    //public const string WebResUrl = "http://localhost:6688/";      //测试更新地址
    //public const string WebResUrl = "http://127.0.0.1/";//测试资源服务器地址
#if USE_PUBLISH
    public const string WebResUrl = "https://cbhx.res.rastargame.com/Mgame/Resource/";//正式资源服务器地址 
    public const string NoticeUrl = "https://cbhx.res.rastargame.com/Mgame/Resource/YuLongMGame/gonggao";
#elif USE_YULONG
    public const string WebResUrl = "https://resource.yldragon.com/hotpatch/Mgame/Resource2/";//测试资源服务器地址
    public const string NoticeUrl = "https://resource.yldragon.com/hotpatch/Mgame/Resource2/YuLongMGame/gonggao";
#elif USE_XINGHUI
    public const string WebResUrl = "https://resource.yldragon.com/hotpatch/Mgame/Resource/";//正式资源服务器地址 
    public const string NoticeUrl = "https://resource.yldragon.com/hotpatch/Mgame/Resource/YuLongMGame/gonggao";
#else
    public const string WebResUrl = "https://resource.yldragon.com/hotpatch/Mgame/Resource/";//测试资源服务器地址 
    public const string NoticeUrl = "https://resource.yldragon.com/hotpatch/Mgame/Resource/YuLongMGame/gonggao";
#endif

    //public const string testUrl = "http://192.168.1.9/YuLongMGame/gonggao";//测试服务器维护公告地址ljs 本机地址(电脑本机的web)

#if UNITY_IOS
    public static string WebResRoot = "ios/";
#elif UNITY_ANDROID
    public static string WebResRoot = "android/";
#elif UNITY_STANDALONE_WIN
    public static string WebResRoot = "windows/";
#endif

    //这里定义代码用需要调用的资源路径信息
    public static string resPath_Asset = "Assets/GameAssets/";//资源根目录

    public static string resPath_LuaPb = "LuaScripts/";//luaPB文件目录

    //UI相关
    public static string resPath_UI = "Prefabs/GUI/UI/";//UI窗口预设目录
    public static string resPath_UI_Element = "Prefabs/GUI/Element/";//UI元素目錄
    public static string resPath_UI_Common = "Prefabs/GUI/UI/Common/";//通用目录

    //角色相关
    public static string resPath_PlayerStory = "Prefabs/Avatar/Story/";//剧情角色预设目录
    public static string resPath_PlayerYueji = "Prefabs/Avatar/AvatarUI/";//乐姬角色预设目录
    public static string resPath_PlayerBattle = "Prefabs/Avatar/Battle/";//战斗角色预设目录
    public static string resPath_PlayerLive2D = "Prefabs/Avatar/Live2D/";//Live2D角色预设目录

    //后院相关
    public static string resPath_BackyardFurnture = "Prefabs/Scenes/Backyard/Furniture/";//后院家具
    //场景相关
    public static string resPath_SceneStoryElement = "Prefabs/Scenes/Story/";//场景剧情预设
    public static string resPath_SceneBackyardElement = "Prefabs/Scenes/Backyard/";//场景后院元素

    //特效相关
    public static string resPath_Skill = "Prefabs/Effect/Skill/";//技能预设目录
    public static string resPath_Buff = "Prefabs/Effect/Buffer/";//技能Buffer预设目录
    public static string resPath_EffectBattleElement = "Prefabs/Effect/Element";//战斗跳字

    //图集
    //public static string resPath_Atlas = "Atlas/";//图集目录
    //public static string resPath_AtlasCommon = "Atlas/Common/";//通用图集目录
    public static string resPath_AtlasUI= "Atlas/UI/";//UI图集目录

    //
    public static string resPath_Pb = "LuaScripts/";

    //剧情场景预设
    public static string resPath_StorySencePrefab = "Prefabs/Scenes/Story/";



    //声音
    //public static string resPath_SoundYueji = "Sounds/Avarar/AvatarUI/";//乐姬音效目录
    //public static string resPath_SoundStory = "Sounds/Avarar/Stroy/";//乐姬故事音效目录
    public static string resPath_SoundStory = "Sounds/Actor/Story/";//乐姬故事音效目录
    public static string resPath_SoundOldStory = "Sounds/BG/";//兼容之前的老的剧情模块功能，后面可以删除掉
    public static string resPath_SoundEffect = "Sounds/Effect/";//战斗特效音效目录
    public static string resPath_SoundBG= "Sounds/BG/";//场景音效目录
    public static string resPath_SoundUI = "Sounds/UI/";//UI音效目录
    public static string resPath_SoundCard = "Sounds/BattleCard/";//手牌音效
    public static string resPath_SoundActor = "Sounds/Actor/";//音效
    public static string resPath_SoundBattle = "Sounds/Battle/";//音效
    public static string resPath_Sound = "Sounds/";

    //视频
    public static string resPath_Video = "Video/";

    //字体
    public static string resPath_Font = "Font/";//字体目录

    //镜头动画
    public static string resPath_AnimationCamera = "Animation/CameraAnims/";

    //UI动画
    public static string resPath_AnimationUI = "Animation/UIAnim/";

    //字体
    public static string resPath_Shader = "Shader";//shader目录

    public static int SocketPort = 0;                           //Socket服务器端口
    public static string SocketAddress = string.Empty;          //Socket服务器地址

    public static string FrameworkRoot {
        get {
            return Application.dataPath + "/" + AppName;
            
        }
    }

    public static string URL(int id)// 
    {
#if USE_PUBLISH
        return "http://101.200.41.150:8088/account/";
#elif USE_XINGHUI
       return "http://account2.tokyo.yldragon.com/account/";
#elif USE_YULONG
        return "http://47.99.93.134:8088/account/";
#else
        if (id == 0)       //内网测试服
        {
           return  "http://192.168.1.188:8088/account/";
        }
        else if (id == 1)  // 渠道测试服
        {
            return "http://account2.tokyo.yldragon.com/account/";
        }
        else if (id == 2)    // 吕岳服
        {
            return "http://192.168.1.18:8088/account/";
        }
        else if (id == 3)    //马宝强服
        {
            return "http://192.168.1.37:8088/account/";
        }
        else if (id == 4)    //方仁和服
        {
            return  "http://192.168.1.21:8088/account/";
        }
        else if (id == 5)    //外网内部渠道服
        {
            return "http://47.99.93.134:8088/account/";
        }
        else
        {
            return "http://47.99.93.134:8088/account/";
        }
#endif
    }
   


    //当前版本号：每次正式对外升级打包，需要此值+1
    //public static int gameVerUpdateNumber = 1001;
}
