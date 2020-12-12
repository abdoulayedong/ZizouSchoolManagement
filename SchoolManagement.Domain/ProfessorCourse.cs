using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain
{
    public class ProfessorCourse
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
    }
}