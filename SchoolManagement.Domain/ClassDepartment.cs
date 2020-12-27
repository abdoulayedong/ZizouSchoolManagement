using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class ClassDepartment
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
