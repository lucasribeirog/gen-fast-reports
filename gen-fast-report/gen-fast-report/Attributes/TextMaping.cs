namespace gen_fast_report.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TextMappingAttribute : Attribute
    {
        public string SearchText { get; }

        // Construtor que recebe o texto de busca
        public TextMappingAttribute(string searchText)
        {
            SearchText = searchText;
        }
    }
}
