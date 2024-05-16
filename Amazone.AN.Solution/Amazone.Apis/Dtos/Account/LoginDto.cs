using System.ComponentModel.DataAnnotations;

namespace Amazone.Apis.Dtos.Account
{
    public class LoginDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

    }
}
