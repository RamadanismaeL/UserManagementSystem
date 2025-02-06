/*
 * @author Ramadan Ismael
 */

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagementSystemBack.src.Data;
using UserManagementSystemBack.src.DTOs.User;
using UserManagementSystemBack.src.Interfaces;
using UserManagementSystemBack.src.Models;

namespace UserManagementSystemBack.src.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementSystem_db _dataContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserManagementSystem_db dataContext, IMapper mapper, ILogger<UserRepository> logger)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ResponseModel<RUserDto>> Create(CUserDto user)
        {
            if (user == null)
                return BadRequestResponse<RUserDto>("User data is required.");

            try
            {
                if (await UserExists(user))
                    return ConflictResponse<RUserDto>("User not available.");

                user.SetPassword();
                var userEntity = _mapper.Map<UserModel>(user);
                await _dataContext.Users.AddAsync(userEntity);
                await _dataContext.SaveChangesAsync();

                return new ResponseModel<RUserDto>
                {
                    Datas = _mapper.Map<RUserDto>(userEntity),
                    Message = "User created successfully.",
                    Status = true
                };
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error creating user.");
                return ErrorResponse<RUserDto>("An unexpected error occurred.");
            }
        }

        public async Task<ResponseModel<List<RUserDto>>> ReadAll()
        {
            try
            {
                var users = await _dataContext.Users.AsNoTracking().ToListAsync();
                return new ResponseModel<List<RUserDto>>
                {
                    Datas = _mapper.Map<List<RUserDto>>(users),
                    Message = users.Any() ? "Users retrieved successfully." : "No users found.",
                    Status = true
                };
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error reading users.");
                return ErrorResponse<List<RUserDto>>("An unexpected error occurred.");
            }
        }

        public async Task<ResponseModel<RUserDto>> Update(UUserDto user, int id)
        {
            if (user == null)
                return BadRequestResponse<RUserDto>("User data is required.");

            try
            {
                var existingUser = await _dataContext.Users.FindAsync(id);
                if (existingUser == null)
                    return NotFoundResponse<RUserDto>("User not found.");

                if (await IsDuplicateEmailOrUsername(user, id))
                    return ConflictResponse<RUserDto>("Email or username not available.");

                existingUser.DateUpdate = DateTime.Now;

                _mapper.Map(user, existingUser);
                _dataContext.Users.Update(existingUser);
                await _dataContext.SaveChangesAsync();

                return new ResponseModel<RUserDto>
                {
                    Datas = _mapper.Map<RUserDto>(existingUser),
                    Message = "User updated successfully.",
                    Status = true
                };
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error updating user.");
                return ErrorResponse<RUserDto>("An unexpected error occurred.");
            }
        }

        public async Task<ResponseModel<string>> Delete(int id)
        {
            try
            {
                var user = await _dataContext.Users.FindAsync(id);
                if (user == null)
                    return NotFoundResponse<string>("User not found.");

                _dataContext.Users.Remove(user);
                await _dataContext.SaveChangesAsync();

                return new ResponseModel<string>
                {
                    Datas = $"FullName : {user.FirstName} {user.LastName}",
                    Message = "User deleted successfully.",
                    Status = true
                };
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error deleting user.");
                return ErrorResponse<string>("An unexpected error occurred.");
            }
        }

        public async Task<UserModel> FindUserName(string userName)
        {
            var user = await _dataContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == userName);

            return user ?? throw new KeyNotFoundException("User not found.");
        }

        // 🔹 Métodos auxiliares para respostas padronizadas
        private async Task<bool> UserExists(CUserDto user) =>
            await _dataContext.Users.AnyAsync(u => u.Email == user.Email || u.UserName == user.UserName);

        private async Task<bool> IsDuplicateEmailOrUsername(UUserDto user, int id) =>
            await _dataContext.Users.AnyAsync(u => (u.Email == user.Email || u.UserName == user.UserName) && u.Id != id);

        private static ResponseModel<T> BadRequestResponse<T>(string message) =>
            new() { Message = message, Status = false };

        private static ResponseModel<T> NotFoundResponse<T>(string message) =>
            new() { Message = message, Status = false };

        private static ResponseModel<T> ConflictResponse<T>(string message) =>
            new() { Message = message, Status = false };

        private static ResponseModel<T> ErrorResponse<T>(string message) =>
            new() { Message = message, Status = false };
    }
}
