using Microsoft.AspNetCore.Identity;

namespace EducationApp.Services
{
    public class FileUpload : IFileUploadServices
    {   


        //fonksiyonlar birleştir
        public string Save(IFormFile file, string userId, string trainingName)
        {

            try
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

                var fileName = Path.GetFileName(file.FileName).Replace(fileNameWithoutExtension, trainingName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", userId, trainingName, fileName);

                // Create a file to write to.   
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return Path.Combine(userId, trainingName, fileName);

            }

            catch
            {

                return String.Empty;

            }

        }

        public string Save(IFormFile file, string userId, string trainingName,string contenName)
        {

            try
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

                var fileName = Path.GetFileName(file.FileName).Replace(fileNameWithoutExtension, contenName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", userId, trainingName, fileName);

                // Create a file to write to.   
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return Path.Combine(userId, trainingName, fileName);

            }

            catch
            {

                return String.Empty;

            }

        }




    }
}
