using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain;

namespace SchoolManagement.Data
{
    public class SchoolManagmentDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region ZizouPop Conncetion String
            optionsBuilder.UseSqlServer(
                ("Server = (localdb)\\MSSQLLocalDB; Database = SchoolManagementDB; Trusted_Connection = True; ")
                );

            #endregion

            #region Boshies Connection String

           // optionsBuilder.UseSqlServer(( "Server = localhost\\SQLEXPRESS; Database = SchoolManagementDB ; Trusted_Connection = True; "));

            #endregion
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<HeadDepartment> HeadDepartments { get; set; }
        public DbSet<ClassCourse> ClassCourses { get; set; }
        public DbSet<Professor_Class> ProfessorClasses { get; set; }

        // public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentCourse>().ToTable("Absences");
            modelBuilder.Entity<ClassCourse>().HasKey(cc => new { cc.ClassId, cc.CourseId });
        }
    }
}
