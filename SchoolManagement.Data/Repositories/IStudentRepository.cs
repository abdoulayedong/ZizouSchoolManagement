using SchoolManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> AddStudent(Student student);
        void DeleteStudent(Student student);
        Task<Student> GetStudentById(int Id);
        List<Student> GetStudents();
        Task<Student> UpdateStudent(Student student);
    }
}