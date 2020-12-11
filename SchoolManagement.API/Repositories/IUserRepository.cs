using SchoolManagement.Domain;
using System.Threading.Tasks;

namespace SchoolManagement.API.Repositories
{
    public interface IUserRepository
    {
        Task<Student> Login(string email, string password);
        Task<Student> Register(Student userToRegister, string password);
        Task<Student> GetUserById(int id);
        Task<bool> UserExists(string email);
    }
}