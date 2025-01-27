using System.Runtime.Serialization;

namespace gen_fast_report.Enums.Balistica
{
    public enum AlimentacaoArma
    {
        [EnumMember(Value = "Manual")]
        Manual = 0,
        [EnumMember(Value = "Pente")]
        Pente = 1,
        [EnumMember(Value = "Magazine")]
        Magazine = 2,
        [EnumMember(Value = "Tambor")]
        Tambor = 3,
        [EnumMember(Value = "BeltFed")]
        BeltFed = 4,
        [EnumMember(Value = "Tubular Magazine")]
        TubularMagazine = 5,
        [EnumMember(Value = "Tambor Giratório")]
        TamborGiratório = 6,
        [EnumMember(Value = "Pump")]
        Pump = 7
    }
}
