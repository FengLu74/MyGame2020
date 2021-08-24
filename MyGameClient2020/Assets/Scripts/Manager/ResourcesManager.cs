using Common.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace Manager
{
    public class ResourcesManager : TSingletonMono<ResourcesManager>
    {
        //區分C#用到的表格，部分LUA表沒有用到
        string[] cfgName_num = new string[55];

        string[] cfgName_str = new string[14];

        public Dictionary<string, BinaryTable> LoadBinaryTables()
        {
            #region 配置表名
            //数字为主键
            cfgName_num[0] = "cfg_storynpc";
            cfgName_num[1] = "cfg_behaviourTree_hero";//
            cfgName_num[2] = "cfg_behaviourTree_monster";
            cfgName_num[3] = "cfg_behaviourTree_summon";
            cfgName_num[4] = "cfg_skillBasic";//
            cfgName_num[5] = "cfg_skillEffect";//
            cfgName_num[6] = "cfg_avatar_attribute";//
            cfgName_num[7] = "cfg_buffBasic";//
            cfgName_num[8] = "cfg_buffEffect";//
            cfgName_num[9] = "cfg_buffGroup";//
            cfgName_num[10] = "cfg_battleCard";//
            cfgName_num[11] = "cfg_summon";
            cfgName_num[12] = "cfg_heroEnter";
            cfgName_num[13] = "cfg_avatar_monster";//
            cfgName_num[14] = "cfg_storybase";
            cfgName_num[15] = "cfg_holyData";//
            cfgName_num[16] = "cfg_storyNode";//
            cfgName_num[17] = "cfg_monsterGroup";//
            cfgName_num[18] = "cfg_mainInterface";
            cfgName_num[19] = "cfg_scene";//
            cfgName_num[20] = "cfg_heroBase";//
            cfgName_num[21] = "cfg_heroskin";//
            cfgName_num[22] = "cfg_heroAttribute";//
            cfgName_num[23] = "cfg_achievementTask";
            cfgName_num[24] = "cfg_battleicon";
            cfgName_num[25] = "cfg_battletest";
            cfgName_num[26] = "cfg_challengeChapter";
            cfgName_num[27] = "cfg_challengeMap";
            cfgName_num[28] = "cfg_challengeMonster";
            cfgName_num[29] = "cfg_chestBase";
            cfgName_num[30] = "cfg_chestWeightsBase";
            cfgName_num[31] = "cfg_dailyTask";
            cfgName_num[32] = "cfg_equipBase";
            cfgName_num[33] = "cfg_goods";
            cfgName_num[34] = "cfg_heroLvUp";
            cfgName_num[35] = "cfg_item";
            cfgName_num[36] = "cfg_Loading";
            cfgName_num[37] = "cfg_noviceTask";
            cfgName_num[38] = "cfg_noviceTaskAward";
            cfgName_num[39] = "cfg_payBase";
            cfgName_num[40] = "cfg_starUpAddition";
            cfgName_num[41] = "cfg_starUpConsume";
            cfgName_num[42] = "cfg_storyChapter";
            cfgName_num[43] = "cfg_avatar_monsterModel";//
            cfgName_num[44] = "cfg_scenesList";
            cfgName_num[45] = "cfg_mainInterfacenew";
            //cfgName_num[46] = "cfg_eventPlot";
            cfgName_num[47] = "cfg_awakenBase";
            //cfgName_num[48] = "cfg_SpActivityPlot";
            cfgName_num[49] = "cfg_monsterAi";
            cfgName_num[50] = "cfg_probeMapRes";
            cfgName_num[51] = "cfg_probeEvent";
            cfgName_num[52] = "cfg_probeMap";
            cfgName_num[53] = "cfg_Activity_Plot";
            cfgName_num[54] = "cfg_skillTrigger";
            //字符串为主键
            cfgName_str[0] = "cfg_particlesResource";
            cfgName_str[1] = "cfg_avaterResource_hero";//
            cfgName_str[2] = "cfg_artEffect";
            cfgName_str[3] = "cfg_basic";//
            cfgName_str[4] = "cfg_campRelationship";//
            cfgName_str[5] = "cfg_language";//
            cfgName_str[6] = "cfg_storydialog";
            cfgName_str[7] = "cfg_storylanguage";
            cfgName_str[8] = "cfg_bgm";
            cfgName_str[9] = "cfg_sound";//
            cfgName_str[10] = "cfg_bk";
            cfgName_str[11] = "cfg_uieffect";
            cfgName_str[12] = "cfg_soundBasic";
            cfgName_str[13] = "cfg_affinityLines";

            #endregion
            Dictionary<string, BinaryTable> binarytableDict = new Dictionary<string, BinaryTable>();

            for (int i = 0; i < cfgName_num.Length; i++)
            {
                //Common.Log("-------cfgName----------" + cfgName_num[i]);
                if (string.IsNullOrEmpty(cfgName_num[i]))
                {
                    continue;
                }
                LuaTable table = LuaMonoManager.Instance.luaenv.Global.Get<LuaTable>(cfgName_num[i]);
                Dictionary<object, object> luaDict = table.Cast<Dictionary<object, object>>();
                var iter = luaDict.GetEnumerator();
                BinaryTable tb = new BinaryTable(cfgName_num[i], true);
                while (iter.MoveNext())
                {
                    LuaTable info = iter.Current.Value as LuaTable;
                    AddLine(info, iter.Current.Key.ToString(), tb, true);
                }
                binarytableDict.Add(cfgName_num[i], tb);
            }

            for (int i = 0; i < cfgName_str.Length; i++)
            {
                LuaTable table = LuaMonoManager.Instance.luaenv.Global.Get<LuaTable>(cfgName_str[i]);
                Dictionary<object, object> luaDict = table.Cast<Dictionary<object, object>>();
                var iter = luaDict.GetEnumerator();

                BinaryTable tb = new BinaryTable(cfgName_str[i], false);
                while (iter.MoveNext())
                {
                    LuaTable info = iter.Current.Value as LuaTable;
                    AddLine(info, iter.Current.Key.ToString(), tb, false);
                }
                binarytableDict.Add(cfgName_str[i], tb);

            }
            return binarytableDict;
        }
        private BinaryTable AddLine(LuaTable data, string lineKey, BinaryTable tb, bool isnumKey)
        {
            Dictionary<object, object> lineDic = data.Cast<Dictionary<object, object>>();
            var iter = lineDic.GetEnumerator();
            if (isnumKey)
            {
                while (iter.MoveNext())
                {
                    tb.AddData(int.Parse(lineKey), iter.Current.Key.ToString(), iter.Current.Value.ToString());
                }
            }
            else
            {
                while (iter.MoveNext())
                {
                    tb.AddData(lineKey, iter.Current.Key.ToString(), iter.Current.Value.ToString());
                }
            }


            return tb;
        }
    }
}
