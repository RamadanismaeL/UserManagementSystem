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
    [Route("api/users")]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CUserDto user)
        {
            if(user == null) return BadRequest("User data is required.");
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _userRepository.Create(user);
            return response.Status ? CreatedAtAction(nameof(ReadAll), response) : Conflict(response);
        }

        [HttpGet]
        [Route("readAll")]
        public async Task<IActionResult> ReadAll()
        {
            var response = await _userRepository.ReadAll();
            return response.Status ? Ok(response) : NotFound(response);
        }

        [HttpPatch]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromBody] UUserDto user, [FromRoute] int id)
        {
            if(user == null) return BadRequest("User data is required.");
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _userRepository.Update(user, id);
            return response.Status ? Ok(response) : NotFound(response);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _userRepository.Delete(id);
            return response.Status ? Ok(response) : NotFound(response);
        }
    }
}