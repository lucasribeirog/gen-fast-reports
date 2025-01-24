using gen_fast_report.Enums.Balistica;
using gen_fast_report.Enums;

namespace gen_fast_report.Models.Controllers
{
    public class BalisticaRequest
    {
        public IFormFile? File { get; set; }
        public Gender Gender { get; set; }
        public Area Area { get; set; }
        public TipoBalistica TipoBalistica { get; set; }
    }
}
