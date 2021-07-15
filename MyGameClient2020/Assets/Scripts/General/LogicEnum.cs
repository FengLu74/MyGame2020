using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame.GameBattle.Logic
{
    public enum WorldElementType
    {
        None,

        Start,////////////////////////////////Start////////////////////////////////////////////////////

        AvatarHero,                 //英雄对象

        AvatarNpc,                  // NPC对象

        AvatarMonster,              // 怪物对象

        ElementEffectObject,        //特效表现对象（GameObject对象）

        Buttle,                     // 子弹逻辑对象

        Bounce,                     // 弹射对象

        ThirdUser,                  //用于释放奇遇卡，圣物等技能相关功能的avator对象，战场观察者

        End, ////////////////////////////////End////////////////////////////////////////////////////
    }




    public enum AvatarComponentType
    {
        Start = 1,
        ComponentAttribute,
        ComponentState,
        ComponentSkill,
        ComponentSkillBuffEffectReceiver,
        ComponentMovement,
        ComponentAnim,
        ComponentEvent,
        ComponentAI,
        ComponentSpecialAI,
        End
    }

    public enum BNodeExecuteState
    {
        Success,
        Running,
        fail,
    }

    public enum BNodeType
    {
        eDecoratorNot,
        eCompositeSelector,
        eCompositeSequence,
        eCompositeParallelSelector,
        eCompositeParallelSequence,
        eCompositeParallelHybird,
        eConditionAttackRangeHasEnemy,
        eActionAttackEnemy,
        eConditionVigilanceRangeHasEnemy,
        eActionMoveToEnemy,
        eActionIdle,
        eBNodeActionFear,
        eBNodeConditionIsFearState,
        eBNodeActionSeduce,
        eBNodeConditionIsSeduceState,
        eBNodeActionTaunt,
        eBNodeConditionIsTauntState,
        eBNodeActionMoveStraight,
        eBNodeConditionIsRushState,
        eBNodeActionRush,
        eBNodeConditionIsRepulseState,
        eBNodeActionRepulse,
        eBNodeActionClearLockEnemy,
        eBNodeConditionIsClearLockEnemy
    }


    public enum CommonSkillState
    {
        Disable,
        Initialized,
        Ticking,
        End,
    }

    public enum SkillFollowType
    {
        target = 1,
        pos = 2,
    }

    public enum EffectRenderPriorityType
    {
        casterAndHitPosHigest = 1,//施法者和受击者里面取最高
        casterAndHitPosLowest = 2,//施法者和受击者里面取最低
        hitPosHigest = 3,//受击者位置取最高
        hitPosLowest = 4,//受击者位置取最低
        casterPos = 5,//施法者位置
    }

    public enum BuffEffectType
    {
        AddShield = 1,//添加护盾
        addSkill = 2,//添加技能
        control = 3,//控制
        replaceSkill = 4,//替换技能
        attributeModify = 5,//属性修改
        resistControl = 6,//抵抗控制
        reduceCost = 7,//减少费用
        restoreEnergy = 8,//能量恢复
        attributeModifyTwo = 9,// 修正属性（给圣物，奇遇卡使用）  
        cardPowerUp = 10,// 卡片强化
        actionPoinLimite = 11,
        actionPoinTimeBack = 12,//回到过去
        actionAddCard = 13,
        skillOrbs = 14, // 技能法球
        buffHurt = 15,//BUFF触发伤害，（受BUFF影响）
    }

    public enum BulletType
    {
        lineForward = 1,//子弹只有方向，直线
        lineTarget = 2,//朝向目标，直线
        linePos = 3,//子弹朝向位置，直线
        parabolaTarget = 4,//朝向目标，抛物线
        parabolaPos = 5,//朝向位置，抛物线        
    }

    public class AvatarPassiveState
    {
        public static readonly int noNormalAttack = 1;//无法普通攻击   (但是可以释放技能，只要是配置表)
        public static readonly int noSkillAttack = 2;//无法技能攻击   （但是可以普通攻击，只需要判断isAttack这个）
        public static readonly int noMoving = 4;//无法移动
        public static readonly int taunt = 8;//强制攻击施法者（嘲讽）
        public static readonly int fear = 16;//强制远离施法者（恐惧）
        public static readonly int seduce = 32;//强制靠近施法者（魅惑）
        public static readonly int rush = 64;//强制靠近目标者（冲锋）
        public static readonly int repulse = 128;//强制远离目标者（击退）
        public static readonly int clearEnemy = 256;//强制清除紫色锁定的敌人（清除锁敌）
    }
}
