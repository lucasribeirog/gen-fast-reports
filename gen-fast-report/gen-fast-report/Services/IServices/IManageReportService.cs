using gen_fast_report.Models.Controllers;
using gen_fast_report.Models;

namespace gen_fast_report.Services.IServices
{
    public interface IManageReportService
    {
        void WriteNewReport(string sourcePath, string destinyPath);
    }
}