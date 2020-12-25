using SchoolManagement.Data.DTOs;
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
        //List<DepartmentDTO> GetDepartments();
        List<ProfessorDepartment> GetProfessorDepartments();
        List<DepartmentProfessor> GetDepartments();
        Task<Department> UpdateDepartment(Department department);
        Task<ProfessorDepartment> ProfessorDepartment(ProfessorDepartment professorDepartment);
        Task<ProfessorDepartment> UpdateProfessorDepartment(ProfessorDepartment professorDepartment);
        void DeleteProfessorDepartment(ProfessorDepartment professorDepartment);
        List<Professor> GetProfessors(DepartmentProfessor department);
    }
}