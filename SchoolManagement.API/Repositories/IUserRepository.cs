using SchoolManagement.Domain;
using System.Threading.Tasks;

namespace SchoolManagement.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> Login(string email, string password);
        Task<User> Register(User userToRegister, string password);
        Task<User> GetUserById(int id);
        Task<bool> UserExists(string email);
    }
}