using System.ComponentModel.DataAnnotations;

namespace Amazone.Apis.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            ErrorMessage = "password must have at least :1 uppercase ,1 lowercase ,1 number ,one special character: @, $, !, %, , ?, &, or similar symbols")]

        public string Password { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;

    }
}
