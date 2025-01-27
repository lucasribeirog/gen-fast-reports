using System.Runtime.Serialization;

namespace gen_fast_report.Enums.Balistica
{
    public enum SoleiraArma
    {
        [EnumMember(Value="Sem soleira")]
        SemSoleira,
        [EnumMember(Value = "Borracha")]
        Borracha,
        [EnumMember(Value = "Plástico")]
        Plastico,
        [EnumMember(Value = "Polímero")]
        Polimero,
        [EnumMember(Value = "Gel")]
        Gel,
        [EnumMember(Value = "Couro")]
        Couro,
        [EnumMember(Value = "Espuma")]
        Espuma,
        [EnumMember(Value = "Metal")]
        Metal,
    }
}
