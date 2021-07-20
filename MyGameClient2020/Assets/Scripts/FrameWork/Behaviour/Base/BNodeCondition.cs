
using FrameWork.AI;
using Battle.Logic;
namespace FrameWork.Behaviour.Base
{
    /// <summary>
    /// 条件判定
    /// </summary>
    public abstract class BNodeCondition:BNode
    {
        public BNodeCondition(/*BlackBoard bb,NodeParam data*/)/*:base(bb,data)*/
        { }

        public override void Initialize(LogicAvatar avatar, BlackBoard bb, NodeParam data)
        {
            base.Initialize(avatar, bb, data);
        }
        public override void Clear()
        {
            base.Clear();
        }
    }
}
