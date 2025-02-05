using System.ComponentModel.DataAnnotations;
using UserManagementSystemBack.src.Configs;
using UserManagementSystemBack.src.Enums;

/*
*@author Ramadan Ismael
*/

namespace UserManagementSystemBack.src.DTOs.User
{
    public class CUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public UserProfileEnum Profile { get; set; }
        public UserStatusEnum Status { get; set; }

        public void SetPassword()
        {
            if(!string.IsNullOrEmpty(Password) || !string.IsNullOrWhiteSpace(Password))
            {
                Password = Password.EncryptPassword();
            }
            else
            {
                throw new ArgumentException("Password can not be null or empty..");
            }
        }
    }
}