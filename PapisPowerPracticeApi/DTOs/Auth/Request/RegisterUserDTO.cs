using System.ComponentModel.DataAnnotations;

namespace PapisPowerPracticeApi.DTOs.Auth.Request
{
    public class RegisterUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
