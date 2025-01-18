using AutoMapper;
using Microsoft.EntityFrameworkCore;
using userManagementSystemBack.src.Data;
using userManagementSystemBack.src.Dtos.UserDto;
using userManagementSystemBack.src.Interfaces;
using userManagementSystemBack.src.Models;


/**
** @author Ramadan Ismael
*/
namespace userManagementSystemBack.src.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel<UserGetAllDto>> Create(CreateUserDto userDto)
        {
            var response = new ResponseModel<UserGetAllDto>();
            try
            {
                if(_dataContext == null)
                {
                    response.Message = "Internal error: Database service is not configured.";
                    response.Status = false;
                    return response;
                }
                else
                {
                   var email = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email == userDto.Email);
                    if(email != null)
                    {
                        response.Message = "Email already exists.";
                        response.Status = false;
                        return response;
                    }
                    else
                    {
                        userDto.SetPassword();
                        var userMap = _mapper.Map<UserModel>(userDto);
                        await _dataContext.Users.AddAsync(userMap);
                        await _dataContext.SaveChangesAsync();

                        var getUserDto = _mapper.Map<UserGetAllDto>(userMap);
                        response.Datas = getUserDto;
                        response.Message = "User successfully registered!";
                        response.Status = true;
                    }
                }
            }
            catch(Exception error)
            {
                response.Message = $"An error occurred while trying to register the user: {error.Message}";
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<List<UserGetAllDto>>> ReadAll()
        {
            var response = new ResponseModel<List<UserGetAllDto>>();
            try
            {
                if(_dataContext == null)
                {
                    response.Message = "Internal error: Database service is not configured.";
                    response.Status = false;
                    return response;
                }
                else
                {
                    var userList = await _dataContext.Users.ToListAsync();
                    if(userList == null)
                    {
                        response.Message = "No users found.";
                        response.Status = false;
                        return response;
                    }

                    var getUserDto = _mapper.Map<List<UserGetAllDto>>(userList);
                    response.Datas = getUserDto;
                    response.Message = "Users successfuly retrieved!";
                    response.Status = true;
                }
            }
            catch(Exception error)
            {
                response.Message = $"An error occurred while retrieving the user: {error.Message}";
                response.Status = false;
            }
            return response;
        }

        public async Task<ResponseModel<UserGetAllDto>> Update(UpdateUserDto userDto)
        {
            var response = new ResponseModel<UserGetAllDto>();
            try
            {
                if(_dataContext == null)
                {
                    response.Message = "Internal error: Database service is not configured.";
                    response.Status = false;
                    return response;
                }
                else
                {
                    if(userDto.Id <= 0)
                    {
                        response.Message = "Invalid ID.";
                        response.Status = false;
                        return response;
                    }
                    else
                    {
                        var userExist = await _dataContext.Users.FindAsync(userDto.Id);
                        if(userExist == null)
                        {
                            response.Message = "User not found";
                            response.Status = false;
                            return response;
                        }
                        else
                        {
                            var email = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email == userDto.Email);
                            if(email != null)
                            {
                                response.Message = "Email already exists.";
                                response.Status = false;
                                return response;
                            }
                            else
                            {
                                var userMap = _mapper.Map(userDto, userExist);
                                _dataContext.Users.Update(userMap);
                                await _dataContext.SaveChangesAsync();

                                var getUserDto = _mapper.Map<UserGetAllDto>(userMap);
                                response.Datas = getUserDto;
                                response.Message = "User successfully updated!";
                                response.Status = true;
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
                if(id <= 0)
                {
                    response.Message = "Invalid ID.";
                    response.Status = false;
                    return response;
                }
                else
                {
                    var userExist = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);
                    if(userExist == null)
                    {
                        response.Message = "User not found";
                        response.Status = false;
                        return response;
                    }
                    else
                    {
                        _dataContext.Users.Remove(userExist);
                        await _dataContext.SaveChangesAsync();

                        response.Datas = userExist.Name;
                        response.Message = "User successfully deleted!";
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
    }
}