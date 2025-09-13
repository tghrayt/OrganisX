using AuthService.Domain.Entities;
using AuthService.Infrastructure.CommandQueryModels;
using AuthService.Infrastructure.CommandQueryProvisers;
using AuthService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.AuthService.DTOs;

namespace AuthService.Test.Infrastructure.CommandQueryProviders
{
    public class RegisterUserCommandTests
    {
        private AuthDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new AuthDbContext(options);
        }

        [Fact]
        public async Task Handle_ShouldRegisterUser_WhenEmailIsUnique()
        {
            // Arrange
            var dbContext = GetDbContext(Guid.NewGuid().ToString());
            var handler = new RegisterUserCommand(dbContext);
            var request = new RegisterRequestCommand(new RegisterRequest
            {
                Email = "unique@email.com",
                Password = "password",
                FirstName = "John",
                LastName = "Doe",
                Local = "fr-FR",
                TimeZone = "Europe/Paris"
            });

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.Email, response.Email);
            Assert.Equal($"{request.FirstName} {request.LastName}", response.DisplayName);
            Assert.NotEqual(Guid.Empty, response.UserId);

            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            Assert.NotNull(user);
            Assert.True(BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var dbContext = GetDbContext(Guid.NewGuid().ToString());
            dbContext.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Email = "exists@email.com",
                PasswordHash = "hash",
                FirstName = "Jane",
                LastName = "Doe",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            await dbContext.SaveChangesAsync();

            var handler = new RegisterUserCommand(dbContext);
            var request = new RegisterRequestCommand(new RegisterRequest
            {
                Email = "exists@email.com",
                Password = "password",
                FirstName = "John",
                LastName = "Doe",
                Local = "fr-FR",
                TimeZone = "Europe/Paris"
            });

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            Assert.Equal("Email déjà utilisé.", ex.Message);
        }
    }
}
