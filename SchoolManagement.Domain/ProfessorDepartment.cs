using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class ProfessorDepartment
    {
        [Key]
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public bool IsHead { get; set; }
    }
}
