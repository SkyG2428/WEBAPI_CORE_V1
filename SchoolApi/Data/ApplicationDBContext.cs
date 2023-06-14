using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;

namespace SchoolApi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StudentCourse>().HasKey(x =>new { x.StudentId, x.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(x => x.Student)
                .WithMany(z => z.StudentCourses)
                .HasForeignKey(x => x.StudentId);


            modelBuilder.Entity<StudentCourse>()
                .HasOne(x => x.Course)
                .WithMany(z => z.StudentCourses)
                .HasForeignKey(x => x.CourseId);






            modelBuilder.Entity<Student>().
                HasMany(x=>x.StudentCourses).WithOne(z=>z.Student)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Course>().
                HasMany(x => x.StudentCourses).WithOne(z => z.Course)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
