using AuthService.Application.Services;
using AuthService.Application.SPIs;
using Moq;
using Shared.AuthService.DTOs;

namespace AuthService.Test.Application
{
    public class AuthServiceImplTests
    {
        [Fact]
        public async Task RegisterAsync_CallsAuthSpiAndReturnsResponse()
        {
            // Arrange
            var mockAuthSpi = new Mock<IAuthSpi>();
            var request = new RegisterRequest("test@email.com", "password", "John", "Doe", "fr-FR", "Europe/Paris");
            var expectedResponse = new RegisterResponse
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                DisplayName = "John Doe"
            };

            mockAuthSpi
                .Setup(s => s.RegisterAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var service = new AuthServiceImpl(mockAuthSpi.Object);

            // Act
            var result = await service.RegisterAsync(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse.UserId, result.UserId);
            Assert.Equal(expectedResponse.Email, result.Email);
            Assert.Equal(expectedResponse.DisplayName, result.DisplayName);
            mockAuthSpi.Verify(s => s.RegisterAsync(request, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
