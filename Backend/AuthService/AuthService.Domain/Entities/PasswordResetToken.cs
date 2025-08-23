namespace AuthService.Domain.Entities
{
    public class PasswordResetToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
