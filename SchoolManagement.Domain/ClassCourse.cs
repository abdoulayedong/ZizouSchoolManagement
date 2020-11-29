using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain
{
    public class ClassCourse
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
