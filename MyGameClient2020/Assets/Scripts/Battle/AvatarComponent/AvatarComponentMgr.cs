using Battle.Logic.MMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// avatar 管理组件类
/// </summary>
/// 2021 - 7 - 21 后续建议使用 ECS 组件式开发
namespace Battle.Logic
{
    public class AvatarComponentMgr
    {
        private LogicAvatar pAvatar;
        public AvatarComponentAI AI;
        public AvatarComponentSkill Skill;
        //public AvatarComponentSkillBuffReceiver SkillBuffEffectReceiver;
        public AvatarComponentState State;
        public AvatarComponentAttribute Attribute;
        public AvatarComponentMovement Movement;
        public AvatarComponentAnim Anim;
        //public AvatarComponentEvent Event;

        public void Initialize(LogicAvatar pAvatar)
        {
            this.pAvatar = pAvatar;

        }
        public void Start()
        {

        }

    }
}
