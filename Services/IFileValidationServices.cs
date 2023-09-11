using Microsoft.AspNetCore.Identity;

namespace EducationApp.Services
{
    public interface IFileValidationServices
    {   

        public ValidateResult ValidateResult(IFormFile file, string userId, string trainingName, params string[] contentType);
        public ValidateResult ValidateResult(IFormFile file, string userId, string trainingName, string contentName, params string[] contentType);
    }

    public class ValidateResult
    {
        public bool IsValid { get; set; }


        public List<string> Errors { get; set; }
    }
}
 