using System.ComponentModel.DataAnnotations;

namespace Shared.AuthService.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Local { get; set; } = "fr-FR";
        public string TimeZone { get; set; } = "UTC";
    }
}
