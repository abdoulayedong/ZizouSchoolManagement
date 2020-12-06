using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class Student : User
    {
        public UniversityDiploma StudiesGrade { get; set; }
        public string PhoneNumber { get; set; }

        public Gender  Gender { get; set; }

        public string Address { get; set; }
        
        public DateTime BirthDate { get; set; }

        public DateTime InscriptionDate { get; set; }

        public ICollection<StudentCourse> Courses { get; set; }


    }
}
