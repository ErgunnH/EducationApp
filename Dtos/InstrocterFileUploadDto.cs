using EducationApp.Enums;
using EducationApp.Models;

namespace EducationApp.Dtos
{
    public class InstrocterFileUploadDto
    {

        public string Title { get; set; }
        //kategori
        public CategoryEnum.Category Category { get ; set ; }

        public int Quota { get; set; }

        //Maliyet
        public decimal Cost { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

      


    }
}
