using SchoolManagement.UI.Library.Models;
using System.Threading.Tasks;

namespace SchoolManagement.UI.Library.API
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string email, string password);
        Task GetLoggedInUserInfo(string token);
    }
}