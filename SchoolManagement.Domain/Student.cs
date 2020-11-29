using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(128)]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Gender  Gender { get; set; }

        public string Address { get; set; }
        
        public DateTime BirthDate { get; set; }

        public DateTime InscriptionDate { get; set; }

        public string MainPhotoUrl { get; set; }

        public ICollection<StudentCourse> Courses { get; set; }


    }
}
