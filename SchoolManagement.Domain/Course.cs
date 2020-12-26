using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string NameCourse { get; set; }
        [Required]
        [StringLength(50)]
        public string CodeCourse { get; set; }
        public int HoursAmountCourse { get; set; }
        public int TotalWeekAmountCourse { get; set; }
        public int CoefficientCourse { get; set; }
        public ICollection<ProfessorCourse> ProfessorCourses { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<CourseClass> CourseClasses { get; set; }
    }
}
