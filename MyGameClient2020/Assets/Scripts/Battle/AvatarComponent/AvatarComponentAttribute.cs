using Battle.Logic.MMath;
using System.Collections.Generic;
using Battle.Logic.Numeric;
namespace Battle.Logic
{
    public class AvatarComponentAttribute : AvatarComponentBase
    {
        public Dictionary<int, Fix64> NumericDic ;

        public override void Initialize(LogicAvatar pAvatar)
        {
            base.Initialize(pAvatar);
            NumericDic = new Dictionary<int, Fix64>();
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

        }
        public override void Clear()
        {
            NumericDic = null;
            base.Clear();
        }
    }
}
