using Battle.Logic.MMath;
using Common.General;
using System.Collections.Generic;

namespace Battle.Manager
{
    public class LogicBattleManager
    {
        public enum BattleResult
        {
            Win = 0,
            Defeat = 1,
            Draw = 2,
        }
        private enum BattleProcess
        {
            None,
            Start = 6,
            End = 7,
        }
        public class HolyData // 圣物数据 ,同种类型的圣物
        {
            public HolyData(int id)
            {
                this.Id = id;
            }
            public int Id;
            public int SkillId;
            public int Count;
            public float CountDownTime;
            public holyTriggerType triggerType = holyTriggerType.None;
            public enum holyTriggerType
            {
                None,
                Start = 6,      // 战斗开始时触发
                End = 7,        //战斗结束时触发
            }
        }
        private List<HolyData> mHolyDataList = new List<HolyData>(); // avatar身上的圣物数据
        /// <summary>
        /// 是否在战斗中
        /// </summary>
        private bool isBatting;
        /// <summary>
        /// 战斗阶段
        /// </summary>
        private BattleProcess curBattleProcess = BattleProcess.None;
        /// <summary>
        /// 全局变量表
        /// </summary>
        private BinaryTable baseTable;
        private BinaryTable holyTable;
        private BinaryTable cfg_storyNode;

        private int battlePoseKeepFrame = 0;
        private bool hasPlayedBattlePose = false;
        private int _worldElementInstanceId;
        private long mLogicUpdateFrameDistance = 0;
        /// <summary>
        /// avatar 入场摆pos 帧数
        /// </summary>
        private int showPosFrame;
        public int newWorldElementInstanceId
        {
            get
            {
                return ++_worldElementInstanceId;
            }
        }
        public long LogicUpdateFrameDistance
        {
            get
            {
                return mLogicUpdateFrameDistance;
            }
            set
            {
                mLogicUpdateFrameDistance = value;
            }
        }

        public void Initialize()
        {
            cfg_storyNode = BinaryTableLoader.Instance.GetBinaryTable("cfg_storyNode");
            baseTable = BinaryTableLoader.Instance.GetBinaryTable("cfg_basic");
            holyTable = BinaryTableLoader.Instance.GetBinaryTable("cfg_holyData");
            showPosFrame = LogicManager.Instance.TimeToFrameCeil(new Fix64(int.Parse(baseTable.GetDataValue("enterBattleTime", "parm"))) / 100);
        }
        public void Update()
        {
            if (battlePoseKeepFrame > 0)
            {
                battlePoseKeepFrame--;
                //if (battlePoseKeepFrame == 0)
                //{
                //    LogicManager.Instance.avatarManager.SetAllAvatarToBattleState();
                //    LogicManager.Instance.cardOptionManager.IsStateBattle = true;
                //    changeBattleProcessState(BattleProcess.Start);
                //    mLogicUpdateFrameDistance = LogicManager.Instance.logicFrameCounter;
                //}
                //if (battlePoseKeepFrame <= showFrame && !hasStartPlayedBattlePose)
                //{
                //    hasStartPlayedBattlePose = true;
                //    LuaMonoInst.Instance.CallLuaFunBool("UIBattleManager.startShowBattle", true);
                //}
            }
        }
        
        private void changeBattleProcessState(BattleProcess state)
        {
            curBattleProcess = state;
            checkHolyTrigger();
        }
        /// <summary>
        /// 检查符合战斗阶段内的圣物
        /// </summary>
        private void checkHolyTrigger()
        {
            for (int i = mHolyDataList.Count - 1; i >= 0; i--)
            {
                if ((int)mHolyDataList[i].triggerType == (int)curBattleProcess)
                {
                    //LogicManager.Instance.cardOptionManager.PlaySkill(mHolyDataList[i].SkillId, 4);
                }
            }
        }
    }
}
