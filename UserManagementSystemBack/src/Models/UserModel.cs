/*
* @author Ramadan Ismael
*/
using System.ComponentModel.DataAnnotations;
using UserManagementSystemBack.src.Enums;
using UserManagementSystemBack.src.Configs;

namespace UserManagementSystemBack.src.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public UserProfileEnum Profile { get; set; }
        public UserStatusEnum Status { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime? DateUpdate { get; set; }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        public void SetNewPassword(string newPassword)
        {
            Password = newPassword.EncryptPassword();
        }

        // Reset Password (Esqueci minha senha) - O sistema gera nova senha temporária e pode enviá-la por e-mail ou SMS
        /*
        string tempPassword = user.GetNewPassword();
        emailService.Send(user.Email, "Your new password is: " + tempPassword);
        
        User newUser = new User { Email = "user@example.com" };
        string tempPassword = newUser.GetNewPassword();
        userRepository.Save(newUser);
        emailService.Send(newUser.Email, "Welcome! Your temporary password is: " + tempPassword);
        */
        public string GetNewPassword()
        {
            string newPassword = Guid.NewGuid().ToString()[..12]; // Gera uma nova senha aleatória de 12 caracteres
            Password = newPassword.EncryptPassword(); //Criptografa a senha e armazena no atributo Password
            return newPassword; //  Retorna a senha sem criptografia
        }
    }
}