using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame.GameBattle.Logic
{
    public class AvatarComponentState : AvatarComponentBase
    {
        private int passivePlayAnimStateCounter = 0;
        private Dictionary<int, Dictionary<int, int>> avatarPassiveStateDict = new Dictionary<int, Dictionary<int, int>>();

        public bool IsPassivePlayingAnim
        {
            get
            {
                return passivePlayAnimStateCounter > 0;
            }
        }

        public override void Initialize(LogicAvatar pAvatar)
        {
            base.Initialize(pAvatar);
        }

        #region passiveState


        public void AddPassiveState(int stateType, int key, int count = 1)
        {
            Dictionary<int, int> dict = null;
            if (!avatarPassiveStateDict.TryGetValue(stateType, out dict))
            {
                dict = new Dictionary<int, int>();
                avatarPassiveStateDict.Add(stateType, dict);
            }
            if (dict.ContainsKey(key))
            {
                dict[key] += count;
            }
            else
            {
                dict.Add(key, count);
            }
            if (stateType == AvatarPassiveState.noNormalAttack)
            {
               //暂时屏蔽 pAvatar.GetComponentMgr().Skill.CheckBreakCurrentUsingSkill(Skill.SkillBreakType.breakByNoUseNormalAttack);
            }
            else if (stateType == AvatarPassiveState.noSkillAttack)
            {
               //暂时屏蔽 pAvatar.GetComponentMgr().Skill.CheckBreakCurrentUsingSkill(Skill.SkillBreakType.breakByNoUseSkill);
            }
            else if (stateType == AvatarPassiveState.taunt)
            {
                avatarPassiveStateDict.Remove(AvatarPassiveState.fear);
                avatarPassiveStateDict.Remove(AvatarPassiveState.seduce);
                avatarPassiveStateDict.Remove(AvatarPassiveState.rush);
                avatarPassiveStateDict.Remove(AvatarPassiveState.repulse);
                avatarPassiveStateDict.Remove(AvatarPassiveState.clearEnemy);
            }
            else if (stateType == AvatarPassiveState.fear)
            {
                avatarPassiveStateDict.Remove(AvatarPassiveState.taunt);
                avatarPassiveStateDict.Remove(AvatarPassiveState.seduce);
                avatarPassiveStateDict.Remove(AvatarPassiveState.rush);
                avatarPassiveStateDict.Remove(AvatarPassiveState.repulse);
                avatarPassiveStateDict.Remove(AvatarPassiveState.clearEnemy);
            }
            if (stateType == AvatarPassiveState.seduce)
            {
                avatarPassiveStateDict.Remove(AvatarPassiveState.fear);
                avatarPassiveStateDict.Remove(AvatarPassiveState.taunt);
                avatarPassiveStateDict.Remove(AvatarPassiveState.rush);
                avatarPassiveStateDict.Remove(AvatarPassiveState.repulse);
                avatarPassiveStateDict.Remove(AvatarPassiveState.clearEnemy);
            }
            if (stateType == AvatarPassiveState.rush)
            {
                avatarPassiveStateDict.Remove(AvatarPassiveState.fear);
                avatarPassiveStateDict.Remove(AvatarPassiveState.taunt);
                avatarPassiveStateDict.Remove(AvatarPassiveState.seduce);
                avatarPassiveStateDict.Remove(AvatarPassiveState.repulse);
                avatarPassiveStateDict.Remove(AvatarPassiveState.clearEnemy);
            }
            if (stateType == AvatarPassiveState.repulse)
            {
                avatarPassiveStateDict.Remove(AvatarPassiveState.fear);
                avatarPassiveStateDict.Remove(AvatarPassiveState.taunt);
                avatarPassiveStateDict.Remove(AvatarPassiveState.seduce);
                avatarPassiveStateDict.Remove(AvatarPassiveState.rush);
                avatarPassiveStateDict.Remove(AvatarPassiveState.clearEnemy);
            }
            if (stateType == AvatarPassiveState.clearEnemy)
            {
                avatarPassiveStateDict.Remove(AvatarPassiveState.fear);
                avatarPassiveStateDict.Remove(AvatarPassiveState.taunt);
                avatarPassiveStateDict.Remove(AvatarPassiveState.seduce);
                avatarPassiveStateDict.Remove(AvatarPassiveState.rush);
                avatarPassiveStateDict.Remove(AvatarPassiveState.repulse);
            }
        }

        public void RemovePassiveState(int stateType, int key, int count = 1)
        {
            Dictionary<int, int> dict = null;
            if (avatarPassiveStateDict.TryGetValue(stateType, out dict))
            {

                if (dict.ContainsKey(key))
                {
                    dict[key] -= count;
                    if (dict[key] <= 0)
                    {
                        dict.Remove(key);
                    }
                }
            }
        }

        public void RemovePassiveStateTwo(int stateType)
        {
            Dictionary<int, int> dict = null;
            if (avatarPassiveStateDict.TryGetValue(stateType, out dict))
            {

                dict.Clear();
            }
        }
        public bool IsCurrentPassiveState(int stateType)
        {
            return avatarPassiveStateDict.ContainsKey(stateType) && avatarPassiveStateDict[stateType].Count > 0;
        }

        #endregion

        public void AddPassivePlayAnimCount()
        {
            passivePlayAnimStateCounter++;
        }

        public void ReducePassivePlayAnimCount()
        {
            passivePlayAnimStateCounter--;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
