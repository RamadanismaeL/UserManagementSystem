/**
** @author Ramadan Ismael
*/
namespace userManagementSystemBack.src.Configs
{
    public static class Encrypt
    {
        private const int _workFactor = 12;
        public static string EncryptPassword(this string value)
        {
            if(string.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException(nameof(value), "The password cannot be null or whitespace.");}
            try{ return BCrypt.Net.BCrypt.HashPassword(value, workFactor: _workFactor); }
            catch(Exception error)
            {
                Console.Error.WriteLine($"Encryption error: {error.Message}");
                throw new InvalidOperationException("An error occurred during password encryption.", error);
            }
        }
    }
}