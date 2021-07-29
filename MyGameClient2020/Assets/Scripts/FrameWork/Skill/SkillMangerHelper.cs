using Battle.Logic;
using FrameWork.Camp;
using System.Collections.Generic;

namespace FrameWork.Skill
{
    /// <summary>
    /// 技能管理help
    /// </summary>
    public class SkillMangerHelper
    {
        private int skillInstanceId;
        public Skill GetSkill(LogicAvatar avatar,BattleCampInfo skillCamp,int skillId)
        {
            Skill skill = ReferencePool.ReferencePool.Acquire<Skill>();
            Dictionary<string, string> data = null;
            skill.Initialize(avatar, skillCamp, data, ++skillInstanceId);
            return skill;
        }
            
    }
}
