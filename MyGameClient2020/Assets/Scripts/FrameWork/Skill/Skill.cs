using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle.Logic;
using FrameWork.Camp;
using FrameWork.ReferencePool;
namespace FrameWork.Skill
{

    public class Skill : IReference
    {
        public int Id;
        public int InstanceId;
        public bool isPassive;
        public int isNormalAttack;
        public SkillType skillType;




        public SkillType SkillType
        {
            get { return skillType; }
            set { skillType = value; }
        }
        public bool isPassiveSkillType()
        {
            return skillType == SkillType.PassiveSkill;
        }
        public bool isNormalSkillType()
        {
            return skillType == SkillType.NormalSkill;
        }
        public bool isDynamicSkillType()
        {
            return skillType == SkillType.DynamicSkill;
        }
        private Action<int> skillStartAction;
        private Action<int> skillUpdateAction;
        private Action<int> skillEndAction;
        private Action<int> skillDestroyAction;
        public Skill()
        {

        }
        public void Clear()
        {

        }


        public void Initialize(LogicAvatar caster, BattleCampInfo skillCamp, Dictionary<string, string> data, int instanceId)
        {

        }
        public void InitExecuteAction(Action<int> start, Action<int> update, Action<int> end, Action<int> destroy)
        {
            skillStartAction = start;
            skillUpdateAction = update;
            skillEndAction = end;
            skillDestroyAction = destroy;
        }
        public void Start()
        {

        }

        public void Update()
        {

        }

        public void End()
        {

        }

        public void Destroy()
        {

        }



    }
}
