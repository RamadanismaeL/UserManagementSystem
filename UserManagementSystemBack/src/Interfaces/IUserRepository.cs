/*
*@author Ramadan Ismael
*/

using UserManagementSystemBack.src.DTOs.User;
using UserManagementSystemBack.src.Models;

namespace UserManagementSystemBack.src.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseModel<RUserDto>> Create(CUserDto user);
        Task<ResponseModel<List<RUserDto>>> ReadAll();
        Task<ResponseModel<RUserDto>> Update(UUserDto user, int id);
        Task<ResponseModel<string>> Delete(int id);
        Task<UserModel> FindUserName(string userName);
    }
}