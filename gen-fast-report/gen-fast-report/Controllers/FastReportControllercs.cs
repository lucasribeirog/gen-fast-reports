using gen_fast_report.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gen_fast_report.Enums;
using gen_fast_report.Data;
using Microsoft.EntityFrameworkCore;

namespace gen_fast_report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FastReportControllercs(StandardReportDbContext context) : ControllerBase
    {
        private readonly StandardReportDbContext _context = context;

        
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
        public async Task<ActionResult<StandardReport>> AddStandardReport(StandardReport newStandardReport)
        {
            if (newStandardReport is null)
                return BadRequest();

            
            _context.StandardReports.Add(newStandardReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStandardReportById), new { id = newStandardReport.Id }, newStandardReport);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStandarReport(int id, StandardReport updatedReport)
        {
            var standardReport = await _context.StandardReports.FindAsync(id);
            if (standardReport is null)
                return NotFound();

            standardReport.Name = updatedReport.Name;
            standardReport.Area = updatedReport.Area;

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
