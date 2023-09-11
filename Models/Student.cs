using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApp.Models
{
    public class Student:IdentityUser
    {
        
    
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<EnrollmentRequest> EnrollmentRequests { get; set; }

    }
}
