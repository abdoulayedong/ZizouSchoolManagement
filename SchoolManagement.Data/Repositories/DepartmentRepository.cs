using SchoolManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SchoolManagement.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SchoolManagmentDBContext _context;
        private readonly IProfessorRepository _professorRepository;

        public DepartmentRepository(SchoolManagmentDBContext context, IProfessorRepository professorRepository)
        {
            _context = context;
            _professorRepository = professorRepository;
        }

        public async Task<Department> AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> GetDepartmentById(int Id)
        {
            return await _context.Departments.FindAsync(Id);
        }

        public List<DepartmentProfessor> GetDepartments()
        {
            var DepartmentProfessors = _context.DepartmentProfessors.ToList();           
            return DepartmentProfessors;
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            var dep = await _context.Departments.FindAsync(department.Id);
            dep.Name = department.Name;
            dep.Code = department.Code;
            await _context.SaveChangesAsync();
            return dep;
        }

        public async void DeleteDepartment(Department department)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task<ProfessorDepartment> ProfessorDepartment(ProfessorDepartment professorDepartment)
        {
            await _context.ProfessorDepartments.AddAsync(professorDepartment);
            await _context.SaveChangesAsync();
            return professorDepartment;
        }

        public async Task<ProfessorDepartment> UpdateProfessorDepartment(ProfessorDepartment professorDepartment)
        {
            var professor = _context.ProfessorDepartments.Where(pro => pro.Id == professorDepartment.Id).FirstOrDefault();
            professor.ProfessorId = professorDepartment.ProfessorId;
            await _context.SaveChangesAsync();            
            return professor;
        }

        public async void DeleteProfessorDepartment(ProfessorDepartment professorDepartment)
        {
            _context.ProfessorDepartments.Remove(professorDepartment);
            await _context.SaveChangesAsync();
        }
    }
}
