using AutoMapper;
using userManagementSystemBack.src.Dtos.UserDto;
using userManagementSystemBack.src.Models;
/**
** @author Ramadan Ismael
*/
namespace userManagementSystemBack.src.Helpers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserModel, UserGetAllDto>();
            CreateMap<CreateUserDto, UserModel>();
            CreateMap<UpdateUserDto, UserModel>();
        }
    }
}