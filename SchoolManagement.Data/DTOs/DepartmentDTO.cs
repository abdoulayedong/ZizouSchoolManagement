using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Data.DTOs
{
    public class DepartmentDTO
    {
        public int DepartmentId { get; set; }
        public int ProfessorId { get; set; }
        public string Name { get; set; }       
        public string Code { get; set; }
        public string HeadDeparment { get; set; }
    }
}
