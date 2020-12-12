using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(5)]
        public string Code { get; set; }
        public ICollection<Class> Classes { get; set; }
        public ICollection<ProfessorDepartment> ProfessorDepartments { get; set; }

    }
}
