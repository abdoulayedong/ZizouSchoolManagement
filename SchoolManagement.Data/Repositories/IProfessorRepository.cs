using SchoolManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositories
{
    public interface IProfessorRepository
    {
        Task<Professor> AddProfessor(Professor professor);
        void DeleteProfessor(Professor professor);
        Professor GetProfessorById(int Id);
        List<Professor> GetProfessors();
        List<ProfessorDepartment> GetProfessorDepartments();
        Task<Professor> UpdateProfessor(Professor professor);
    }
}