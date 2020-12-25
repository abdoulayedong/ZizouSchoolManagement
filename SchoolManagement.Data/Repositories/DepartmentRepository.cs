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

        public List<ProfessorDepartment> GetProfessorDepartments()
        {
            return _context.ProfessorDepartments.ToList();
        }

        public List<Professor> GetProfessors(DepartmentProfessor department)
        {
            var Professors = from depProf in _context.ProfessorDepartments
                             where depProf.DepartmentId == department.DepartmentId 
                             from prof in _context.Professors
                             where prof.Id == depProf.ProfessorId
                             select new Professor
                             {
                                 Id = prof.Id,
                                 FirstName = prof.FirstName,
                                 LastName = prof.LastName,
                                 Cin = prof.Cin,
                                 Diplome = prof.Diplome,
                                 Email = prof.Email,
                                 HiringDate = prof.HiringDate
                             };
            return Professors.ToList();
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
            if(professor != null)
            {
                professor.ProfessorId = professorDepartment.ProfessorId;
                professor.DepartmentId = professorDepartment.DepartmentId;
                professor.IsHead = professorDepartment.IsHead;
                await _context.SaveChangesAsync();
                return professor;
            }
            else
            {
                await ProfessorDepartment(professorDepartment);
                return professorDepartment;
            }
        }

        public async void DeleteProfessorDepartment(ProfessorDepartment professorDepartment)
        {
            _context.ProfessorDepartments.Remove(professorDepartment);
            await _context.SaveChangesAsync();
        }
    }
}
