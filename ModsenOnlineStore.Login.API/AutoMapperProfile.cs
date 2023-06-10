using AutoMapper;
using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;
using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
