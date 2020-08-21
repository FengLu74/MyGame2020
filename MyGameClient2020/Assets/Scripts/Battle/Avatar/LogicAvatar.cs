using MGame.GameBattle.Logic;
using MGame.GameBattle.Logic.MMath;

namespace MGame.GameBattle.Logic
{
    public abstract class LogicAvatar : LogicWorldElement
    {
        private enum AvatarBattleBehaviourState
        {
            none,
            entrance,//入场移动
            playPose,//播放pose动画
            waitBattle,//播放完pose等待状态
            battle,//战斗
        }
        private enum AvatarState
        {
            sleep,
            alive,
            dead,
            destroy,
        }
        private AvatarState state;
        private AvatarBattleBehaviourState battleBehaviourState = AvatarBattleBehaviourState.none;
        private Fix64 deadKeepTime;
        private bool destroyNextFrame;
        private bool suspended;
        private bool hideAvatar;
        /// <summary>
        /// 护盾相关
        /// </summary>
        //public ShieldContainer shieldContainer;
        //public AreaShieldContainer areaShieldContainer = new AreaShieldContainer();
        //private Action<bool> OnAvatarStateChangeCall;
        private FixVector3 entranceInitPos;
        private int entranceKeepTimeFrame;
        private int entranceTimeFrameCounter;
        private int poseTimeFrameCounter;
        private Fix64 entranceLerp;
        private FixVector3 entranceTargetPos;


        public LogicAvatarData avatarData;
        //////////////////////////////////////////////////LogicAvatar 属性////////////////////////////////////////////////////////////
        public bool IsDead
        {
            get { return state == AvatarState.dead; }
        }
        public bool IsAlive
        {
            get { return state == AvatarState.alive; }
        }
        public bool IsDestroyed
        {
            get { return state == AvatarState.destroy; }
        }
        public bool IsSuspended
        {
            get { return suspended; }
        }
        public bool IsAvatarHide
        {
            get { return hideAvatar; }
        }
        public LogicAvatar(WorldElementType eType) : base(eType)
        {
        }
        public virtual bool NeedCreteComponent(AvatarComponentType type)
        {
            switch (type)
            {
                case AvatarComponentType.ComponentAI:
                case AvatarComponentType.ComponentSkill:
                //case AvatarComponentType.ComponentSkillBuffEffectReceiver:
                case AvatarComponentType.ComponentState:
                case AvatarComponentType.ComponentAttribute:
                case AvatarComponentType.ComponentMovement:
                case AvatarComponentType.ComponentAnim:
                case AvatarComponentType.ComponentEvent:
                    return true;
            }
            return false;
        }
        protected override void Initialize(ElementData data)
        {
            base.Initialize(data);
            avatarData = (LogicAvatarData)data;
            state = AvatarState.sleep;
            pComponentMgr.Initialize(this);
            shieldContainer = new ShieldContainer(OnShieldChanged);
            if (elementType != WorldElementType.ThirdUser)
            {
                deadKeepTime = LogicCommon.GetValue(avatarData.avatarTableLine, "deadTime", Fix64.Zero) / 100;
            }
            destroyNextFrame = false;
        }
    }
}
