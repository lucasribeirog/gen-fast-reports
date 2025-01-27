namespace gen_fast_report.Validators
{
    public interface IFileValidationService
    {
        bool IsValidDocx(IFormFile file);
        bool IsValidImage(IFormFile file);
    }
}
