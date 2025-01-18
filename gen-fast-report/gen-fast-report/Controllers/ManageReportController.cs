using gen_fast_report.Models.Controllers;
using gen_fast_report.Models;
using gen_fast_report.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using gen_fast_report.Validators;

namespace gen_fast_report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageReportController(IManageReportService manageReportService,
        IFileValidationService fileValidationService) : ControllerBase
    {
        private readonly IManageReportService _manageReportService = manageReportService;
        private readonly IFileValidationService _fileValidationService = fileValidationService;
        
        [HttpPost]
        public async Task<IActionResult> GenerateReport(ReportRequest reportRequest)
        {
            if (reportRequest == null)
                return BadRequest();

            var file = reportRequest.File;
            if (file == null)
                return BadRequest("Nenhum arquivo foi enviado.");

            if (!_fileValidationService.IsValidDocx(file))
                return BadRequest("O arquivo enviado não é um .docx válido.");

            await _manageReportService.WriteNewReport(reportRequest);

            return NoContent();
        }
    }
}
