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
        public Gender Gender { get; set; }
        public string Address { get; set; }        
        public DateTime BirthDate { get; set; }
        public DateTime InscriptionDate { get; set; }

        //1- One to many relationship with Class
        public Class Class { get; set; }
        //2- Many to many relationship with Courses
        public ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
