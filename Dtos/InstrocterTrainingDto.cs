using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EducationApp.Dtos
{
    public class InstrocterTrainingDto
    {   


        private string _filePath;

        public int TrainingId { get; set; }
        public string Title { get; set; }

        public string FilePath { get => this._filePath; set => this._filePath = value.Replace("\\","/"); }

        public decimal Cost { get; set; }



    }
}
