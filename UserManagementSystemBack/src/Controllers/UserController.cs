/*
*@author Ramadan Ismael
*/

using Microsoft.AspNetCore.Mvc;
using UserManagementSystemBack.src.DTOs.User;
using UserManagementSystemBack.src.Interfaces;

namespace UserManagementSystemBack.src.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository">Take a userRepository method</param>
        /// <param name="logger">Invoke error logs</param>
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Create new system user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>User Successfully created or an error</returns>
        /// <response code="201">User successfully created</response>
        /// <response code="400">Invalid Datas in request</response>
        /// <response code="409">Conflit - User already exist</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CUserDto user)
        {
            if(user == null) return BadRequest("User data is required.");
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _userRepository.Create(user);
            return response.Status ? CreatedAtAction(nameof(ReadAll), response) : Conflict(response);
        }

        /// <summary>
        /// Return all user already exist
        /// </summary>
        /// <returns>Users list or error message</returns>
        /// <returns>Lista de usuários ou erro caso não haja registros.</returns>
        /// <response code="200">Lista de usuários retornada com sucesso.</response>
        /// <response code="404">Nenhum usuário encontrado.</response>
        /// <response code="500">Erro inesperado no servidor.</response>
        [HttpGet]
        [Route("readAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadAll()
        {
            var response = await _userRepository.ReadAll();
            return response.Status ? Ok(response) : NotFound(response);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="user">Objeto contendo os novos dados do usuário.</param>
        /// <param name="id">ID do usuário a ser atualizado.</param>
        /// <returns>Retorna os dados atualizados ou erro caso não encontrado.</returns>
        /// <response code="200">Usuário atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos na requisição.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro inesperado no servidor.</response>
        [HttpPatch]
        [Route("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UUserDto user, [FromRoute] int id)
        {
            if(user == null) return BadRequest("User data is required.");
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _userRepository.Update(user, id);
            return response.Status ? Ok(response) : NotFound(response);
        }

        /// <summary>
        /// Exclui um usuário com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID do usuário a ser excluído.</param>
        /// <returns>Confirmação da exclusão ou erro caso não encontrado.</returns>
        /// <response code="204">Usuário deletado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro inesperado no servidor.</response>
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _userRepository.Delete(id);
            return response.Status ? Ok(response) : NotFound(response);
        }
    }
}