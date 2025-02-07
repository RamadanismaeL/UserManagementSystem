/*
*@author Ramadan Ismael
*/
using System.ComponentModel;

namespace UserManagementSystemBack.src.Enums
{
    /// <summary>
    /// User Status enum
    /// </summary>
    public enum UserStatusEnum
    {
        /// <summary>
        /// Sentinela
        /// </summary>
        [Description("Not Set")]
        NotSet = -1,  // Sentinela
        /// <summary>
        /// User do not access the system
        /// </summary>
        [Description("User Inactive")]
        Inactive = 0,
        /// <summary>
        /// user access
        /// </summary>
        [Description("User Active")]
        Active = 1
    }
}