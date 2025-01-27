namespace gen_fast_report.Validators
{
    public class FileValidationService : IFileValidationService
    {
        public bool IsValidDocx(IFormFile file)
        {
            if (file.ContentType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                return false;
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return fileExtension == ".docx";
        }

        public bool IsValidImage(IFormFile file)
        {
            // Verifica se o ContentType é um dos tipos de imagem suportados
            var validContentTypes = new List<string>
            {
                "image/jpeg",
                "image/png",
            };

            if (!validContentTypes.Contains(file.ContentType))
            {
                return false;
            }

            // Verifica a extensão do arquivo
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var validExtensions = new List<string> { ".jpg", ".jpeg", ".png"};

            return validExtensions.Contains(fileExtension);
        }


    }
}
