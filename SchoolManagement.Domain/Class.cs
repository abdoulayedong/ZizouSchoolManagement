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

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<ClassSubject> Subjects { get; set; }

    }
}
