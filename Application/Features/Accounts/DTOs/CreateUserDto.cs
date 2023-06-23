using System.ComponentModel.DataAnnotations;

namespace Application.Features.Accounts.DTOs
{
    public class CreateUserDto 
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least 8 characters, including at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

    }
}