using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain;

namespace SchoolManagement.Data
{
    public class SchoolManagmentDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            #region Boshies Connection String

                optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = SchoolManagementDB ; Trusted_Connection = True; ");

            #endregion
        }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<ProfessorDepartment> ProfessorDepartments { get; set; }
        public DbSet<ProfessorCourse> ProfessorCourses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<CourseClass> CourseClasses { get; set; }
        public DbSet<ProfessorClass> ProfessorClasses { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        // public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Professor>().ToTable("Professors");

            modelBuilder.Entity<ProfessorDepartment>()
                .HasKey(pd => pd.Id);
            modelBuilder.Entity<ProfessorDepartment>()
                .HasOne(pd => pd.Professor)
                .WithMany(p => p.ProfessorDepartments)
                .HasForeignKey(pd => pd.ProfessorId);
            modelBuilder.Entity<ProfessorDepartment>()
                .HasOne(pd => pd.Department)
                .WithMany(d => d.ProfessorDepartments)
                .HasForeignKey(pd => pd.DepartmentId);

            modelBuilder.Entity<ProfessorCourse>()
                .HasKey(pco => pco.Id);
            modelBuilder.Entity<ProfessorCourse>()
                .HasOne(pco => pco.Course)
                .WithMany(c => c.ProfessorCourses)
                .HasForeignKey(pco => pco.CourseId);
            modelBuilder.Entity<ProfessorCourse>()
                .HasOne(pco => pco.Professor)
                .WithMany(p => p.ProfessorCourses)
                .HasForeignKey(pco => pco.ProfessorId);

            modelBuilder.Entity<ProfessorClass>()
                .HasKey(pca => pca.Id);
            modelBuilder.Entity<ProfessorClass>()
                .HasOne(pca => pca.Class)
                .WithMany(c => c.ProfessorClasses)
                .HasForeignKey(pca => pca.ClassId);
            modelBuilder.Entity<ProfessorClass>()
                .HasOne(pca => pca.Professor)
                .WithMany(p => p.ProfessorClasses)
                .HasForeignKey(pca => pca.ProfessorId);

            modelBuilder.Entity<CourseClass>()
                .HasKey(cc => cc.Id);
            modelBuilder.Entity<CourseClass>()
                .HasOne(cc => cc.Class)
                .WithMany(c => c.CourseClasses)
                .HasForeignKey(cc => cc.ClassId);
            modelBuilder.Entity<CourseClass>()
                .HasOne(cc => cc.Course)
                .WithMany(c => c.CourseClasses)
                .HasForeignKey(cc => cc.CourseId);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => sc.Id);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Classes)
                .WithOne(c => c.Department);

            modelBuilder.Entity<Class>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Class);
            //modelBuilder.Entity<Department>()
            //    .HasMany(d => d.Professors).WithOne().HasForeignKey(cle => cle.DepartmentId);

            //modelBuilder.Entity<StudentCourse>().ToTable("Absences");
            //modelBuilder.Entity<CourseClass>().HasKey(cc => new { cc.ClassId, cc.CourseId });
        }
    }
}
