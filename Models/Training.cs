using EducationApp.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApp.Models
{
    public class Training
    {   



        [Key]
        public int TrainingId { get; set; }

        [ForeignKey("Id")]
        public string InstrocterId { get; set; }
        public Instrocter Instrocter { get; set; }

        //Başlık
        public string Title { get; set; }

        //kategori
        public CategoryEnum.Category Category { get; set; }

        public string FilePath { get; set; }
        //Kontenjan
        public int Quota { get; set; }

        //Maliyet
        public decimal Cost { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        // Kullanıcı kimlik numarası, aynı zamanda dış anahtar olarak kullanılacak
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        //İçerikleri
        public virtual ICollection<Content> Contents { get; set; }



    }
}
