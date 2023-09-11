using EducationApp.Models;
using Microsoft.AspNetCore.Identity;

namespace EducationApp.Services
{
    public class FileManagement
    {




        public static async Task<string> FileUploadAsync (IFormFile file, IdentityUser user,string fileName)
        {
            
            if (file != null && file.Length > 0)
            {

                var fileName2 = Path.GetFileName(file.FileName);

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", user.Id );
                
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", user.Id, fileName);

                if (!File.Exists(filePath))
                {
                    // Create a file to write to.   
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    return String.Empty;

                }

                return filePath;  

            }
            else
            {
                return String.Empty;
            }

        }

    }
}
