using EducationApp.Models;

namespace EducationApp.Dtos
{
    public class StudentTrainingPurchaseDetail
    {

        private string _filePath;

        private decimal _sumCost;

        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<Content> Contents { get; set; }

        public int TrainingId { get; set; }
        public string Title { get; set; }

        public string FilePath { get => this._filePath; set => this._filePath = value.Replace("\\", "/"); }

        public decimal Cost { get; set; }

        public decimal SumCost
        {
            get => this._sumCost; set
            {
                TimeSpan difference = this.EndDate - this.StartDate;

                int totalDays = (int)difference.TotalDays;

                this._sumCost = totalDays * this.Cost;

            }
        }

        public int Quota { get; set; }       

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
