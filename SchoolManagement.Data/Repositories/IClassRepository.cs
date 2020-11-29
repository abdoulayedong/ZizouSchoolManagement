using SchoolManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositories
{
    public interface IClassRepository
    {
        Task<Class> AddClass(Class @class);
        void DeleteClass(Class @class);
        Task<Class> GetClassById(int Id);
        List<Class> GetClasses();
        Task<Class> UpdateClass(Class @class);
    }
}