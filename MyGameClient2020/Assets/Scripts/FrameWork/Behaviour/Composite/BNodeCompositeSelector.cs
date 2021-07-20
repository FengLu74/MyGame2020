
using FrameWork.Behaviour.Base;
using FrameWork.AI;
using Battle.Logic;


namespace FrameWork.Behaviour.Composite
{
    /// <summary>
    /// 顺序执行器
    /// ps 所有子节点按顺序执行，一旦子节点成功 则返回成功。否则全部执行完所有子节点返回失败
    /// </summary>
    public class BNodeCompositeSelector:BNodeComposite
    {
        private int lastRunIndex;
        public BNodeCompositeSelector(/*BlackBoard bb,NodeParam data*/)/*:base(bb,data)*/
        { }
        protected override void Open()
        {
            base.Open();
        }
        public override BNodeExecuteState Execute()
        {
            base.Execute();
            lastRunIndex = 0;
            if(childNodes!=null&& childNodes.Count > 0 )
            {
                if(!forceReevaluate&& runningNodeIndex>=0)
                {
                    BNodeExecuteState state = childNodes[runningNodeIndex].Execute();
                    if (state == BNodeExecuteState.Success)
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
                for (int i = lastRunIndex; i < childNodes.Count; i++)
                {
                    BNodeExecuteState state = state = childNodes[i].Execute();
                    if (state == BNodeExecuteState.Success)
                    {
                        if (runningNodeIndex >= 0 && runningNodeIndex != i)
                        {
                            childNodes[runningNodeIndex].ForceEnd();
                        }
                        runningNodeIndex = -1;
                        return ReturnResultAndClose(state);
                    }
                    else if (state == BNodeExecuteState.Running)
                    {
                        if (runningNodeIndex >= 0 && runningNodeIndex != i)
                        {
                            childNodes[runningNodeIndex].ForceEnd();
                        }
                        runningNodeIndex = i;
                        return ReturnResultAndClose(state);
                    }
                }
                return ReturnResultAndClose(BNodeExecuteState.fail);
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
