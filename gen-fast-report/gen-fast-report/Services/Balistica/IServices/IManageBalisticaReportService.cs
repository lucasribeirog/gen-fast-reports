using gen_fast_report.Models.Controllers;

namespace gen_fast_report.Services.IServices
{
    public interface IManageBalisticaReportService
    {
        Task<string> WriteNewReport(BalisticaRequest balisticaRequest);
    }
}
