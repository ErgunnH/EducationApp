using EducationApp.Enums;
using System;

namespace EducationApp.Dtos
{
    public class StudentTrainingDto
    {


        private string _filePath;

        private decimal _sumCost;

        public int TrainingId { get; set; }
        public string Title { get; set; }

        public string FilePath { get => this._filePath; set => this._filePath = value.Replace("\\", "/"); }

        //public CategoryEnum Category { get; set; }




    }
}
