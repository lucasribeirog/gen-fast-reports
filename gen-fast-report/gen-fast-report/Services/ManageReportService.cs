using gen_fast_report.Models.Controllers;
using gen_fast_report.Models.DTOs;
using gen_fast_report.Services.IServices;
using System.Linq;
using System.Reflection.Metadata;
using Xceed.Document.NET;
using Xceed.Words.NET;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace gen_fast_report.Services
{
    public class ManageReportService : IManageReportService
    {
        private readonly string _standardReportBalisticaPath = "C:\\Users\\RYZEN 7\\OneDrive\\Documentos\\DOCUMENTOS LUCAS\\gen-fast-report\\Origem\\Balistica\\1.docx";
        private readonly string _destinyPath = "C:\\Users\\RYZEN 7\\OneDrive\\Documentos\\DOCUMENTOS LUCAS\\gen-fast-report\\Destino";
        public async Task<string> WriteNewReport(ReportRequest reportRequest)
        {
            try
            {
                IFormFile file = reportRequest.File!;
                string destinyPathComplete = Path.Combine(_destinyPath, file!.FileName);

                // Save file in the destiny path
                await using (var fileStream = new FileStream(destinyPathComplete, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                BalisticaDTO balisticaReceive = GetDataFromInputReport(destinyPathComplete);
                DocX document = DocX.Load(_standardReportBalisticaPath);

                if (document is not null && balisticaReceive is not null)
                {
                    document = ReplaceValues(document, balisticaReceive);
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
        private DocX ReplaceValues(DocX document, BalisticaDTO balisticaReceive)
        {
            var stringReplaces = new Dictionary<string, string>
            {
                { "#NL", balisticaReceive.Laudo! },
                { "#NR", balisticaReceive.Requisicao! },
                { "#REDS", balisticaReceive.Reds! },
                { "#UR", balisticaReceive.UnidadeRequisitante! },
                { "#AR", balisticaReceive.AutoridadeRequisitante! },
                { "#PCNET", balisticaReceive.Pcnet! },
                { "#RP", balisticaReceive.ResponsavelPericia! },
                { "#FAV", balisticaReceive.Fav! },
                { "#EXE", balisticaReceive.DescricaoExame! },
                { "#DIE", balisticaReceive.DataInicio! },
                { "#HIE", balisticaReceive.HoraInicio! },
            };

            foreach (var replace in stringReplaces)
            {
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = replace.Key,
                    NewValue = replace.Value
                });
            }

            return document;
        }

        private BalisticaDTO GetDataFromInputReport(string path)
        {
            try
            {
                DocX document = DocX.Load(path);
                Console.WriteLine("Arquivo Lido com sucesso");
                BalisticaDTO data = new BalisticaDTO();
                data.IdPcnet = document.Bookmarks.First().Paragraph.Text;
                data.Laudo = GetParagraphTextContaining(document, "Nº Laudo:");
                data.Requisicao = GetParagraphTextContaining(document, "Nº Requisição Pericial:");
                data.Reds = GetParagraphTextContaining(document, "Nº REDS:");
                data.UnidadeRequisitante = GetParagraphTextContaining(document, "Unidade Requisitante:");
                data.AutoridadeRequisitante = GetParagraphTextContaining(document, "Autoridade Requisitante:");
                data.Pcnet = GetParagraphTextContaining(document, "Nº Procedimento Origem:");
                data.ResponsavelPericia = GetParagraphTextContaining(document, "Responsável pela Perícia:");
                data.Fav = GetParagraphTextContaining(document, "Nº da FAV:");
                data.DescricaoExame = GetParagraphTextContaining(document, "Exame em:");
                data.DataInicio = GetParagraphTextContaining(document, "Data do início do exame:");
                data.HoraInicio = GetParagraphTextContaining(document, "Hora do início do exame:");
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível inicializar o seu documento", ex.ToString());
                throw;
            }
        }


        private string GetParagraphTextContaining(DocX document, string searchText)
        {
            var paragraph = document.Paragraphs.FirstOrDefault(p => p.Text.Contains(searchText));
            if (paragraph is not null)
            {
                int startIndex = paragraph.Text.IndexOf(searchText) + searchText.Length;
                return paragraph.Text.Substring(startIndex).Trim();
            }
            return "";

        }
    }
    }
