using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain
{
    public class DepartmentProfessor
    {
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsHead { get; set; }
    }
}
