using userManagementSystemBack.src.Configs;
using userManagementSystemBack.src.Enums;
/**
** @author Ramadan Ismael
*/
namespace userManagementSystemBack.src.Dtos.UserDto
{
    public class CreateUserDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserProfileEnum Profile { get; set; }
        public UserStatusEnum Status { get; set; }

        public void SetPassword()
        {
            if(!string.IsNullOrEmpty(Password)) { Password = Password.EncryptPassword(); }
            else { throw new ArgumentException("Password can't be null or empty.."); }
        }
    }
}