using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle.Logic.MMath;
using Battle.Manager;
using FrameWork.Skill;
/// <summary>
/// avatar 技能组件
/// </summary>
namespace Battle.Logic
{
    public class AvatarComponentSkill : AvatarComponentBase
    {
        //主动技能列表
        private Queue<Skill> toUseActiveSkillQueue;
        //普通攻击列表
        private Queue<Skill> toUseNormalSkillQueue;
        // avatar 普通攻击数据
        public Dictionary<int, Skill> ownNormalSkillDict;
        // avatar 主动技能数据
        public Dictionary<int, Skill> ownActiveSkillDict;
        // avatar 被动技能数据
        public Dictionary<int, Skill> ownPassiveUsingSkillDict;


        public override void Initialize(LogicAvatar pAvatar)
        {
            base.Initialize(pAvatar);
            toUseNormalSkillQueue = new Queue<Skill>();
            toUseActiveSkillQueue = new Queue<Skill>();
            ownNormalSkillDict = new Dictionary<int, Skill>();
            ownActiveSkillDict = new Dictionary<int, Skill>();
            ownPassiveUsingSkillDict = new Dictionary<int, Skill>();
        }
        public override void Start()
        {
            base.Start();
            CreateOwnerSkills();
        }
        /// <summary>
        /// 释放技能
        /// </summary>
        /// 
        /// 强制结束当前技能



        private void CreateOwnerSkills()
        {
            if (pAvatar.elementType == WorldElementType.ThirdUser) return;
            if (pAvatar.avatarData.avatarTableLine.ContainsKey("normalSkill"))
            {
                string skillStr = pAvatar.avatarData.avatarTableLine["normalSkill"].AsString;
                string[] skills = skillStr.Split(';');
                for (int i = 0; i < skills.Length; i++)
                {
                    Skill s = LogicManager.Instance.skillManager.GetSkill(pAvatar, pAvatar.GetComponentMgr().Attribute.AvatarCamp, int.Parse(skills[i]));
                    if (!s.isPassiveSkillType() && !ownNormalSkillDict.ContainsKey(s.Id))
                    {
                        ownNormalSkillDict.Add(s.Id, s);
                    }
                }
            }

            if (pAvatar.avatarData.avatarTableLine.ContainsKey("activeSkill"))
            {
                //
                string skillStr = pAvatar.avatarData.avatarTableLine["activeSkill"].AsString;
                string[] skills = skillStr.Split(';');
                for (int i = 0; i < skills.Length; i++)
                {
                    Skill s = LogicManager.Instance.skillManager.GetSkill(pAvatar, pAvatar.GetComponentMgr().Attribute.AvatarCamp, int.Parse(skills[i]));
                    if (!s.isPassiveSkillType() && !ownActiveSkillDict.ContainsKey(s.Id))
                    {
                        ownActiveSkillDict.Add(s.Id, s);
                    }
                }
            }

            if (pAvatar.avatarData.avatarTableLine.ContainsKey("passiveSkill"))
            {
                string skillStr = pAvatar.avatarData.avatarTableLine["passiveSkill"].AsString;
                string[] skills = skillStr.Split(';');
                for (int i = 0; i < skills.Length; i++)
                {
                    string[] sd = skills[i].Split(':');
                    if (int.Parse(sd[0]) != 0)
                    {
                        Skill s = LogicManager.Instance.skillManager.GetSkill(pAvatar, pAvatar.GetComponentMgr().Attribute.AvatarCamp, int.Parse(sd[0]));
                        AddPassiveUsingSkill(s);
                    }
                }
            }
        }
        public void AddPassiveUsingSkill(Skill s)
        {
            if (pAvatar.IsDead) { return; }
            if (s.isPassive)
            {
                if (!ownPassiveUsingSkillDict.ContainsKey(s.InstanceId))
                {
                    ownPassiveUsingSkillDict.Add(s.InstanceId, s);
                    s.Start();
                }
                else
                {
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// 根据 距离，普攻 得到一个可使用的技能
        /// </summary>
        /// <param name="skillRange">距离</param>
        /// <param name="justNormal">是否为普攻</param>
        /// <returns></returns>
        public Skill GetCanUseSkill(Fix64 skillRange,bool justNormal = false)
        {
            Skill s = null;
            if(!justNormal)
            {
                foreach(var kv in ownActiveSkillDict)
                {
                    if((kv.Value.IsTargetIsSelf()|| kv.Value.GetUseRangeMax()>= skillRange) && kv.Value.IsCDOver())
                    {
                        if(s==null)
                        {
                            s = kv.Value;
                            continue;
                        }
                    }
                    if(kv.Value.GetUsePriority() < s.GetUsePriority())
                    {
                        s = kv.Value;
                    }
                    
                }
            }
            if(s==null)
            {
                foreach (var kv in ownNormalSkillDict)
                {
                    if ((kv.Value.IsTargetIsSelf() || kv.Value.GetUseRangeMax() >= skillRange) && kv.Value.IsCDOver())
                    {
                        if (s == null)
                        {
                            s = kv.Value;
                            continue;
                        }
                    }
                    if (kv.Value.GetUsePriority() < s.GetUsePriority())
                    {
                        s = kv.Value;
                    }
                }
            }
            return s;
        }

        public override void Clear()
        {
            toUseNormalSkillQueue = null;
            toUseActiveSkillQueue = null;
            ownNormalSkillDict = null;
            ownActiveSkillDict = null;
            ownPassiveUsingSkillDict = null;
            base.Clear();
        }
    }
}
