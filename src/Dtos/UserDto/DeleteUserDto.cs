using userManagementSystemBack.src.Enums;
/**
** @author Ramadan Ismael
*/
namespace userManagementSystemBack.src.Dtos.UserDto
{
    public class DeleteUserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public UserProfileEnum Profile { get; set; }
        public UserStatusEnum Status { get; set; }
    }
}