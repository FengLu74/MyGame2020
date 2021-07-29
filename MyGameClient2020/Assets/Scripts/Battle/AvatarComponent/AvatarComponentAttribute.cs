using Battle.Logic.MMath;
using System.Collections.Generic;
using Battle.Logic.Numeric;
using FrameWork.Camp;

namespace Battle.Logic
{
    public class AvatarComponentAttribute : AvatarComponentBase
    {
        public Dictionary<int, Fix64> NumericDic ;
        public BattleCampInfo AvatarCamp;
        public override void Initialize(LogicAvatar pAvatar)
        {
            base.Initialize(pAvatar);
            NumericDic = new Dictionary<int, Fix64>();
            CreateAttributeDict();
        }
        private void CreateAttributeDict()
        {
            
        }






        public Fix64 this[NumericType numeric]
        {
            get {
                return getValueByKey((int)numeric);
            }
            set {
                Fix64 v = getValueByKey((int)numeric);
                if (v == value) return;

                NumericDic[(int)numeric] = value;
                updateAttr(numeric);
            }
        }

        private Fix64 getValueByKey(int key)
        {
            Fix64 value = Fix64.Zero;
            NumericDic.TryGetValue(key, out value);
            return value;
        }


        private void updateAttr(NumericType numericType)
        {
            if (numericType < NumericType.Max) // 基础数值 不必更新
            {
                return;
            }
            int final = (int)numericType / 10;
            int bas = final * 10 + 1;
            int pct = final * 10 + 2;
            int add = final * 10 + 3;

            int max = final + 1;
            //                 基础值                 附加的绝对值               附加的百分比值
            Fix64 result = getValueByKey(bas) + getValueByKey(add) + getValueByKey(bas) * getValueByKey(pct) / 1000 ;
            NumericDic[final] = result;
            //数值变化通知
        }
        public override void Clear()
        {
            NumericDic = null;
            base.Clear();
        }
    }
}
