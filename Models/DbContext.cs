using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().ToTable("Students");

            modelBuilder.Entity<Instrocter>().ToTable("Instrocters");


            modelBuilder.Entity<Training>()
               .HasOne(t => t.Instrocter)
               .WithMany(i => i.Trainings)
               .HasForeignKey(t => t.InstrocterId);

            modelBuilder.Entity<Enrollment>()
               .HasOne(t => t.Student)
               .WithMany(i => i.Enrollments)
               .HasForeignKey(t => t.StudentId);


        }
        //Eğitimler
        public DbSet<Training> Trainings { get; set; }

        //public DbSet<User> Users { get; set; }
        public DbSet<Instrocter> Instrocters { get; set; }

        public DbSet<Student> Students { get; set; }

        //İçerikler
        public DbSet<Content> Contents { get; set; }  

        ////Ders Kayıtları
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
