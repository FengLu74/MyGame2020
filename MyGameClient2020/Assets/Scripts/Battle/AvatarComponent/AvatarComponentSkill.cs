using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// avatar 技能组件
/// </summary>
namespace Battle.Logic
{
    public class AvatarComponentSkill : AvatarComponentBase
    {
        //private Skill curUsingSkill;
        public void CheckBreakCurrentUsingSkill(int breakType)//检测是否被动打断而提前结束技能
        {
            //if (curUsingSkill != null)
            //{
            //    curUsingSkill.CheckBreakSkill(breakType);
            //}
        }
        public override void Clear()
        {
            base.Clear();
        }
    }
}
