using MGame.GameBattle.Logic.MMath;
using MGame.General;
using MGame.Logic;
using System.Collections.Generic;


namespace MGame.GameBattle.Logic
{
    public class LogicCommon
    {
        public static string GetTableKeyValue(string tableName, string linekey, string key)
        {
            BinaryTable effectResCfg = BinaryTableLoader.Instance.GetBinaryTable(tableName);
            return effectResCfg.GetDataValue(linekey, key);
        }

        public static string GetTableKeyValue(string tableName, int linekey, string key)
        {
            BinaryTable effectResCfg = BinaryTableLoader.Instance.GetBinaryTable(tableName);
            return effectResCfg.GetDataValue(linekey, key);
        }

        public static int GetValue(Dictionary<string, string> dict, string key, int defaultValue)
        {
            int v = defaultValue;
            string s;
            if (dict.TryGetValue(key, out s))
            {
                int.TryParse(s, out v);
            }
            return v;
        }

        public static bool GetValue(Dictionary<string, string> dict, string key, bool defaultValue)
        {
            bool v = defaultValue;
            string s;
            if (dict.TryGetValue(key, out s))
            {
                bool.TryParse(s, out v);
            }
            return v;
        }

        public static Fix64 GetValue(Dictionary<string, string> dict, string key, Fix64 defaultValue)
        {
            Fix64 v = defaultValue;
            string s;
            if (dict.TryGetValue(key, out s))
            {
                int n;
                if (int.TryParse(s, out n))
                {
                    v = (Fix64)n;
                }
            }
            return v;
        }

        public static string GetValue(Dictionary<string, string> dict, string key, string defaultValue)
        {
            string v;
            if (dict.TryGetValue(key, out v))
            {
                return v;
            }
            return defaultValue;
        }


        public static int GetValue(Dictionary<string, DataNodeString> dict, string key, int defaultValue)
        {
            DataNodeString n = null;
            dict.TryGetValue(key, out n);
            if (n != null)
            {
                return n.AsInt;
            }
            return defaultValue;
        }

        public static Fix64 GetValue(Dictionary<string, DataNodeString> dict, string key, Fix64 defaultValue)
        {
            DataNodeString n = null;
            dict.TryGetValue(key, out n);
            if (n != null)
            {
                return n.AsFix64;
            }
            return defaultValue;
        }


        public static float GetValue(Dictionary<string, string> dict, string key, float defaultValue)
        {
            float v = defaultValue;
            string s;
            if (dict.TryGetValue(key, out s))
            {
                float.TryParse(s, out v);
            }
            return v;
        }


        public static bool CheckMoveToPos(List<LogicAvatar> avatarList, LogicAvatar avatar, FixVector3 from, ref FixVector3 to)//未测试
        {
            return false; // 暂时屏蔽
            //FixVector3 forwardToTarget;
            //FixVector3 forward;

            //for (int i = 0; i < avatarList.Count; i++)
            //{
            //    if (avatar == avatarList[i])
            //    {
            //        continue;
            //    }
            //    Fix64 targetToPosDis = FixVector3.Distance(to, avatarList[i].LocalPos);
            //    Fix64 targetToAvatarMinDis = avatarList[i].GetComponentMgr().Movement.radius + avatar.GetComponentMgr().Movement.radius;
            //    if (targetToPosDis < targetToAvatarMinDis)
            //    {
            //        forwardToTarget = avatarList[i].LocalPos - avatar.LocalPos;
            //        //UnityEngine.Debug.DrawLine(from.ToVector3(), avatarList[i].avatarPos.ToVector3(), UnityEngine.Color.red);
            //        forward = (to - from);

            //        forward = forward - FixVector3.Project(forward, forwardToTarget);
            //        to = from + forward;
            //        //UnityEngine.Debug.DrawLine(from.ToVector3(), to.ToVector3(), UnityEngine.Color.blue);
            //        return false;
            //    }
            //}
            //return true;
        }


        public static FixVector2 ForwardRotate(Fix64 x, Fix64 y, Fix64 angle)
        {
            angle = angle / 180 * Fix64.PI;
            Fix64 x1 = Fix64.Cos(angle) * x - Fix64.Sin(angle) * y;
            Fix64 y1 = Fix64.Sin(angle) * x + Fix64.Cos(angle) * y;
            return new FixVector2(x1, y1);
        }

        public FixVector3 UpdateBSE(Fix64 t, FixVector3 p0, FixVector3 p1, FixVector3 p2)
        {
            FixVector3 bPos = (1 - t) * (1 - t) * p0 + 2 * t * (1 - t) * p1 + t * t * p2;
            return bPos;
        }
    }
}
