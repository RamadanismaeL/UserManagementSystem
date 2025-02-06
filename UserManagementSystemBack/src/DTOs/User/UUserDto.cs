/*
*@author Ramadan Ismael
*/

using System.ComponentModel.DataAnnotations;
using UserManagementSystemBack.src.Enums;

namespace UserManagementSystemBack.src.DTOs.User
{
    public class UUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public UserProfileEnum Profile { get; set; }
        public UserStatusEnum Status { get; set; }
    }
}