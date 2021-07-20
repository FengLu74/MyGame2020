using Battle.Logic;
using FrameWork.AI;

namespace FrameWork.Behaviour.Base
{
    /// <summary>
    /// 行为节点
    /// </summary>
    public abstract class BNodeAction:BNode
    {
        public BNodeAction()
        { }
        public override void Initialize(LogicAvatar avatar, BlackBoard bb, NodeParam data)
        {
            base.Initialize(avatar, bb, data);
            base.SetStructType(NodeStructType.Action);
        }
        public override void Clear()
        {
            base.Clear();
        }

    }
}
