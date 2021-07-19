using FrameWork.AI;

namespace FrameWork.Behaviour.Base
{
    /// <summary>
    /// 行为节点
    /// </summary>
    public abstract class BNodeAction:BNode
    {
        public BNodeAction(BlackBoard bb,NodeParam data):base(bb,data)
        { }
        public override void Initialize()
        {
        }
        public override void Clear()
        {
            base.Clear();
        }

    }
}
