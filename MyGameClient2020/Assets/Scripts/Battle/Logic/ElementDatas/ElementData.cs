using Battle.Logic.MMath;
using Battle.Manager;
using Game.Logic;
using System.Collections.Generic;


namespace Battle.Logic
{
    /// <summary>
    /// 元素基础数据
    /// </summary>
    public class ElementData
    {
        public FixVector3 localPos = FixVector3.Zero;
        public FixVector3 localScale = FixVector3.One;
        public FixVector3 localForward = FixVector3.Forward;
        public bool forwardRight = true;
    }
    public class LogicAvatarData:ElementData
    {
        public Dictionary<string, DataNodeString> avatarTableLine;
        public BattleCampInfo camp;
        public int HeroID;
        public int MonsterID;
    }
    public class LogicHeroData : LogicAvatarData
    {

    }
    public class LogicMonsterData : LogicAvatarData
    {

    }
}
