using gen_fast_report.Attributes;
using gen_fast_report.Enums.Balistica;

namespace gen_fast_report.Models.DTOs
{


   public class BalisticaDTO
   {
    [TextMapping("Id Laudo")]
    public string? IdLaudo { get; set; }

      [TextMapping("Nº Laudo:")]
      public string? Laudo { get; set; }

      [TextMapping("Nº Requisição Pericial:")]
      public string? Requisicao { get; set; }

      [TextMapping("Nº REDS:")]
      public string? Reds { get; set; }

      [TextMapping("Unidade Requisitante:")]
      public string? UnidadeRequisitante { get; set; }

      [TextMapping("Autoridade Requisitante:")]
      public string? AutoridadeRequisitante { get; set; }

      [TextMapping("Nº Procedimento Origem:")]
      public string? Pcnet { get; set; }

      [TextMapping("Responsável pela Perícia:")]
      public string? ResponsavelPericia { get; set; }

      [TextMapping("Nº da FAV:")]
      public string? Fav { get; set; }

      [TextMapping("Exame em:")]
      public string? DescricaoExame { get; set; }

      [TextMapping("Data do início do exame:")]
      public string? DataInicio { get; set; }

      [TextMapping("Hora do início do exame:")]
      public string? HoraInicio { get; set; }

   }
}
