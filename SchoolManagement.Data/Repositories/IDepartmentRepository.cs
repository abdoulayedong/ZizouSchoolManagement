using SchoolManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> AddDepartment(Department department);
        void DeleteDepartment(Department department);
        Task<Department> GetDepartmentById(int Id);
        List<Department> GetDepartments();
        Task<Department> UpdateDepartment(Department department);
        Task<ProfessorDepartment> ProfessorDepartment(ProfessorDepartment professorDepartment);
    }
}