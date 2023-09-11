using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApp.Models
{
    public class Instrocter: IdentityUser
    {

      

        public virtual ICollection<Training> Trainings { get; set; }




    }
}
