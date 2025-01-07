using gen_fast_report.Enums;

namespace gen_fast_report.Models
{
    public class StandardReport
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Area Area { get; set; } 
        //public IFormFile? File { get; set; }

    }
}
