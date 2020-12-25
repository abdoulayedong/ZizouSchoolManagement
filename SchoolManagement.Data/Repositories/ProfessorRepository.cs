using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly SchoolManagmentDBContext _context;

        public ProfessorRepository(SchoolManagmentDBContext context)
        {
            _context = context;
        }

        public async Task<Professor> AddProfessor(Professor professor)
        {
            await _context.Professors.AddAsync(professor);
            await _context.SaveChangesAsync();
            return professor;
        }

        public Professor GetProfessorById(int Id)
        {
            return _context.Professors.Find(Id);
          
        }

        public List<Professor> GetProfessors()
        {
            return _context.Professors.ToList();
        }

        public List<ProfessorDepartment> GetProfessorDepartments()
        {
            return _context.ProfessorDepartments.ToList();
        }

        public async Task<Professor> UpdateProfessor(Professor professor)
        {
            var prof = await _context.Professors.FindAsync(professor.Id);
            prof.Cin = professor.Cin;
            prof.Diplome = professor.Diplome;
            prof.Email = professor.Email;
            prof.FirstName = professor.FirstName;
            prof.LastName = professor.LastName;
            prof.PasswordHash = professor.PasswordHash;
            prof.PasswordSalt = professor.PasswordSalt;
            prof.MainPhotoUrl = professor.MainPhotoUrl;
            await _context.SaveChangesAsync();
            return prof;
        }

        public async void DeleteProfessor(Professor professor)
        {
            _context.Professors.Remove(professor);
            await _context.SaveChangesAsync();
        }
    }
}
