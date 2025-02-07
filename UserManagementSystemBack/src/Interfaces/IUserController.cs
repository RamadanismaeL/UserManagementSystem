/*
*@author Ramadan Ismael
*/

using Microsoft.AspNetCore.Mvc;
using UserManagementSystemBack.src.DTOs.User;

namespace UserManagementSystemBack.src.Interfaces
{
    /// <summary>
    /// UserController Interface
    /// </summary>
    public interface IUserController
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IActionResult> Create([FromBody] CUserDto user);
        /// <summary>
        /// get user data
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> ReadAll();
        /// <summary>
        /// update users data
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IActionResult> Update([FromBody] UUserDto user, [FromRoute] int id);
        /// <summary>
        /// delete any user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IActionResult> Delete([FromRoute] int id);
    }
}