/*
*@author Ramadan Ismael
*/

namespace UserManagementSystemBack.src.Configs
{
    /// <summary>
    /// UserEncrypt Password to access this system
    /// </summary>
    public static class UserEncrypt
    {
        private const int _workFactor = 12;
        /// <summary>
        /// simple method to encrypt User Password
        /// </summary>
        /// <param name="value"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static string EncryptPassword(this string value, ILogger? logger = null)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Password is null or empty");
            }
            try
            {
                return BCrypt.Net.BCrypt.HashPassword(value, workFactor: _workFactor);
            }
            catch (Exception ex)
            {
                 logger?.LogError(ex, "Error to encrypt password");
                throw new Exception("Error to encrypt password", ex);
            }
        }
    }
}