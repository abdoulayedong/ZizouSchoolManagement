using SchoolManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SchoolManagement.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public readonly SchoolManagmentDBContext _context;

        public CourseRepository(SchoolManagmentDBContext context)
        {
            _context = context;
        }
        public async Task<Course> AddCourse(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> GetCourseById(int Id)
        {
            return await _context.Courses.FindAsync(Id);
        }

        public List<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }

        public async Task<Course> UpdateCourse(Course course)
        {
            var cours = await _context.Courses.FindAsync(course.Id);
            cours.Name = course.Name;
            cours.Code = course.Code;
            cours.HoursAmount = course.HoursAmount;
            cours.TotalWeekAmount = course.TotalWeekAmount;
            await _context.SaveChangesAsync();
            return cours;
        }

        public async void DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
