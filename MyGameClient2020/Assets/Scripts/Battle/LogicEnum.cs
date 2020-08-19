namespace MGame.Battle.Logic
{
    /// <summary>
    /// 战斗中单位枚举
    /// </summary>
    public enum WorldElementType
    {
        None = 0,
        AvatarHero = 1,           // 战斗中的英雄
        AvatarMonster = 2,        // 战斗中的怪物
        AvatarNpc = 3,            // 战斗中的NPC
        ThirdUser = 100,          // 战斗中负责处理其他作战单位的请求（例如释放英雄/敌人单位的技能）
    }
    /// <summary>
    /// 单位组件类型
    /// </summary>
    public enum AvatarComponentType
    {
        Start = 1,
        ComponentAttribute,
        ComponentState,
        ComponentSkill,
        ComponentMovement,
        ComponentAnim,
        ComponentEvent,
        ComponentAI,
        End
    }
    /// <summary>
    /// AI节点执行状态
    /// </summary>
    public enum BNodeExecuteState
    {
        Success,
        Runing,
        Fail,
    }
    

}
