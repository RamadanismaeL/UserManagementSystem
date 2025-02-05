/*
*@author Ramadan Ismael
*/

using Microsoft.AspNetCore.Mvc;
using UserManagementSystemBack.src.DTOs.User;

namespace UserManagementSystemBack.src.Interfaces
{
    public interface IUserController
    {
        Task<IActionResult> Create([FromBody] CUserDto user);
        Task<IActionResult> ReadAll();
        Task<IActionResult> Update([FromBody] UUserDto user, [FromRoute] int id);
        Task<IActionResult> Delete([FromRoute] int id);
    }
}