using AutoMapper;
using SchoolManagement.Data.DTOs;
using SchoolManagement.Domain;

namespace SchoolManagement.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles ()
        {
            CreateMap<User, UserDTO>();

        }
    }
}
