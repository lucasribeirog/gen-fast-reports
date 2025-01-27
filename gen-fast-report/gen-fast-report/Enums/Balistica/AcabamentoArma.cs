using System.Runtime.Serialization;

namespace gen_fast_report.Enums.Balistica
{
    public enum AcabamentoArma
    {
        [EnumMember(Value = "Oxidada")]
        Oxidada = 0,
        [EnumMember(Value = "Cromada")]
        Cromada = 1,
        [EnumMember(Value = "Niquelada")]
        Niquelada = 2,
        [EnumMember(Value = "Fosfatada")]
        Fosfatada = 3,        
    }
}
