/*
*@author Ramadan Ismael
*/

namespace UserManagementSystemBack.src.Services
{
    public static class UserEncrypt
    {
        private const int _workFactor = 12;
        // <summary>
        /// Encrypts a password using BCrypt.
        /// </summary>
        /// <param name="value">The password to encrypt.</param>
        /// <param name="logger">Optional logger for error logging.</param>
        /// <returns>The hashed password.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the password is null or empty.</exception>
        /// <exception cref="Exception">Thrown if an error occurs during encryption.</exception>
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