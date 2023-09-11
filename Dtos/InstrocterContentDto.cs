using EducationApp.Models;

namespace EducationApp.Dtos
{
    public class InstrocterContentDto
    {

        public int ContentId { get; set; }
        public int TrainingId { get; set; } // Hangi eğitime ait olduğunu belirten dış anahtar    
        public string ContentName { get; set; }

        public string ContentType { get; set; }
        public string ContentPath { get; set; }




    }
}
