using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public readonly SchoolManagmentDBContext _context;

        public StudentRepository(SchoolManagmentDBContext context)
        {
            _context = context;
        }
        public async Task<Student> AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> GetStudentById(int Id)
        {
            return await _context.Students.FindAsync(Id);
        }

        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            var studen = await _context.Students.FindAsync(student.Id);
            studen.FirstName = studen.FirstName;
            studen.LastName = student.LastName;
            studen.PhoneNumber = student.PhoneNumber;
            studen.InscriptionDate = student.InscriptionDate;
            studen.Email = student.Email;
            studen.Address = student.Address;
            studen.BirthDate = student.BirthDate;
            studen.MainPhotoUrl = student.MainPhotoUrl;
            await _context.SaveChangesAsync();
            return studen;
        }

        public async void DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
