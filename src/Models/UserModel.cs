using userManagementSystemBack.src.Configs;
using userManagementSystemBack.src.Enums;
/**
** @author Ramadan Ismael
*/
namespace userManagementSystemBack.src.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserProfileEnum Profile { get; set; }
        public UserStatusEnum Status { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime DateUpdate { get; set; }

        public bool VerifyPassword(string password) { return BCrypt.Net.BCrypt.Verify(password, Password); }

        public void SetNewPassword(string newPassword) { Password = newPassword.EncryptPassword(); }

        public string GetNewPassword()
        {
            string newPassword = Guid.NewGuid().ToString()[..8];
            Password = newPassword.EncryptPassword();
            return newPassword;
        }
    }
}