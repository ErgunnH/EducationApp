using System.ComponentModel.DataAnnotations;

namespace EducationApp.Models
{
    public class Content
    {
        [Key]
        public int ContentId { get; set; }
        public int TrainingId { get; set; } // Hangi eğitime ait olduğunu belirten dış anahtar       

        public Training Training { get; set; }
        public string ContentName { get; set; }
        public string ContentType { get; set; }
        public string ContentPath { get; set; }

    }
}
