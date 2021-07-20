using FrameWork.Behaviour.Base;
using FrameWork.AI;
using Battle.Logic;

namespace FrameWork.Behaviour.Composite
{
    /// <summary>
    /// 顺序执行器
    /// ps 所有子节点按顺序执行,一旦遇到子节点失败，则返回失败。否则全部执行完返回成功。
    /// </summary>
    public class BNodeCompositeSequence : BNodeComposite
    {
        private int lastRunIndex;
        public BNodeCompositeSequence(/*BlackBoard bb,NodeParam data*/)/*:base(bb,data)*/
        { }

        protected override void Open()
        {
            base.Open();
        }
        public override BNodeExecuteState Execute()
        {
            base.Execute();
            lastRunIndex = 0;
            if(childNodes!=null&& childNodes.Count>0)
            {
                // 下次执行时如果有正在running的节点 则先执行一次running节点，根据state决定后续节点情况
                if(!forceReevaluate && runningNodeIndex >= 0)
                {
                    BNodeExecuteState state = childNodes[runningNodeIndex].Execute();
                    if (state == BNodeExecuteState.fail)
                    {
                        runningNodeIndex = -1;
                        return ReturnResultAndClose(state);
                    }
                    else if (state == BNodeExecuteState.Running)
                    {
                        return ReturnResultAndClose(state);
                    }
                    else
                    {
                        lastRunIndex = runningNodeIndex + 1;
                        runningNodeIndex = -1;
                    }
                }
                for(int i = lastRunIndex;i<childNodes.Count;i++)
                {
                    BNodeExecuteState state = childNodes[i].Execute();
                    if(state == BNodeExecuteState.fail)
                    {
                        // 有前面的节点在执行ing ，一旦遇到后继节点失败 则终止正在执行的节点
                        if(runningNodeIndex >=0 && runningNodeIndex!=i)
                        {
                            childNodes[runningNodeIndex].ForceEnd();
                        }
                        runningNodeIndex = -1;
                        return ReturnResultAndClose(state);
                    }
                    else if(state == BNodeExecuteState.Running)
                    {
                        // 有前面的节点在执行ing ，一旦遇到后继节点running 则终止正在执行的节点
                        if (runningNodeIndex >= 0 && runningNodeIndex !=i)
                        {
                            childNodes[runningNodeIndex].ForceEnd();
                        }
                        runningNodeIndex = i;
                        return ReturnResultAndClose(state);
                    }
                }
                return ReturnResultAndClose(BNodeExecuteState.Success);
            }
    
            return ReturnResultAndClose(BNodeExecuteState.fail);
        }
        public override void Close()
        {
            base.Close();
        }
        public override void Clear()
        {
            lastRunIndex = 0;
            base.Clear();
        }
    }
}
