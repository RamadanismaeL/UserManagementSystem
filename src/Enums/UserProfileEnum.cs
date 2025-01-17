
using System.ComponentModel;


/**
** @author Ramadan Ismael
*/
namespace userManagementSystemBack.src.Enums
{
    public enum UserProfileEnum
    {
        [Description("Standard User")]
        User = 0,
        [Description("Administrator User")]
        Admin = 1
    }
}