
using FrameWork.ReferencePool;
using Battle.Logic.MMath;

namespace Battle.Logic
{
    /// <summary>
    /// 战斗单位
    /// </summary>
    public abstract class LogicWorldElement:IReference
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

        public virtual void Clear()
        {
            eType = WorldElementType.None;
            isActive = false;
            instanceId = 0;
            localPos = FixVector3.Zero;
            localScale = FixVector3.One;
            localForward = FixVector3.Forward;
            forwardRight = true;
            data = null;
        }


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
            //instanceId = LogicManager.Instance.battleManager.newWorldElementInstanceId;

        }
        protected virtual void Initialize(ElementData data)
        {
            this.data = data;
            this.localPos = data.localPos;
            this.localScale = data.localScale;
            this.localForward = data.localForward;
            this.forwardRight = data.forwardRight;
        }
        protected virtual void OnCreate()
        {
        }
        protected virtual void OnDestroy()
        {
        }
        public virtual void Update()
        {
        }
        public static LogicWorldElement LogicWorldElementFactory(WorldElementType type, ElementData data)
        {
            LogicWorldElement pElement = null;
            switch (type)
            {
                case WorldElementType.AvatarHero:
                    //pElement = new LogicAvatarHero();
                    break;

                case WorldElementType.AvatarMonster:
                    //pElement = new LogicAvatarMonster();
                    break;
            }
            if (pElement != null)
            {
                pElement.Initialize(data);
                pElement.Create();
            }
            return pElement;
        }


    }
}


