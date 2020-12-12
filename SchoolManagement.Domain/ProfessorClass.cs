using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class ProfessorClass
    {
        [Key]
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public bool IsPrincipal { get; set; }
    }
}
