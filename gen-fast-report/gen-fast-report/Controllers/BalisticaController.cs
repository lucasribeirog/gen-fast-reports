using gen_fast_report.Models.Controllers;
using gen_fast_report.Services.IServices;
using gen_fast_report.Validators;
using Microsoft.AspNetCore.Mvc;

namespace gen_fast_report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalisticaController(IFileValidationService fileValidationService,
        IManageBalisticaReportService manageBalisticaReportService) : ControllerBase
    {
        private readonly IFileValidationService _fileValidationService = fileValidationService;
        private readonly IManageBalisticaReportService _manageBalisticaReportService = manageBalisticaReportService;

        [HttpPost]
        public async Task<IActionResult> GenerateReport(BalisticaRequest balisticaRequest)
        {
            if (balisticaRequest == null)
                return BadRequest();

            var file = balisticaRequest.File;
            if (file == null)
                return BadRequest("Nenhum arquivo foi enviado.");

            if (!_fileValidationService.IsValidDocx(file))
                return BadRequest("O arquivo enviado não é um .docx válido.");

            await _manageBalisticaReportService.WriteNewReport(balisticaRequest);

            return NoContent();
        }
    }
}
