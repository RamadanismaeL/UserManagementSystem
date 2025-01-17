

using userManagementSystemBack.src.Dtos.UserDto;
using userManagementSystemBack.src.Models;


/**
** @author Ramadan Ismael
*/
namespace userManagementSystemBack.src.Interfaces
{
    public interface IUserService
    {
        Task<ResponseModel<UserGetAllDto>> Create(CreateUserDto userDto);
        Task<ResponseModel<List<UserGetAllDto>>> ReadAll();
        Task<ResponseModel<UserGetAllDto>> Update(UpdateUserDto userDto, int id);
        Task<ResponseModel<UserGetAllDto>> Delete(int id);
    }
}