using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain
{
    public class StudentClass
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }


        public DateTime AbsenceDate { get; set; }
    }
}
