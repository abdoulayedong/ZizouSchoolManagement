using SchoolManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        public List<Department> GetDepartments()
        {
            return _context.Departments.ToList();
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
    }
}
