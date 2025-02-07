/*
*@author Ramadan Ismael
*/

using System.ComponentModel.DataAnnotations;
using UserManagementSystemBack.src.Enums;

namespace UserManagementSystemBack.src.DTOs.User
{
    /// <summary>
    /// Update User DTOs
    /// </summary>
    public class UUserDto
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
        /// <value>string?</value>
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
        /// User profile
        /// </summary>
        /// <value></value>
        public UserProfileEnum Profile { get; set; }
        /// <summary>
        /// User Status
        /// </summary>
        /// <value></value>
        public UserStatusEnum Status { get; set; }
    }
}