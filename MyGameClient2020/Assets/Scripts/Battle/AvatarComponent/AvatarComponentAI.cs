using System;
using Common.General;
using FrameWork.AI;
using FrameWork.Behaviour.Base;
using System.Collections.Generic;
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

        private void createBehaviourTree()
        {
            blackBoard = new BlackBoard();
            string aiCfgName = pAvatar.avatarData.avatarTableLine["behaviourTree"].AsString;
            int nodeId = pAvatar.avatarData.avatarTableLine["ai"].AsInt;
            BinaryTable table = BinaryTableLoader.Instance.GetBinaryTable(aiCfgName);
            nodeRoot = RecursionCreateTree(table, nodeId);
        }
        private BNode RecursionCreateTree(BinaryTable binaryTable,int rootId)
        {
            Dictionary<string, string> lineData = binaryTable.GetLineData(rootId);
            BNodeType nodeType = (BNodeType)Enum.Parse(typeof(BNodeType), lineData["nodeType"]);
            string childStr = null;
            lineData.TryGetValue("childs", out childStr);
            string paraStr;
            NodeParam nodeParam = new NodeParam();
            if (lineData.TryGetValue("para", out paraStr)) 
            {
                nodeParam.param = paraStr.Split(';');
            }
            nodeParam.nodeName = rootId.ToString();

            BNode node = BNode.NodeFactory(pAvatar, nodeType, blackBoard, nodeParam);
            switch(nodeType)
            {

                case BNodeType.eCompositeSelector:
                case BNodeType.eCompositeSequence:

                    if (string.IsNullOrEmpty(childStr))
                    {
                    }
                    else
                    {
                        string[] childSplit = childStr.Split('|');
                        for (int i = 0; i < childSplit.Length; i++)
                        {
                            BNode childNode = CreateNode(binaryTable, rootId, childSplit[i]);
                            if (childNode != null)
                            {
                                BNodeComposite nodeComposite = (BNodeComposite)node;
                                nodeComposite.AddChild(childNode);

                                string forceReevaluateStr;
                                if (lineData.TryGetValue("forceReevaluate", out forceReevaluateStr))
                                {
                                    nodeComposite.SetForceReevaluate(forceReevaluateStr == "1");
                                }
                                string sortTypeStr;
                                if (lineData.TryGetValue("sortType", out sortTypeStr))
                                {
                                    //nodeComposite.SortChilds();
                                }
                            }
                        }
                    }
                    break;
            }
            return node;
        }
        BNode CreateNode(BinaryTable table, int nodeId, string childStr)
        {
            if (string.IsNullOrEmpty(childStr))
            {
                //GameLog.LogError(string.Format("the behaviour tree node's child should not be empty in {0} at nodeId {1} !", table.tableName, nodeId));
                return null;
            }

            int childNodeId;
            if (int.TryParse(childStr, out childNodeId))
            {
                return RecursionCreateTree(table, childNodeId);
            }
            else
            {
                //GameLog.LogError(string.Format("the behaviour tree node's child string is error in {0} at nodeId {1} !", table.tableName, nodeId));
                return null;
            }
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
