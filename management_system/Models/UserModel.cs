using System.ComponentModel.DataAnnotations;

namespace Management_system.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }

        public UserModel (int id, string email, string name, string password)
        {
            Id = id;
            Email = email;
            Name = name;
            Password = password;

            Validate();
        }

        public void Validate () 
        {
            ValidateName();
            ValidateEmail();
            ValidatePassword();
        }

        private void ValidateName()
        {
            if (string.IsNullOrEmpty(Name) || Name.Length <= 2 || Name.Length >= 20) 
            {
                throw new ValidationException("O Nome é obrigatório.");
            }
        }
        private void ValidateEmail()
        {
            if (string.IsNullOrEmpty(Email) || Email.Length > 40)
            {
                throw new ValidationException("O Email é obrigatório.");
            }
        }
        private void ValidatePassword()
        {
            if (string.IsNullOrEmpty(Password) || Email.Length < 8)
            {
                throw new ValidationException("A Senha deve ter no mínimo 8 caracteres.");
            }
        }
    }
}
