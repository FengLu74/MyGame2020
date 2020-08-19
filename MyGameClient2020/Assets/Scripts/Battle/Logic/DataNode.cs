using MGame.GameBattle.Logic.MMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame.Logic
{
    public abstract class DataNode
    {
        public abstract int AsInt { get; }
        public abstract string AsString { get; }
        public abstract Fix64 AsFix64 { get; }
        public abstract bool AsBool { get; }
        public abstract FixVector3 AsFixVector3 { get; }

    }
    public class DataNodeNumber : DataNode
    {
        private int valueNum;

        public DataNodeNumber(int valueNum)
        {
            this.valueNum = valueNum;
        }

        public override int AsInt
        {
            get
            {
                return valueNum;
            }
        }
        public override string AsString
        {
            get
            {
                return valueNum.ToString();
            }
        }

        public override Fix64 AsFix64
        {
            get
            {
                return new Fix64(valueNum);
            }
        }

        public override bool AsBool
        {
            get
            {
                return valueNum > 0;
            }
        }

        public override FixVector3 AsFixVector3
        {
            get
            {
                return FixVector3.Zero;
            }
        }
    }

    public class DataNodeString : DataNode
    {
        public string valueStr;

        public DataNodeString(string valueStr)
        {
            this.valueStr = string.IsNullOrEmpty(valueStr) ? "" : valueStr;
        }

        public override int AsInt
        {
            get
            {
                int v = 0;
                if (int.TryParse(valueStr, out v))
                    return v;
                return 0;
            }
        }

        public override string AsString
        {
            get
            {
                return valueStr;
            }
        }

        public override Fix64 AsFix64
        {
            get
            {
                return Fix64.FractionStrToFix64(valueStr);
            }
        }

        public override bool AsBool
        {
            get
            {
                return valueStr == "true" || valueStr == "TRUE";
            }
        }

        public override FixVector3 AsFixVector3
        {
            get
            {
                return FixVector3.Zero;
            }
        }
    }

    public class DataNodeBool : DataNode
    {
        private bool value;

        public DataNodeBool(bool value)
        {
            this.value = value;
        }

        public override int AsInt
        {
            get
            {
                return value ? 1 : 0;
            }
        }

        public override string AsString
        {
            get
            {
                return value.ToString();
            }
        }

        public override Fix64 AsFix64
        {
            get
            {
                return (Fix64)AsInt;
            }
        }

        public override bool AsBool
        {
            get
            {
                return value;
            }
        }

        public override FixVector3 AsFixVector3
        {
            get
            {
                return FixVector3.Zero;
            }
        }
    }

    public class DataNodeVector3 : DataNode
    {
        private FixVector3 value;

        public DataNodeVector3(FixVector3 value)
        {
            this.value = value;
        }

        public override int AsInt
        {
            get
            {
                return 0;
            }
        }

        public override string AsString
        {
            get
            {
                return value.ToString();
            }
        }

        public override Fix64 AsFix64
        {
            get
            {
                return Fix64.Zero;
            }
        }

        public override bool AsBool
        {
            get
            {
                return true;
            }
        }

        public override FixVector3 AsFixVector3
        {
            get
            {
                return value;
            }
        }
    }
}
