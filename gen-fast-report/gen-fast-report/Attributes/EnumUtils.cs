using System.Runtime.Serialization;

namespace gen_fast_report.Attributes
{
    public class EnumUtils
    {
        public static string GetEnumMemberValue(Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute));

            return attribute?.Value ?? enumValue.ToString(); // Retorna o valor do EnumMember, ou o nome padrão se não houver atributo
        }
    }
}
