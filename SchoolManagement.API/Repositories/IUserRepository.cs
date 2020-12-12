using SchoolManagement.Domain;
using System.Threading.Tasks;

namespace SchoolManagement.API.Repositories
{
    public interface IUserRepository
    {
        Task<Administrator> Login(string email, string password);
        Task<Administrator> Register(Administrator userToRegister, string password);
        Task<Administrator> GetUserById(int id);
        Task<bool> UserExists(string email);
    }
}