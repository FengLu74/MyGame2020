using MGame.GameBattle.Logic.MMath;
using MGame.GameBattle.Manager;
using MGame.Logic;
using System.Collections.Generic;


namespace MGame.GameBattle.Logic
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
        public Dictionary<string, DataNodeString> avatarTabline;
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
