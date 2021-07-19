using System;

/// <summary>
/// avatar AI组件
/// ps： 逻辑帧轮询根节点，暂不考虑打断机制。
/// 
/// 2021-7-19
/// 但是有个问题是如果avatar 一直持续技能打怪中ing，有外界环境改变（如敌人释放一个控制技能）
/// 这时只能是轮询根节点判断控制状态
/// </summary>
namespace Battle.Logic
{
    public class AvatarComponentAI : AvatarComponentBase
    {


        public override void Clear()
        {
            base.Clear();
        }
    }
}
