using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class Professor : User
    {
        [Column (TypeName = "DateTime2")]
        [Required]
        public DateTime HiringDate { get; set; }
        [MaxLength(8)]
        [Required]
        public string Cin { get; set; }
        [Required]
        public UniversityDiploma Diplome { get; set; }
        public ICollection<ProfessorCourse> ProfessorCourses { get; set; }
        public ICollection<ProfessorDepartment> ProfessorDepartments { get; set; }
        public ICollection<ProfessorClass> ProfessorClasses { get; set; }
    }

}
