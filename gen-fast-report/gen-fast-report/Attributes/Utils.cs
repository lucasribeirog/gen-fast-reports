using System.Runtime.Serialization;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace gen_fast_report.Attributes
{
    public class Utils
    {
        public static string GetEnumMemberValue(Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute));

            return attribute?.Value ?? enumValue.ToString(); // Retorna o valor do EnumMember, ou o nome padrão se não houver atributo
        }

        public static void ReplaceText(DocX document, string placeholder, string newValue)
        {
            document.ReplaceText(new StringReplaceTextOptions
            {
                SearchValue = placeholder,
                NewValue = newValue
            });
        }

        public static void RemoveText(DocX document, string marker)
        {
            // Identifica o texto começando com o marcador e o substitui por uma string vazia
            document.ReplaceText(new StringReplaceTextOptions
            {
                SearchValue = marker,
                NewValue = String.Empty
            });
        }

        public static void RemoveParagraphWithInitiateText(DocX document, List<string> markers)
        {
            foreach (var marker in markers)
            {
                var paragraphs = document.Paragraphs
                    .Where(p => p.Text.TrimStart().StartsWith(marker))
                    .ToList();

                foreach (var paragraph in paragraphs)
                {
                    document.RemoveParagraph(paragraph);
                }
            }
        }


    }
}
