using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public int NumberOfStudents { get; set; }

        public int NumberOfCourses { get; set; }

        public int NumberOfProfessors { get; set; }

        //1- One to many relationship with Departement
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Student> Students { get; set; }

        // Many to many relationship with Courses
        public ICollection<ProfessorClass> ProfessorClasses { get; set; }
        public ICollection<CourseClass> CourseClasses { get; set; }

    }
}
