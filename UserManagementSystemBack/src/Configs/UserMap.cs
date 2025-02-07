/*
*@author Ramadan Ismael
*/

using AutoMapper;
using UserManagementSystemBack.src.DTOs.User;
using UserManagementSystemBack.src.Models;

namespace UserManagementSystemBack.src.Configs
{
    /// <summary>
    /// Data Transfere Objects Configuration
    /// </summary>
    public class UserMap : Profile
    {
        /// <summary>
        /// simple method that do relational
        /// </summary>
        public UserMap()
        {
            CreateMap<UserModel, RUserDto>();
            CreateMap<CUserDto, UserModel>();
            CreateMap<UUserDto, UserModel>();
        }
    }
}