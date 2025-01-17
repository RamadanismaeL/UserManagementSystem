using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using userManagementSystemBack.src.Dtos.UserDto;

namespace userManagementSystemBack.src.Interfaces
{
    public interface IUserController
    {
        Task<IActionResult> Create([FromBody] CreateUserDto userDto);
        Task<IActionResult> ReadAll();
        Task<IActionResult> Update([FromBody] UpdateUserDto userDto, int id);
        Task<IActionResult> Delete(int id);
    }
}