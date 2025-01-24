using gen_fast_report.Models.Controllers;
using gen_fast_report.Models.DTOs;
using gen_fast_report.Services.IServices;
using Xceed.Words.NET;

namespace gen_fast_report.Services.Balistica
{
    public class ManageBalisticaReportService(IManageReportService manageReportService) : IManageBalisticaReportService
    {
        private readonly string _standardReportBalisticaPath = "D:\\PRISCILLA\\DOCUMENTO\\documentos\\DOCUMENTOS LUCAS\\gen-fast-report\\Origem\\Balistica\\1.docx";
        private readonly string _destinyPath = "D:\\PRISCILLA\\DOCUMENTO\\documentos\\DOCUMENTOS LUCAS\\gen-fast-report\\Destino";

        private readonly IManageReportService _manageReportService = manageReportService;
        public async Task<string> WriteNewReport(BalisticaRequest balisticaRequest)
        {
            try
            {
                IFormFile file = balisticaRequest.File!;
                string destinyPathComplete = Path.Combine(_destinyPath, file!.FileName);
                await using (var fileStream = new FileStream(destinyPathComplete, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                //Get header values from input document
                BalisticaDTO balisticaReceive = _manageReportService.GetDataHeaderFromInputReport<BalisticaDTO>(destinyPathComplete);

                DocX document = DocX.Load(_standardReportBalisticaPath);

                if (document is not null && balisticaReceive is not null)
                {
                    //Replace Header
                    document = _manageReportService.ReplaceValuesFromHeader(document, balisticaReceive);
                    //Replace Main
                    //document = ReplaceMainValues(document, reportRequest);
                    //Save
                    document.SaveAs(destinyPathComplete);
                }
                return "Documento feito com sucesso";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao construir documento", ex.ToString());
                throw;
            }
        }
    }
}
