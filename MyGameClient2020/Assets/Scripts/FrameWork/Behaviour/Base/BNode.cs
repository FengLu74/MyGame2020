
using FrameWork.ReferencePool;
using Battle.Logic;
using FrameWork.AI;
using FrameWork.Behaviour.Composite;


namespace FrameWork.Behaviour.Base
{
    /// <summary>
    /// 节点配置数据参数
    /// </summary>
    public class NodeParam
    {
        public string nodeName;
        public string[] param;
    }

    /// <summary>
    /// 行为树节点基类
    /// </summary>
    public abstract class BNode : IReference
    {
        /// <summary>
        /// 节点运行状态
        /// </summary>
        public BNodeExecuteState executeState;
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName;
        /// <summary>
        /// 行为树归属的avatar
        /// </summary>
        protected LogicAvatar logicAvatar;
        /// <summary>
        /// 节点状态
        /// </summary>
        public BNodeOperationState runState = BNodeOperationState.None;
        /// <summary>
        /// 黑板数据
        /// </summary>
        protected BlackBoard bbChunk;
        /// <summary>
        /// 节点数据
        /// </summary>
        protected NodeParam nodeData;

        //public BNode(BlackBoard bb,NodeParam data)
        //{
        //    bbChunk = bb;
        //    nodeData = data;
        //    runState = BNodeOperationState.Start;
        //    NodeName = data.nodeName;
        //}
        public BNode()
        {
        }
        public virtual void Initialize(LogicAvatar avatar,BlackBoard bb,NodeParam data)
        {
            bbChunk = bb;
            nodeData = data;
            runState = BNodeOperationState.Start;
            NodeName = data.nodeName;
            logicAvatar = avatar;
        }

        protected virtual void Open()
        { }
        public virtual void Close()
        {
            runState = BNodeOperationState.End;
        }
        public virtual void ForceEnd()
        {
            runState = BNodeOperationState.End;
        }

        public string GetNodeName()
        { return NodeName; }

        public virtual BNodeExecuteState Execute()
        {
            if(runState!= BNodeOperationState.Running)
            {
                Open();
                runState = BNodeOperationState.Running;
            }
            return BNodeExecuteState.Success;
        }
        protected BNodeExecuteState ReturnResultAndClose(BNodeExecuteState state)
        {
            executeState = state;
            switch(state)
            {
                case BNodeExecuteState.Success:
                    Close();
                    break;
                case BNodeExecuteState.fail:
                    Close();
                    break;
                case BNodeExecuteState.Running:
                    break;
            }
            return state;
        }

        public static BNode NodeFactory(LogicAvatar avatar,BNodeType bType,BlackBoard bb,NodeParam data)
        {
            BNode bNode = null;
            switch(bType)
            {
                case BNodeType.eCompositeSelector:
                    bNode = ReferencePool.ReferencePool.Acquire<BNodeCompositeSelector>();
                    break;
                case BNodeType.eCompositeSequence:
                    bNode = ReferencePool.ReferencePool.Acquire<BNodeCompositeSequence>();
                    break;
                case BNodeType.eCompositeParallel:
                    bNode = ReferencePool.ReferencePool.Acquire<BNodeCompositeParallel>();
                    break;
            }
            if(bNode!=null)
            {
                bNode.Initialize(avatar, bb, data);
            }
            return bNode;
        }

    



        public virtual void Clear()
        {
            
        }
    }
}
