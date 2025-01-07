using gen_fast_report.Enums;

namespace gen_fast_report.Models.Controllers
{
    public class StandardReportRequest
    {
        public string? Name { get; set; }
        public Area Area { get; set; }
        public IFormFile File { get; set; }
    }
}
