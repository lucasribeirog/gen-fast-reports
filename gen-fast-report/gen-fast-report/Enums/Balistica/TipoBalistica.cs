using System.Runtime.Serialization;

namespace gen_fast_report.Enums.Balistica
{
    public enum TipoBalistica
    {
        [EnumMember(Value = "Arma de Fogo")]
        ArmaFogo = 0,
        [EnumMember(Value = "Munição")]
        Municao = 1
    }
}
