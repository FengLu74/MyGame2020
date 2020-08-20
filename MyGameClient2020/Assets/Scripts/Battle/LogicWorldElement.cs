using MGame.GameBattle.Logic.MMath;
using MGame.GameBattle.Manager;

namespace MGame.GameBattle.Logic
{
    /// <summary>
    /// 战斗单位
    /// </summary>
    public abstract class LogicWorldElement
    {
        private WorldElementType eType;
        private bool isActive = false;
        private int instanceId;
        //////////////////////////////////// 坐标///////////////////////////////////////////////////
        private FixVector3 localPos = FixVector3.Zero;
        private FixVector3 localScale = FixVector3.One;
        private FixVector3 localForward = FixVector3.Forward;
        private bool forwardRight = true;
        public ElementData data;
        
        public WorldElementType elementType
        {
            get { return eType; }
        }
        public bool IsActive
        {
            get { return IsActive; }
        }
        public int InstanceId
        {
            get { return instanceId; }
        }
        public bool IsForwardRight
        {
            set { forwardRight = value; }
            get { return forwardRight; }
        }
        public FixVector3 LocalPos
        {
            set
            {
                localPos = value;
            }
            get { return localPos; }
        }
        public FixVector3 LocalScale
        {
            set { localScale = value; }
            get { return localScale; }
        }
        public FixVector3 LocalForward
        {
            set { localForward = value; }
            get { return localForward; }
        }
        public LogicWorldElement(WorldElementType eType)
        {
            this.eType = eType;
        }
        public void Create()
        {
            isActive = true;
            instanceId = LogicManager.Instance.battleManager.newWorldElementInstanceId;

        }
    }
}


