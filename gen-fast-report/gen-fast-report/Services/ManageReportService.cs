using gen_fast_report.Models;
using gen_fast_report.Services.IServices;
using System.Linq;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace gen_fast_report.Services
{
    public class ManageReportService : IManageReportService
    {
        public void WriteNewReport(string sourcePath, string destinyPath)
        {
            Balistica balisticaReceive = GetDataFromInputReport(sourcePath);
            DocX document = DocX.Load(destinyPath + "/balistica.docx");
            Console.WriteLine("Arquivo Lido com sucesso");
            if (document is not null && balisticaReceive is not null) 
            {
                document.ReplaceText(new StringReplaceTextOptions()
                {
                    SearchValue = "Nº Laudo:",
                    NewValue = balisticaReceive.Laudo
                });
                document.SaveAs(destinyPath + "/documento_gerado.docx");
            }

        }
        private Balistica GetDataFromInputReport(string path)
        {
            try
            {
                DocX document = DocX.Load(path);
                Console.WriteLine("Arquivo Lido com sucesso");
                Balistica data = new Balistica();
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
            return document.Paragraphs
                           .FirstOrDefault(p => p.Text.Contains(searchText))?.Text ?? "";

        }
    }
    }
