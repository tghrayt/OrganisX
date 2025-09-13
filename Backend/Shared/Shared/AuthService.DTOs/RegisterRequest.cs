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

        public string Local { get; set; }
        public string TimeZone { get; set; }

        public RegisterRequest(string email, string password, string firstName, string lastName, string local, string timeZone)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Local = local ?? "fr-FR";
            TimeZone = timeZone ?? "UTC";
        }

        public RegisterRequest()
        {
            Local = "fr-FR";
            TimeZone = "UTC";
        }
    }
}
