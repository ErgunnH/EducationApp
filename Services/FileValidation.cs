using Microsoft.AspNetCore.Identity;

namespace EducationApp.Services
{
    public class FileValidation : IFileValidationServices
    {


        //bu iki fonksiyon birleştirilmeli

        public ValidateResult ValidateResult(IFormFile file, string userId, string trainingName, params string[] contentType)
        {

            bool isValid = true;

            List<string> errors = new();

            var fileName = Path.GetFileName(file.FileName);

            var userfolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", userId);

            if (!Directory.Exists(userfolderPath))
            {
                Directory.CreateDirectory(userfolderPath);
            }

            var trainingfolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", userId, trainingName);

            if (Directory.Exists(trainingfolderPath))
            {
                isValid = false;

                errors.Add("Aynı isimde başka bir eğitimin var, Lütfen başka bir isim belirleyin");

            }
            else
            {
                Directory.CreateDirectory(trainingfolderPath);
            }

            var filePath = Path.Combine(trainingfolderPath, fileName);

            if (File.Exists(filePath))
            {
                isValid = false;

                errors.Add("Aynı isimde başka bir içerik dosyası var, Lütfen dosya ismini değiştirin");
            }

            string fileExtension = Path.GetExtension(fileName).Replace(".","");

            if (!contentType.Contains(fileExtension))
            {
                isValid = false;

                errors.Add("Lütfen belirtilen dosyalar haricinde dosya yüklemsi yapmayınız (" + String.Join(",", contentType) + ")");
            }

            return new ValidateResult
            {
                IsValid = isValid,

                Errors = errors

            };



        }

        public ValidateResult ValidateResult(IFormFile file, string userId, string trainingName, string contenName, params string[] contentType)
        {

            bool isValid = true;

            List<string> errors = new();

            var extension = Path.GetExtension(file.FileName);

            var fileName = contenName+ extension;

            var userfolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", userId);

            if (!Directory.Exists(userfolderPath))
            {
                Directory.CreateDirectory(userfolderPath);
            }

            var trainingfolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", userId, trainingName);

            if (!Directory.Exists(trainingfolderPath))
            {
                Directory.CreateDirectory(trainingfolderPath);

            }           

            var filePath = Path.Combine(trainingfolderPath, fileName);

            if (File.Exists(filePath))
            {
                isValid = false;

                errors.Add("Aynı isimde başka bir içerik dosyası var, Lütfen dosya ismini değiştirin");
            }

            

            if (!contentType.Contains(extension.Replace(".", "")))
            {
                isValid = false;

                errors.Add("Lütfen belirtilen dosyalar haricinde dosya yüklemsi yapmayınız (" + String.Join(",", contentType) + ")");
            }

            return new ValidateResult
            {
                IsValid = isValid,

                Errors = errors

            };



        }
    }
}
