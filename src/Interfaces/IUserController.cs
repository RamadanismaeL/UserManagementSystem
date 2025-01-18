using Microsoft.AspNetCore.Mvc;
using userManagementSystemBack.src.Dtos.UserDto;

namespace userManagementSystemBack.src.Interfaces
{
    public interface IUserController
    {
        Task<IActionResult> Create([FromBody] CreateUserDto userDto);
        Task<IActionResult> ReadAll();
        Task<IActionResult> Update([FromBody] UpdateUserDto userDto, [FromRoute] int id);
        Task<IActionResult> Delete([FromRoute] int id);
    }
}