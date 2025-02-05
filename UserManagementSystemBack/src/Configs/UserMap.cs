/*
*@author Ramadan Ismael
*/

using AutoMapper;
using UserManagementSystemBack.src.DTOs.User;
using UserManagementSystemBack.src.Models;

namespace UserManagementSystemBack.src.Configs
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserModel, RUserDto>();
            CreateMap<CUserDto, UserModel>();
            CreateMap<UUserDto, UserModel>()
            .ForMember(u => u.DateUpdate, opt => opt.MapFrom(src => src.DateUpdate));
        }
    }
}