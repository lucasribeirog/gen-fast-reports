using gen_fast_report.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gen_fast_report.Enums;
using gen_fast_report.Data;
using Microsoft.EntityFrameworkCore;
using gen_fast_report.Services;
using gen_fast_report.Models.Controllers;
using gen_fast_report.Validators;
using gen_fast_report.Services.IServices;

namespace gen_fast_report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandardReportController(StandardReportDbContext context,
        IFileValidationService fileValidationService,
        IUploadReportHandler uploadReportHandler) : ControllerBase
    {
        private readonly IFileValidationService _fileValidationService = fileValidationService;
        private readonly StandardReportDbContext _context = context;
        private readonly IUploadReportHandler _uploadReportHandler = uploadReportHandler;

        [HttpGet]
        public async Task<ActionResult<List<StandardReport>>> GetStandardReports()
        {
            return Ok(await _context.StandardReports.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<StandardReport>> GetStandardReportById(int id)
        {
            var standardReport = await _context.StandardReports.FindAsync(id);
            if (standardReport is null)
                return NotFound();
            return Ok(standardReport);
        }

        [HttpPost]
        public async Task<ActionResult<StandardReport>> AddStandardReport(StandardReportRequest newStandardReportRequest)
        {
            if (newStandardReportRequest == null)
                return BadRequest();

            var file = newStandardReportRequest.File;
            if (file == null)
                return BadRequest("Nenhum arquivo foi enviado.");

            if (!_fileValidationService.IsValidDocx(file))
                return BadRequest("O arquivo enviado não é um .docx válido.");

            StandardReport newStandardReport = await _uploadReportHandler.InsertByteArray(newStandardReportRequest);

            _context.StandardReports.Add(newStandardReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStandardReportById), new { id = newStandardReport.Id }, newStandardReport);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStandarReport(int id, StandardReportRequest newStandardReportRequest)
        {
            var standardReport = await _context.StandardReports.FindAsync(id);
            if (standardReport is null)
                return NotFound();

            var file = newStandardReportRequest.File;
            if (file == null)
                return BadRequest("Nenhum arquivo foi enviado.");

            if (!_fileValidationService.IsValidDocx(file))
                return BadRequest("O arquivo enviado não é um .docx válido.");

            StandardReport newStandardReport = await _uploadReportHandler.InsertByteArray(newStandardReportRequest);

            standardReport.Name = newStandardReport.Name;
            standardReport.Area = newStandardReport.Area;
            standardReport.File = newStandardReport.File;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStandarReport(int id)
        {
            var standardReport = await _context.StandardReports.FindAsync(id);
            if (standardReport is null)
                return NotFound();

            _context.StandardReports.Remove(standardReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
