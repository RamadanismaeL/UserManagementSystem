
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

                userDto.SetPassword();
                var userMap = _mapper.Map<UserModel>(userDto);
                await _dataContext.Users.AddAsync(userMap);
                await _dataContext.SaveChangesAsync();

                var getUserDto = _mapper.Map<UserGetAllDto>(userMap);
                response.Datas = getUserDto;
                response.Message = "User Successfuly registered!";
                response.Status = true;
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
                var userList = await _dataContext.Users.ToListAsync();
                if(userList == null)
                {
                    response.Message = "No users found.";
                    response.Status = false;
                    return response;
                }
            }
            catch(Exception error)
            {
                response.Message = $"An error occurred while retrieving the user: {error.Message}";
                response.Status = false;
            }
        }

        public Task<ResponseModel<UserGetAllDto>> Update(UpdateUserDto userDto, int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<UserGetAllDto>> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}