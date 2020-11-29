using SchoolManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositories
{
    public interface ICourseRepository
    {
        Task<Course> AddCourse(Course course);
        void DeleteCourse(Course course);
        Task<Course> GetCourseById(int Id);
        List<Course> GetCourses();
        Task<Course> UpdateCourse(Course course);
    }
}