using System.ComponentModel.DataAnnotations;

namespace PapisPowerPracticeApi.DTOs.Auth.Request
{
    public class LoginUserDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
