using AuthService.API.DataAccessLayer.Entites;
using AuthService.API.DTO_s.Register;
using AutoMapper;

namespace AuthService.API.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Register, AppUser>();
        }
    }
}
