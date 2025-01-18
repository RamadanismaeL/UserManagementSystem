using Microsoft.AspNetCore.Mvc;
using userManagementSystemBack.src.Dtos.UserDto;
using userManagementSystemBack.src.Interfaces;

namespace userManagementSystemBack.src.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this._userService = userService;
            this._logger = logger;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto userDto)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);
                var user = await _userService.Create(userDto);
                if(user.Status == false) return NotFound(user);
                return Ok(user);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occured! - Create");
                return StatusCode(400, "Error Message.");
            }
        }

        [HttpGet]
        [Route("readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var user = await _userService.ReadAll();
                if(user.Status == false) return NotFound(user);
                return Ok(user);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occured! - ReadAll");
                return StatusCode(401, "Error Message.");
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto userDto, [FromRoute] int id)
        {
            try
            {
                userDto.Id = id;
                userDto.DateUpdate = DateTime.Now;
                //Console.WriteLine($"ID : {id}\n -- \nData: {userDto.DateUpdate} \n\n");
                if(!ModelState.IsValid) return BadRequest(ModelState);
                var user = await _userService.Update(userDto);
                if(user.Status == false) return NotFound(user);
                return Ok(user);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occured! - Update");
                return StatusCode(402, "Error Message.");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);
                var user = await _userService.Delete(id);
                if(user.Status == false) return NotFound(user);
                return Ok(user);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occured! - Delete");
                return StatusCode(403, "Error Message.");
            }
        }
    }
}