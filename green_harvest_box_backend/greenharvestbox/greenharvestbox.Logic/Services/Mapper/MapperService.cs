using AutoMapper;
using greenharvestbox.Data.Models;
using greenharvestbox.Logic.Models.dto.Login.dto;

namespace greenharvestbox.Logic.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserBasicDto>();
            
        }
    }

}
