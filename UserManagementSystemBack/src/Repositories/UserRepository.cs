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

        public Task<ResponseModel<List<RUserDto>>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<RUserDto>> Update(UUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<string>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> FindByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        
    }
}