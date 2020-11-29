﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Domain
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public int HoursAmount { get; set; }
        public int TotalWeekAmount { get; set; }

        public ICollection<StudentCourse> Students { get; set; }

        public ICollection<ClassCourse> Classes { get; set; }
    }
}