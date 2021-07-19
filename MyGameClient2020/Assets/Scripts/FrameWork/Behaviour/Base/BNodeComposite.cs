using FrameWork.AI;
using System.Collections.Generic;

namespace FrameWork.Behaviour.Base
{
    /// <summary>
    /// 组合器
    /// </summary>
    public abstract class BNodeComposite:BNode
    {
        protected List<BNode> childNodes = new List<BNode>();
        protected bool forceReevaluate = false;
        protected int runningNodeIndex = -1;
        public BNodeComposite(BlackBoard bb,NodeParam data):base(bb,data)
        { }
        public override void Initialize()
        {
        }
        public BNode[] GetChildsArray()
        { return childNodes.ToArray(); }

        public void SetForceReevaluate(bool forceReevaluate)
        {
            this.forceReevaluate = forceReevaluate;
        }
        public void AddChild(BNode node)
        {
            childNodes.Add(node);
        }
        public override void ForceEnd()
        {
            for(int i =0;i<childNodes.Count;i++)
            {
                if(childNodes[i]!=null)
                {
                    childNodes[i].ForceEnd();
                }
            }
            base.ForceEnd();
            runningNodeIndex = -1;
        }

        public override void Clear()
        {
            base.Clear();
        }
    }
}
