using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain
{
    public class StudentSubject
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime AbsenceDate { get; set; }
    }
}
