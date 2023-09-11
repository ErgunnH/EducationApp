using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApp.Models
{   


    //Kyıt istek Tablosu
    public class EnrollmentRequest
    {
        [Key]
        public int RequestId { get; set; }

        public int TrainingId { get; set; }

        public virtual Training Training { get; set; }

        [ForeignKey("Id")]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public bool RequestStatus { get; set; }


    }
}
