/*
*@author Ramadan Ismael
*/
using System.ComponentModel;

namespace UserManagementSystemBack.src.Enums
{
    /// <summary>
    /// UserProfileEnum
    /// </summary>
    public enum UserProfileEnum
    {
        /// <summary>
        /// profile user
        /// </summary>
        [Description("Standard User - Viewing and modifying their own data")]
        User = 0,
        /// <summary>
        /// profile admin
        /// </summary>
        [Description("Administrator User - Full access across the system")]
        Admin = 1
    }
}