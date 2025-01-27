using gen_fast_report.Attributes;
using gen_fast_report.Enums;
using gen_fast_report.Services.IServices;
using System.Reflection;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace gen_fast_report.Services
{
    public class ManageReportService : IManageReportService
    {    
        //public DocX ReplaceMainValues(DocX document, ReportRequest reportRequest)
        //{
        //    // Verificar se a área e tipo de balística são compatíveis
        //    if (reportRequest.Area == Enums.Area.Balistica && reportRequest.TipoBalistica == Enums.Balistica.TipoBalistica.ArmaFogo)
        //    {
        //        // Determinar os valores de gênero para as substituições
        //        var genderValues = GetGenderValues(reportRequest.Gender);

        //        // Realizar as substituições no documento
        //        document.ReplaceText(new StringReplaceTextOptions
        //        {
        //            SearchValue = "#historico1",
        //            NewValue = genderValues.Item1
        //        });

        //        document.ReplaceText(new StringReplaceTextOptions
        //        {
        //            SearchValue = "#historico2",
        //            NewValue = genderValues.Item2
        //        });
        //    }

        //    // Retornar o documento modificado
        //    return document;
        //}

        public Tuple<string, string> GetGenderValues(Gender gender)
        {
            // Retornar os valores corretos de acordo com o gênero
            if (gender == Gender.Male)
            {
                return new Tuple<string, string>("o", "signatário");
            }
            else
            {
                return new Tuple<string, string>("a", "signatária");
            }
        }
        public DocX ReplaceValuesFromHeader<T>(DocX document, T data)
        {
            var properties = typeof(T).GetProperties();
            var stringReplaces = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                var attributeName = $"#{property.Name.ToUpper()}";
                var value = property.GetValue(data)?.ToString() ?? "";
                stringReplaces[attributeName] = value;
            }

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

        public T GetDataHeaderFromInputReport<T>(string path) where T : new()
        {
            try
            {
                DocX document = DocX.Load(path);
                Console.WriteLine("Arquivo Lido com sucesso");

                T data = new T();

                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    var textMappingAttribute = property.GetCustomAttribute<TextMappingAttribute>();
                    if (textMappingAttribute != null && textMappingAttribute.SearchText != "Id Laudo")
                    {
                        string searchText = textMappingAttribute.SearchText;

                        string value = GetParagraphTextContaining(document, searchText);

                        property.SetValue(data, value);

                        Console.WriteLine($"Propriedade {property.Name}: {value}");
                    }
                    else if (textMappingAttribute != null && textMappingAttribute.SearchText == "Id Laudo")
                    {
                        if (document.Bookmarks is not null)
                        {
                            string header = document.Bookmarks.First().Paragraph.Text;
                            property.SetValue(data, header);
                            Console.WriteLine($"Propriedade {property.Name}: {header}");
                        }

                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao extrair dados do documento", ex.ToString());
                throw;
            }
        }


        public string GetParagraphTextContaining(DocX document, string searchText)
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
