using AutoMapper;
using BankApp.Entities;

namespace BankApp.Core.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
