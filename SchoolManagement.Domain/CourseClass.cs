using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class CourseClass
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
