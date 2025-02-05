/*
*@author Ramadan Ismael
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserManagementSystemBack.src.DTOs.User;
using UserManagementSystemBack.src.Interfaces;

namespace UserManagementSystemBack.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CUserDto user)
        {
            try
            {
                if(user == null) return BadRequest("User data is required.");
                if(!ModelState.IsValid) return BadRequest(ModelState);
                var newUser = await _userRepository.Create(user);
                if(newUser.Status == false) return Conflict(newUser);
                return Ok(newUser);
            }
            catch(Exception error)
            {
                _logger.LogError(error, "An error occurred while creating a user.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet]
        [Route("readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var userList = await _userRepository.ReadAll();
                if(userList.Status == false) return NotFound(userList);
                return Ok(userList);
            }
            catch(Exception error)
            {
                _logger.LogError(error, "An error occurred while reading all users.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPatch]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromBody] UUserDto user, [FromRoute] int id)
        {
            try
            {
                if(user == null) return BadRequest("User data is required.");
                if(!ModelState.IsValid) return BadRequest(ModelState);
                //user.Id = id;
                user.DateUpdate = DateTime.UtcNow;
                var newUser = await _userRepository.Update(user);
                if(newUser.Status == false) return NotFound(newUser);
                Console.WriteLine($"ID = {user.Id}");
                return Ok(newUser);
            }
            catch(Exception error)
            {
                _logger.LogError(error, "An error occurred while updating a user.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var user = await _userRepository.Delete(id);
                if(user.Status == false) return NotFound(user);
                return Ok(user);
            }
            catch(Exception error)
            {
                _logger.LogError(error, "An error occurred while deleting a user.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}