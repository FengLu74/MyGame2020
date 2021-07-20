using Battle.Logic;
using FrameWork.AI;

namespace FrameWork.Behaviour.Base
{
    public abstract class BNodeDecorator:BNode
    {
        public BNodeDecorator(/*BlackBoard bb,NodeParam data*/)/*:base(bb,data)*/
        {
        }
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
