/*
*@author Ramadan Ismael
*/
using System.ComponentModel;

namespace UserManagementSystemBack.src.Enums
{
    public enum UserStatusEnum
    {
        [Description("Not Set")]
        NotSet = -1,  // Sentinela

        [Description("User Inactive")]
        Inactive = 0,

        [Description("User Active")]
        Active = 1
    }
}