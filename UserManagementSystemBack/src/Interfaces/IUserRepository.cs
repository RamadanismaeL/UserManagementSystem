/*
*@author Ramadan Ismael
*/

using UserManagementSystemBack.src.DTOs.User;
using UserManagementSystemBack.src.Models;

namespace UserManagementSystemBack.src.Interfaces
{
    /// <summary>
    /// User Repository Interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Create User using DTOs
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ResponseModel<RUserDto>> Create(CUserDto user);
        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        Task<ResponseModel<List<RUserDto>>> ReadAll();
        /// <summary>
        /// Update User using DTOs
        /// </summary>
        /// <param name="user">dtos user</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<ResponseModel<RUserDto>> Update(UUserDto user, int id);
        /// <summary>
        /// delete User using DTOs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseModel<string>> Delete(int id);
        /// <summary>
        /// find some username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<UserModel> FindUserName(string userName);
    }
}