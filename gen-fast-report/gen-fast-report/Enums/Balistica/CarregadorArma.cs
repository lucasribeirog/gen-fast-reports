using System.Runtime.Serialization;

namespace gen_fast_report.Enums.Balistica
{
    public enum CarregadorArma
    {
        [EnumMember(Value = "Sem Carregador")]
        SemCarregador = 0,
        [EnumMember(Value = "Removível")]
        Removivel = 1,
        [EnumMember(Value = "Fixo")]
        Fixo = 2,
        [EnumMember(Value = "Tubular")]
        Tubular = 3,
        [EnumMember(Value = "Tambor")]
        Tambor = 4,
        [EnumMember(Value = "Fita")]
        Fita = 5,
        [EnumMember(Value = "Box Magazine")]
        BoxMagazine = 6,
    }
}
