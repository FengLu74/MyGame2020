using MGame.GameBattle.Logic.MMath;

namespace MGame.GameBattle.Logic
{
    /// <summary>
    /// 战斗单位
    /// </summary>
    public abstract class LogicWorldElement
    {
        private LogicWorldElement eType;
        private bool isActive = false;
        private int instanceId;
        //////////////////////////////////// 坐标///////////////////////////////////////////////////
        private FixVector3 localPos = FixVector3.Zero;
        private FixVector3 localScale = FixVector3.One;
        private FixVector3 localForward = FixVector3.Forward;
        private bool forwardRight = true;

    }
}


