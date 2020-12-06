using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        public readonly SchoolManagmentDBContext _context;

        public ClassRepository(SchoolManagmentDBContext context)
        {
            _context = context;
        }
        public async Task<Class> AddClass(Class @class)
        {
            await _context.Classes.AddAsync(@class);
            await _context.SaveChangesAsync();
            return @class;
        }

        public async Task<Class> GetClassById(int Id)
        {
            return await _context.Classes.FindAsync(Id);
        }

        public List<Class> GetClasses()
        {
            //return _context.Classes.ToList();
            return new List<Class>();
        }

        public async Task<Class> UpdateClass(Class @class)
        {
            var clas = await _context.Classes.FindAsync(@class.Id);
            clas.Name = @class.Name;
            clas.Code = @class.Code;
            clas.NumberOfCourses = @class.NumberOfCourses;
            clas.NumberOfProfessors = @class.NumberOfProfessors;
            clas.NumberOfStudents = @class.NumberOfStudents;
            await _context.SaveChangesAsync();
            return clas;
        }

        public async void DeleteClass(Class @class)
        {
            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
        }
    }
}
