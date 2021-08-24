using System;
using System.Collections.Generic;


namespace Common.General
{
    public delegate void Callback();
    public delegate void Callback<T>(T arg1);
    public delegate void Callback<T, U>(T arg1, U arg2);


    /// <summary>
    /// 表现相关事件
    /// </summary>
    public class MessageEvent
    {

        public struct EventType
        {
            public const string Type_Avatar_Hide = "Type_Avatar_Hide";
            public const string Type_Skill_Suspend = "Type_Skill_Suspend";
            public const string Type_World_Element_Create = "Type_World_Element_Create";
            public const string Type_World_Element_Destroy = "Type_World_Element_Destroy";
            public const string Type_World_Hero_Destroy = "Type_World_Hero_Destroy";
            public const string Type_Avatar_On_Hurt = "Type_Avatar_On_Hurt";
            public const string Type_Avatar_On_Treat = "Type_Avatar_On_Treat";
            public const string Type_Avatar_On_HP_Changed = "Type_Avatar_On_HP_Changed";
            public const string Type_Avatar_Use_Active_Skill = "Type_Avatar_Use_Active_Skill";
            public const string Type_Avatar_Skill_Event = "Type_Avatar_Skill_Event";
            /// <summary>
            /// 卡牌行动点数+
            /// </summary>
            public const string Type_Card_Skill_Event_ADD = "Type_Card_Skill_Event_ADD";
            /// <summary>
            /// 卡牌行动点数-
            /// </summary>
            public const string Type_Card_Skill_Event_MINUS = "Type_Card_Skill_Event_MINUS";
            /// <summary>
            /// 角色响应选中卡牌时特效
            /// </summary>
            public const string Type_Avatar_Card_Event_SELECT = "Type_Avatar_Card_Event_SELECT";
            /// <summary>
            /// 牌组，弃牌发生变化
            /// </summary>
            public const string Type_Card_Card_Event_Change = "Type_Card_Card_Event_Change";
            /// <summary>
            /// 释放技能时，角色说话
            /// </summary>
            public const string Type_Card_Skill_Event_Talk = "Type_Card_Skill_Event_Talk";
            /// <summary>
            /// 角色，怪物生命值发生变化
            /// </summary>
            public const string Type_Avatar_Hp_Event_Change = "Type_Avatar_Hp_Event_Change";
            /// <summary>
            /// 角色位置发生变化，主摄像机发生偏移，缩放
            /// </summary>
            public const string Type_Avatar_PosAndCamera_Event_Change = "Type_Avatar_PosAndCamera_Event_Change";
            /// <summary>
            /// 角色，怪物生命值发生变化Lua
            /// </summary>
            public const string Type_Avatar_Hp_Event_Change_Lua = "Type_Avatar_Hp_Event_Change_Lua";
            /// <summary>
            /// buff作用对象
            /// </summary>
            public const string Type_Avatar_Buff_Event_Change_State = "Type_Avatar_Buff_Event_Change_State";
            /// <summary>
            /// 伤害暴击
            /// </summary>
            public const string Type_Avatar_On_Crit = "Type_Avatar_On_Crit";
            /// <summary>
            /// 洗牌倒计时
            /// </summary>
            public const string Type_Card_On_Shuff_Time = "Type_Card_On_Shuff_Time";

            /// <summary>
            /// 费用点数刷新
            /// </summary>
            public const string Type_Card_Skill_Point_Update = "Type_Card_Skill_Point_Update";
            /// <summary>
            /// 洗牌
            /// </summary>
            public const string Type_Card_Shuff_Card_Update = "Type_Card_Shuff_Card_Update";
            /// <summary>
            /// 点数刷新特效
            /// </summary>
            public const string Type_Card_Action_Card_Effect_Update = "Type_Card_Action_Card_Effect_Update";
            /// <summary>
            /// 费用点数刷新0
            /// </summary>
            public const string Type_Card_Skill_Point_Update0 = "Type_Card_Skill_Point_Update0";
            /// <summary>
            /// 聚爆
            /// </summary>
            public const string Type_Card_Skill_Point_Update1 = "Type_Card_Skill_Point_Update1";
            /// <summary>
            /// 顶针
            /// </summary>
            public const string Type_Camera_State_Change = "Type_Camera_State_Change";
            /// <summary>
            /// 替换
            /// </summary>
            public const string Type_Card_Chage = "Type_Card_Chage";
            public const string Type_Bodys_Chage = "Type_Bodys_Chage";
            public const string Type_Init_Herad_Chage = "Type_Init_Herad_Chage";
            public const string Type_Init_Herad_ChageSSDD = "Type_Init_Herad_ChageSSDD";
            public const string Type_Net_NetEvent_Reboot = "Type_Net_NetEvent_Reboot";
        }


        public static Dictionary<string, Delegate> eventDict = new Dictionary<string, Delegate>();
        public static void AddListener(string eventName, Callback call)
        {
            if (!eventDict.ContainsKey(eventName))
            {
                eventDict[eventName] = call;
            }
            else
            {
                eventDict[eventName] = (Callback)eventDict[eventName] + call;
            }
        }

        public static void AddListener<T>(string eventName, Callback<T> call)
        {
            if (!eventDict.ContainsKey(eventName))
            {
                eventDict[eventName] = call;
            }
            else
            {
                eventDict[eventName] = (Callback<T>)eventDict[eventName] + call;
            }
        }

        public static void AddListener<T, U>(string eventName, Callback<T, U> call)
        {
            if (!eventDict.ContainsKey(eventName))
            {
                eventDict[eventName] = call;
            }
            else
            {
                eventDict[eventName] = (Callback<T, U>)eventDict[eventName] + call;
            }
        }


        public static void RemoveListener(string eventName, Callback call)
        {
            if (eventDict.ContainsKey(eventName))
            {
                eventDict[eventName] = (Callback)eventDict[eventName] - call;
            }
        }

        public static void RemoveListener<T>(string eventName, Callback<T> call)
        {
            if (eventDict.ContainsKey(eventName))
            {
                eventDict[eventName] = (Callback<T>)eventDict[eventName] - call;
            }
        }

        public static void RemoveListener<T, U>(string eventName, Callback<T, U> call)
        {
            if (eventDict.ContainsKey(eventName))
            {
                eventDict[eventName] = (Callback<T, U>)eventDict[eventName] - call;
            }
        }

        public static void Broadcast(string eventName)
        {
            Delegate d;
            if (eventDict.TryGetValue(eventName, out d))
            {
                Callback cb = d as Callback;
                if (cb != null)
                {
                    cb();
                }
            }
        }

        public static void Broadcast<T>(string eventName, T arg1)
        {
            Delegate d;
            if (eventDict.TryGetValue(eventName, out d))
            {
                Callback<T> cb = d as Callback<T>;
                if (cb != null)
                {
                    cb(arg1);
                }
            }
        }

        public static void Broadcast<T, U>(string eventName, T arg1, U arg2)
        {
            Delegate d;
            if (eventDict.TryGetValue(eventName, out d))
            {
                Callback<T, U> cb = d as Callback<T, U>;
                if (cb != null)
                {
                    cb(arg1, arg2);
                }
            }
        }
    }

}