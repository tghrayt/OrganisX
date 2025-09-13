using AuthService.Infrastructure.CommandQueryModels;
using AuthService.Infrastructure.Repositories;
using MediatR;
using Moq;
using Shared.AuthService.DTOs;

namespace AuthService.Test.Infrastructure.Repositories
{
    public class AuthRepositoryTests
    {
        [Fact]
        public async Task RegisterAsync_ShouldSendRegisterRequestCommand_AndReturnResponse()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var request = new RegisterRequest("test@email.com", "password", "John", "Doe", "fr-FR", "Europe/Paris");
            var expectedResponse = new RegisterResponse
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                DisplayName = "John Doe"
            };

            mockMediator
                .Setup(m => m.Send(It.IsAny<RegisterRequestCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var repository = new AuthRepository(mockMediator.Object);

            // Act
            var result = await repository.RegisterAsync(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse.UserId, result.UserId);
            Assert.Equal(expectedResponse.Email, result.Email);
            Assert.Equal(expectedResponse.DisplayName, result.DisplayName);
            mockMediator.Verify(m => m.Send(It.Is<RegisterRequestCommand>(cmd =>
                cmd.Email == request.Email &&
                cmd.Password == request.Password &&
                cmd.FirstName == request.FirstName &&
                cmd.LastName == request.LastName &&
                cmd.Local == request.Local &&
                cmd.TimeZone == request.TimeZone
            ), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
