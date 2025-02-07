using System.ComponentModel.DataAnnotations;
using UserManagementSystemBack.src.Configs;
using UserManagementSystemBack.src.Enums;

/*
*@author Ramadan Ismael
*/

namespace UserManagementSystemBack.src.DTOs.User
{
    /// <summary>
    /// Create User using DTOs
    /// </summary>
    public class CUserDto
    {
        /// <summary>
        /// User FirstName
        /// </summary>
        /// <value>string</value>
        public string? FirstName { get; set; }
        /// <summary>
        /// User LastName
        /// </summary>
        /// <value>string</value>
        public string? LastName { get; set; }
        /// <summary>
        /// User PhoneNumber
        /// </summary>
        /// <value>string</value>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// User email address
        /// </summary>
        /// <value>just EmailAddress like @</value>
        [EmailAddress]
        public string? Email { get; set; }
        /// <summary>
        /// User username to access the system
        /// </summary>
        /// <value>string</value>
        public string? UserName { get; set; }
        /// <summary>
        /// User Password to validate the system access
        /// </summary>
        /// <value></value>
        public string? Password { get; set; }
        /// <summary>
        /// User profile
        /// </summary>
        /// <value></value>
        public UserProfileEnum Profile { get; set; }
        /// <summary>
        /// User Status
        /// </summary>
        /// <value></value>
        public UserStatusEnum Status { get; set; }

        /// <summary>
        /// Simple methos to Password encrypt
        /// </summary>
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