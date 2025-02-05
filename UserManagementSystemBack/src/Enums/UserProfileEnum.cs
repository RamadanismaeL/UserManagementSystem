/*
*@author Ramadan Ismael
*/
using System.ComponentModel;

namespace UserManagementSystemBack.src.Enums
{
    public enum UserProfileEnum
    {
        [Description("Standard User - Viewing and modifying their own data")]
        User = 0,
        [Description("Administrator User - Full access across the system")]
        Admin = 1
    }
}