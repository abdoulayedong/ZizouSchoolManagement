using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain;

namespace SchoolManagement.Data
{
    public class SchoolManagmentDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                (@"Server = (localdb)\MSSQLLocalDB; Database = SchoolManagementDB; Trusted_Connection = True; ")
                );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ClassCourse> ClassCourses { get; set; }

        // public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>().ToTable("Absences");

            modelBuilder.Entity<ClassCourse>().HasKey(cc => new { cc.ClassId, cc.CourseId });
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
        }
    }
}
