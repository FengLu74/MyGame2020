using Battle.Logic;
using FrameWork.AI;
using FrameWork.Behaviour.Base;
using System.Collections.Generic;
namespace FrameWork.Behaviour.Composite
{
    /// <summary>
    /// 并行执行器
    /// 所有子节点翻转  同时执行，全部节点执行成功，返回成功，否则返回失败（如果存在Running则 继续执行直到 Success，Fail）
    /// </summary>
    public class BNodeCompositeParallel:BNodeComposite
    {
        private int lastRunIndex;
        private Dictionary<int,BNodeExecuteState> childNodeExecuteStateDict = new Dictionary<int,BNodeExecuteState>();
        public BNodeCompositeParallel(/*BlackBoard bb,NodeParam data*/)/*:base(bb,data)*/
        { }
 
        protected override void Open()
        {
            base.Open();
            childNodes.Reverse();
            
        }
        public override BNodeExecuteState Execute()
        {
            base.Execute();
            lastRunIndex = 0;
            if(childNodes!=null && childNodes.Count>0)
            {

                for(int i=lastRunIndex;i<childNodes.Count; i++)
                {
                    if(childNodeExecuteStateDict.ContainsKey(i)&& childNodeExecuteStateDict[i]!= BNodeExecuteState.Running)
                    {
                        continue;
                    }
                    else
                    {
                        BNodeExecuteState state = childNodes[i].Execute();
                        if (childNodeExecuteStateDict.ContainsKey(i))
                        {
                            childNodeExecuteStateDict[i] = state;
                        }
                        else
                        { childNodeExecuteStateDict.Add(i, state); }
                        if (state == BNodeExecuteState.Success)
                        {
                            runningNodeIndex = -1;
                        }
                        else if (state == BNodeExecuteState.fail)
                        {
                            runningNodeIndex = -1;
                        }
                        else
                        {
                            runningNodeIndex = i;
                        }
                    }
                }
                ReturnResultAndClose(CheckBNodeStateDict()) ;
        
            }

            return ReturnResultAndClose(BNodeExecuteState.fail);
        }


        private BNodeExecuteState CheckBNodeStateDict()
        {
            BNodeExecuteState state = BNodeExecuteState.Success;
            if(childNodeExecuteStateDict.Count != childNodes.Count)
            {
                state = BNodeExecuteState.Running;
            }
            else
            {
                bool findRunning = false;
                bool findFail = false;
                foreach(var KeyValue in childNodeExecuteStateDict)
                {
                    if(KeyValue.Value == BNodeExecuteState.Running)
                    {
                        findRunning = true;
                    }
                    if (KeyValue.Value == BNodeExecuteState.fail)
                    {
                        findFail = true;
                    }
                }
                if (findRunning)
                {
                    state = BNodeExecuteState.Running;
                }
                else
                {
                    if (findFail)
                    {
                        state = BNodeExecuteState.fail;
                    }
                }
            }

            return state;
        }

        //private List<int> ListRandom(List<int> myList)
        //{

        //    Random ran = new Random();
        //    List newList = new List();
        //    int index = 0;
        //    int temp = 0;
        //    for (int i = 0; i < myList.Count; i++)
        //    {

        //        index = ran.Next(0, myList.Count - 1);
        //        if (index != i)
        //        {
        //            temp = myList[i];
        //            myList[i] = myList[index];
        //            myList[index] = temp;
        //        }
        //    }
        //    return myList;
        //}

        public override void Close()
        {
            base.Close();
        }
        public override void Clear()
        {
            lastRunIndex = 0;
            childNodeExecuteStateDict.Clear();
            base.Clear();
        }
    }
}
