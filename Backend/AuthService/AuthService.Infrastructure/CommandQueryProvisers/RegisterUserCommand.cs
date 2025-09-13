using AuthService.Domain.Entities;
using AuthService.Infrastructure.CommandQueryModels;
using AuthService.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.AuthService.DTOs;

namespace AuthService.Infrastructure.CommandQueryProvisers
{
    public sealed class RegisterUserCommand(AuthDbContext context) : IRequestHandler<RegisterRequestCommand, RegisterResponse>
    {
        public async Task<RegisterResponse> Handle(RegisterRequestCommand request, CancellationToken cancellationToken)
        {
            if (await context.Users.AnyAsync(u => u.Email == request.Email))
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

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new RegisterResponse
            {
                UserId = user.Id,
                Email = user.Email,
                DisplayName = $"{user.FirstName} {user.LastName}"
            };
        }
    }
}
