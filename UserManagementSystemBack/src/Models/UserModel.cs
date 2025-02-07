/*
* @author Ramadan Ismael
*/
using System.ComponentModel.DataAnnotations;
using UserManagementSystemBack.src.Enums;
using UserManagementSystemBack.src.Configs;

namespace UserManagementSystemBack.src.Models
{
    /// <summary>
    /// UserModel
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// User id
        /// </summary> <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// User FirstName
        /// </summary>
        /// <value>string</value>
        public string? FirstName { get; set; }
        /// <summary>
        /// User LastName
        /// </summary>
        /// <value>string</value>
        public string? LastName { get; set; }
        /// <summary>
        /// User PhoneNumber
        /// </summary>
        /// <value>string</value>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// User email address
        /// </summary>
        /// <value>just EmailAddress like @</value>
        [EmailAddress]
        public string? Email { get; set; }
        /// <summary>
        /// User username to access the system
        /// </summary>
        /// <value>string</value>
        public string? UserName { get; set; }
        /// <summary>
        /// User Password to validate the system access
        /// </summary>
        /// <value></value>
        public string? Password { get; set; }
        /// <summary>
        /// User profile
        /// </summary>
        /// <value></value>
        public UserProfileEnum Profile { get; set; }
        /// <summary>
        /// User Status
        /// </summary>
        /// <value></value>
        public UserStatusEnum Status { get; set; }
        /// <summary>
        /// User date registered
        /// </summary>
        /// <value></value>
        public DateTime DateRegister { get; set; }
        /// <summary>
        /// User date updated anything
        /// </summary>
        /// <value></value>
        public DateTime? DateUpdate { get; set; }

        /// <summary>
        /// Password Compare
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        /// <summary>
        /// Cadastre new Password
        /// </summary>
        /// <param name="newPassword"></param>
        public void SetNewPassword(string newPassword)
        {
            Password = newPassword.EncryptPassword();
        }

        /// <summary>
        /// Reset Password (Esqueci minha senha) - O sistema gera nova senha temporária e pode enviá-la por e-mail ou SMS
        ///string tempPassword = user.GetNewPassword();
        ///emailService.Send(user.Email, "Your new password is: " + tempPassword);        
        ///User newUser = new User { Email = "user@example.com" };
        ///string tempPassword = newUser.GetNewPassword();
        ///userRepository.Save(newUser);
        ///emailService.Send(newUser.Email, "Welcome! Your temporary password is: " + tempPassword);
        /// </summary>
        /// <returns>password encryped</returns>
        public string GetNewPassword()
        {
            string newPassword = Guid.NewGuid().ToString()[..12]; // Gera uma nova senha aleatória de 12 caracteres
            Password = newPassword.EncryptPassword(); //Criptografa a senha e armazena no atributo Password
            return newPassword; //  Retorna a senha sem criptografia
        }
    }
}