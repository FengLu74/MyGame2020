using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame.GameBattle.Manager
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

    }
}
