
using FrameWork.ReferencePool;
using Battle.Logic;
using FrameWork.AI;

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











        public  void Clear()
        {
            
        }
    }
}
