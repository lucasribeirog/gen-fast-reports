using gen_fast_report.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gen_fast_report.Enums;

namespace gen_fast_report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FastReportControllercs : ControllerBase
    {
        static private List<StandardReport> standardReports = new List<StandardReport>
        {
            new StandardReport
            {
                Id = 1,
                Name = "Padrao Balistica",
                Area = Area.Balistica
            },
            new StandardReport
            {
                Id = 2,
                Name = "Padrao Trânsito",
                Area = Area.Transito
            },
            new StandardReport
            {
                Id = 3,
                Name = "Padrao Vida",
                Area = Area.Vida
            },
            new StandardReport
            {
                Id = 4,
                Name = "Padrao Documentoscopia",
                Area = Area.Documentoscopia
            },
            new StandardReport
            {
                Id = 5,
                Name = "Padrao Meio Ambiente",
                Area = Area.MeioAmbiente
            }
        };

        [HttpGet]
        public ActionResult<List<StandardReport>> GetStandardReports()
        {
            return Ok(standardReports);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<StandardReport> GetStandardReportById(int id) 
        { 
            var standardReport = standardReports.FirstOrDefault(x => x.Id == id);
            if (standardReport is null)
                return NotFound();
            return Ok(standardReport);
        }

        [HttpPost]
        public ActionResult<StandardReport> AddStandardReport(StandardReport newStandardReport)
        {
            if (newStandardReport is null)
                return BadRequest();

            newStandardReport.Id = standardReports.Max(x => x.Id) + 1;
            standardReports.Add(newStandardReport);
            return CreatedAtAction(nameof(GetStandardReportById), new { id = newStandardReport.Id}, newStandardReport);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStandarReport(int id,  StandardReport updatedReport)
        {
            var standardReport = standardReports.FirstOrDefault(x => x.Id == id);
            if (standardReport is null)
                return NotFound();
            standardReport.Name = updatedReport.Name;
            standardReport.Area = updatedReport.Area;

            return NoContent();
        }
    }
}
