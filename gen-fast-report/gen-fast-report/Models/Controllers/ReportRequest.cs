﻿using gen_fast_report.Enums;
using gen_fast_report.Enums.Balistica;

namespace gen_fast_report.Models.Controllers
{
    public class ReportRequest
    {
        public IFormFile? File{ get; set; }
        public Gender Gender { get; set; }
        public Area Area { get; set; }
        public TipoBalistica TipoBalistica { get; set; }
    }
}
