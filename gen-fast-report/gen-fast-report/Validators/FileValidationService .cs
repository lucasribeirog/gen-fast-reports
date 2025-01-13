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
    }
}
