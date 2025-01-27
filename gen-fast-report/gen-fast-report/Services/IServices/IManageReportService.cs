using gen_fast_report.Models.Controllers;
using gen_fast_report.Models;
using Xceed.Words.NET;
using gen_fast_report.Enums;

namespace gen_fast_report.Services.IServices
{
    public interface IManageReportService
    {
        Tuple<string, string> GetGenderValues(Gender gender);
        DocX ReplaceValuesFromHeader<T>(DocX document, T data);
        T GetDataHeaderFromInputReport<T>(string path) where T : new();
        string GetParagraphTextContaining(DocX document, string searchText);
    }
}