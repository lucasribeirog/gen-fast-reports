using gen_fast_report.Models;
using gen_fast_report.Models.Controllers;

namespace gen_fast_report.Services
{
    public interface IUploadReportHandler
    {
        Task<byte[]> GetByteArray(IFormFile file);
        Task<StandardReport> InsertByteArray(StandardReportRequest newStandardReportRequest);
    }
    public class UploadReportHandler : IUploadReportHandler
    {
        public async Task<byte[]> GetByteArray(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public  async Task<StandardReport> InsertByteArray(StandardReportRequest newStandardReportRequest)
        {
            byte[] bytes = await GetByteArray(newStandardReportRequest.File!);

            StandardReport newStandardReport = new StandardReport();
            newStandardReport.File = bytes;
            newStandardReport.Name = newStandardReportRequest.Name;
            newStandardReport.Area = newStandardReportRequest.Area;
            return newStandardReport;
        }

    }
}
