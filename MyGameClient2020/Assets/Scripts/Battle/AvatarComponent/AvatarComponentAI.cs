using System;
using FrameWork.AI;
using FrameWork.Behaviour.Base;
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

        private BlackBoard blackBoard;
        private BNode nodeRoot;

        public override void Initialize(LogicAvatar pAvatar)
        {
            base.Initialize(pAvatar);
            //目前先试用配置表格方式实现 后续要通过编辑器可视化数据
            createBehaviourTree();
        }
        public override void Update()
        {
            base.Update();
            //轮询根节点
            nodeRoot.Execute();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            nodeRoot.Close();
            nodeRoot.ReleaseChilds();
        }

        private BNode createBehaviourTree()
        {
            BNode node = null;
            return node;
        }


        public BNode GetRootNode()
        {
            return nodeRoot;
        }
        public override void Clear()
        {
            blackBoard = null;
            nodeRoot = null;
            base.Clear();
        }
    }
}
