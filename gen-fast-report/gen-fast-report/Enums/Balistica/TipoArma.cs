using System.Runtime.Serialization;

namespace gen_fast_report.Enums.Balistica
{
    public enum TipoArma
    {
        [EnumMember(Value = "Polveira")]
        Polveira = 0,
        [EnumMember(Value = "Garrucha")]
        Garrucha = 1,
        [EnumMember(Value = "Revólver")]
        Revólver = 2,
        [EnumMember(Value = "Espingarda")]
        Espingarda = 3,
    }
}
