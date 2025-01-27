using gen_fast_report.Enums.Balistica;
using gen_fast_report.Enums;
using System.ComponentModel.DataAnnotations;

namespace gen_fast_report.Models.Controllers
{
    public class BalisticaRequest
    {
        public required IFormFile File { get; set; }

        public required Gender Gender { get; set; }

        public required TipoBalistica BalisticType { get; set; }
        public required STRCS STRCS { get; set; }
        public IFormFile? Image { get; set; }

        public string? Caliber { get; set; }

        public string? Brand { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public required int Amount { get; set; }

        public int? EnvelopeNumber { get; set; }

        public TipoArma WeaponType { get; set; }

        public string? Model { get; set; }

        public string? SerialNumber { get;set; }

        public AcabamentoArma FinishWeapon { get;set; }

        public AlimentacaoArma WeaponFeed { get; set; }

        public CarregadorArma WeaponCharger { get; set; }

        public int CapacityCharger { get; set; }

        public SoleiraArma SoleiraArma { get; set; }

        public string? PipeMeasurement { get; set; }

        public string? TotalMeasure { get;set;}

        public ResultadoExameBalistica BallisticsExamResult { get; set; }

    }
}
