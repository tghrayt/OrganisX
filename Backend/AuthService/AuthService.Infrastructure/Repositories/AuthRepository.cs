using AuthService.Application.SPIs;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.AuthService.DTOs;

namespace AuthService.Infrastructure.Repositories
{
    public class AuthRepository : IAuthSpi
    {

        private readonly AuthDbContext _context;

        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                throw new Exception("Email déjà utilisé.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = passwordHash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Local = request.Local,
                TimeZone = request.TimeZone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            user.AuditLogs.Add(new AuditLog
            {
                Id = Guid.NewGuid(),
                Action = $"User Registered : {user.FirstName} {user.LastName}",
                Timestamp = DateTime.UtcNow,
                Description = "Nouvel utilisateur inscrit via Register endpoint"
            });

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new RegisterResponse
            {
                UserId = user.Id,
                Email = user.Email,
                DisplayName = $"{user.FirstName} {user.LastName}"
            };
        }
    }
}
