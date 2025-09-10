namespace Shared.AuthService.DTOs
{
    public class RegisterResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
