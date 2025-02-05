/*
*@author Ramadan Ismael
*/

using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public UserRepository(UserManagementSystem_db dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<ResponseModel<RUserDto>> Create(CUserDto user)
        {
            var response = new ResponseModel<RUserDto>();
            bool checkUserFullName = await _dataContext.Users
                .AnyAsync(u => u.FirstName == user.FirstName && u.LastName == user.LastName);
            bool checkUserEmail = await _dataContext.Users
                .AnyAsync(u => u.Email == user.Email);
            bool checkUserUserName = await _dataContext.Users
                .AnyAsync(u => u.UserName == user.UserName);

            try
            {
                if(_dataContext == null)
                {
                    response.Message = "The database is not connected";
                    response.Status = false;
                }
                else
                {
                    if(checkUserFullName)
                    {
                        response.Message = "The user not available.";
                        response.Status = false;
                    }
                    else
                    {
                        if(checkUserEmail)
                        {
                            response.Message = "Please choose a different email.";
                            response.Status = false;
                        }
                        else if(checkUserUserName)
                        {
                            response.Message = "Username not available.";
                            response.Status = false;
                        }
                        else
                        {
                            user.SetPassword();
                            var userMap = _mapper.Map<UserModel>(user);
                            await _dataContext.Users.AddAsync(userMap);
                            await _dataContext.SaveChangesAsync();

                            var getUser = _mapper.Map<RUserDto>(userMap);
                            response.Datas = getUser;
                            response.Message = "User created successfully.";
                            response.Status = true;
                        }
                    }
                }
            }
            catch(Exception error)
            {
                response.Message = $"An error occurred while creating the user: {error.Message}";
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<List<RUserDto>>> ReadAll()
        {
            var response = new ResponseModel<List<RUserDto>>();
            try
            {
                if(_dataContext == null)
                {
                    response.Message = "The database is not connected";
                    response.Status = false;
                }
                else
                {
                    var userList = await _dataContext.Users.ToListAsync();
                    if(userList.Count == 0)
                    {
                        response.Message = "No users found.";
                        response.Status = false;
                    }

                    var getUsers = _mapper.Map<List<RUserDto>>(userList);
                    response.Datas = getUsers;
                    response.Message = "Users read successfully.";
                    response.Status = true;
                }
            }
            catch(Exception error)
            {
                response.Message = $"An error occurred while reading the users: {error.Message}";
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<RUserDto>> Update(UUserDto user)
        {
            var response = new ResponseModel<RUserDto>();
            var userExist = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
            var allData = await _dataContext.Users
                .Where(u => u.Id == user.Id)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.PhoneNumber,
                    u.Email,
                    u.UserName,
                    u.Profile,
                    u.Status
                })
                .FirstOrDefaultAsync();
            var email_username_exit = await _dataContext.Users
                .Where(u => u.Id == user.Id)
                .Select(u => new
                {
                    u.Email,
                    u.UserName
                })
                .FirstOrDefaultAsync();
            var emailExist = await _dataContext.Users.AnyAsync(u => u.Email == user.Email);
            var userNameExist = await _dataContext.Users.AnyAsync(u => u.UserName == user.UserName);

            try
            {
                if(_dataContext == null)
                {
                    response.Message = "The database is not connected";
                    response.Status = false;
                }
                else
                {
                    if(user.Id <= 0 || userExist == null)
                    {
                        response.Message = "The user not found.";
                        response.Status = false;
                    }
                    else
                    {
                        if(allData is not null &&
                        allData.FirstName == user.FirstName &&
                        allData.LastName == user.LastName &&
                        allData.PhoneNumber == user.PhoneNumber &&
                        allData.Email == user.Email &&
                        allData.UserName == user.UserName &&
                        allData.Profile == user.Profile &&
                        allData.Status == user.Status)
                        {
                            response.Message = "No changes were made.";
                            response.Status = false;
                        }
                        else
                        {
                            if(email_username_exit is not null &&
                            email_username_exit.Email == user.Email &&
                            email_username_exit.UserName == user.UserName)
                            {
                                var userMap = _mapper.Map(user, userExist);
                                _dataContext.Users.Update(userMap);
                                await _dataContext.SaveChangesAsync();

                                var getUser = _mapper.Map<RUserDto>(userMap);
                                response.Datas = getUser;
                                response.Message = "User updated successfully.";
                                response.Status = true;
                            }
                            else
                            {
                                if(emailExist && user.Email != email_username_exit?.Email)
                                {
                                    response.Message = "Please choose a different email.";
                                    response.Status = false;
                                }
                                else if(userNameExist && user.UserName != email_username_exit?.UserName)
                                {
                                    response.Message = "Username not available.";
                                    response.Status = false;
                                }
                                else
                                {
                                    var userMap = _mapper.Map(user, userExist);
                                    _dataContext.Users.Update(userMap);
                                    await _dataContext.SaveChangesAsync();

                                    var getUser = _mapper.Map<RUserDto>(userMap);
                                    response.Datas = getUser;
                                    response.Message = "User updated successfully.";
                                    response.Status = true;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception error)
            {
                response.Message = $"An error occurred while updating the user: {error.Message}";
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<string>> Delete(int id)
        {
            var response = new ResponseModel<string>();
            try
            {
                if(_dataContext == null)
                {
                    response.Message = "The database is not connected";
                    response.Status = false;
                }
                else
                {
                    var userExist = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);
                    if(id <= 0 || userExist == null)
                    {
                        response.Message = "The user not found.";
                        response.Status = false;
                    }
                    else
                    {
                        _dataContext.Users.Remove(userExist);
                        await _dataContext.SaveChangesAsync();
                        
                        response.Datas = "{\n\tFullName : "+userExist.FirstName+" "+userExist.LastName+"\n\tUserName : "+userExist.UserName+"\n\tEmail : "+userExist.Email+"\n\tProfile : "+userExist.Profile+"\n}";
                        response.Message = "User deleted successfully.";
                        response.Status = true;
                    }
                }
            }
            catch(Exception error)
            {
                response.Message = $"An error occurred while deleting the user: {error.Message}";
                response.Status = false;
            }
            return response;
        }

        public async Task<UserModel> FindUserName(string userName)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(u => (u.UserName ?? "").Equals(userName, StringComparison.Ordinal)) ?? throw new Exception("User not found.");
        }

        
    }
}