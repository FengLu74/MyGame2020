using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Logic.Numeric
{
    /// <summary>
    /// 数值类型
    /// 
    /// ps avatar 初始化时 Hp = MaxHp = HpBase 
    /// </summary>
    public enum NumericType
    {
        Max = 10000,          // 小于该值 表明是 avatar 基础数值，大于该值 是战场上 动态附加的数值

        Level = 1001,
        MaxLevel = 1002,

        Hp = 1003,            // 当前状态血量值
        HpBase = Hp*10+1,     // 配置表基础值 根据Avatar等级,星级拿到的配置表数据

        HpPct = Hp*10+2,      // 附加最大生命值的百分比（ 比如有一个buff 使avatar 获得 最大生命值的20% 持续5s 还原 ）

        HpAdd = Hp*10+3,      // 附加得绝对值 ( 比如有一个buff 使avatar 增加/减少500 生命值 持续5s 后还原 )

        MaxHp = 1004,         // 

        /// <summary>
        /// ////////////////////// 攻击 ///////////////////////////////////////////////
        /// </summary>
        Ack = 1005,
        AckBase = Ack * 10+1,
        
        AckPct = Ack*10+2,

        AckAdd = Ack*10+3,

        AckMax = 1006,
        /// <summary>
        /// ////////////////////// 速度 ///////////////////////////////////////////////
        /// </summary>
        Speed = 1007,

        SpeedBase = Speed*10+1,

        SpeedPct = Speed*10+2,

        SpeedAdd = Speed*10+3,

        SpeedMax = 1008,
        /// <summary>
        /// ////////////////////// 攻击速度 ///////////////////////////////////////////////
        /// </summary>
        ActSpeed = 1009,

        ActSpeedBase = ActSpeed*10+1,

        ActSpeedPct = ActSpeed*10+2,

        ActSpeedAdd = ActSpeed*10+3,

        ActSpeedMax = 1010,
        /// <summary>
        /// ////////////////////// 暴击率 ///////////////////////////////////////////////
        /// </summary>
        Crt = 1011,

        CrtBase = Crt*10+1,

        CrtPct = Crt*10+2,

        CrtAdd = Crt*10+3,

        CrtMax = 1012,
        /// <summary>
        /// ////////////////////// 暴击伤害 ///////////////////////////////////////////////
        /// </summary>
        Crtdam = 1013,
    
        CrtdamBase = Crtdam*10+1,

        CrtdamPct = Crtdam*10+2,

        CrtdamAdd = Crtdam*10+3,

        CrtdamMax = 1014,
        /// <summary>
        /// ////////////////////// 命中值 ///////////////////////////////////////////////
        /// </summary>
        /// 
        Hit = 1015,
        HitBase = Hit*10+1,

        HitPct = Hit*10+2,
        HitAdd = Hit*10+3, 
        HitMax = 1016,
        /// <summary>
        /// ////////////////////// 命中抵抗 ///////////////////////////////////////////////
        /// </summary>
        ///
        HitLess = 1017,
        HitLessBase = HitLess*10+1,
        HitLessPct  = HitLess*10+2,
        HitLessAdd = HitLess*10+3,
        HitLessMax = 1018,
        /// <summary>
        /// ////////////////////// 吸血 ///////////////////////////////////////////////
        /// </summary>
        ///
        Vampire = 1019,
        VampireBase = Vampire*10+1,
        VampirePct = VampireBase*10+2,
        VampireAdd = VampirePct*10+3,
        VampireMax = 1020,
        /// <summary>
        /// ////////////////////// 物理伤害系数（物理增伤） ///////////////////////////////////////////////
        /// </summary>
        ///
        PhysicalInjury = 1021,
        PhysicalInjuryBase = PhysicalInjury * 10 + 1,
        PhysicalInjuryPct = PhysicalInjury * 10 + 2,
        PhysicalInjuryAdd = PhysicalInjury * 10 + 3,
        PhysicalInjuryMax = 1022,

        /// <summary>
        /// ////////////////////// 魔法伤害系数（魔法增伤） ///////////////////////////////////////////////
        /// </summary>
        ///
        MagicInjury = 1023,
        MagicInjuryBase = MagicInjury * 10 + 1,
        MagicInjuryPct = MagicInjury * 10 + 2,
        MagicInjuryAdd = MagicInjury * 10 + 3,
        MagicInjuryMax = 1024,
        /// <summary>
        /// ////////////////////// 治疗系数（治疗增加/减少） ///////////////////////////////////////////////
        /// </summary>
        ///
        Treat= 1025,
        TreatBase = Treat * 10 + 1,
        TreatPct = Treat * 10 + 2,
        TreatAdd = Treat * 10 + 3,
        TreatMax = 1026,


    }
}
