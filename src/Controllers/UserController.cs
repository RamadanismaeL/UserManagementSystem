using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using userManagementSystemBack.src.Dtos.UserDto;
using userManagementSystemBack.src.Interfaces;

namespace userManagementSystemBack.src.Controllers
{
    [ApiController]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) { this._userService = userService; }

        [HttpPost]
        [Route("/api/user/create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto userDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.Create(userDto);
            if(user.Status == false) return NotFound(user);
            return Ok(user);
        }

        [HttpGet]
        [Route("/api/user/readAll")]
        public async Task<IActionResult> ReadAll()
        {
            var user = await _userService.ReadAll();
            if(user.Status == false) return NotFound(user);
            return Ok(user);
        }

        [HttpPut]
        [Route("/api/user/update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto userDto, int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            userDto.Id = id;
            var user = await _userService.Update(userDto, id);
            if(user.Status == false) return NotFound(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Route("/api/user/delete{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var usuario = await _userService.Delete(id);
            if(usuario.Status == false) return BadRequest(usuario);
            return Ok(usuario);
        }
    }
}