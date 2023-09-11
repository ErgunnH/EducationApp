using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApp.Models
{   


    //Kayıt
    public class Enrollment
    {

        [Key]
        public int EnrollmentId { get; set; }

        [ForeignKey("Id")]
        public string StudentId { get; set; }
        public Student Student { get; set; }

        public int TrainingId { get; set; } // Hangi eğitime ait olduğunu belirten dış anahtar       

        public virtual Training Training { get; set; }

        public DateTime StartDate { get; set; }
    
        public bool IsCompleted { get; set; }

        public bool IsCancelled { get; set; }






    }
}
