using AuthService.Api.Controllers;
using AuthService.Domain.APIs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.AuthService.DTOs;

namespace AuthService.Test.Api
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenRegistrationSucceeds()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Email = "test@example.com",
                Password = "Password123!",
                FirstName = "John",
                LastName = "Doe",
                Local = "fr-FR",
                TimeZone = "UTC"
            };
            var response = new RegisterResponse
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                DisplayName = $"{request.FirstName} {request.LastName}"
            };

            _authServiceMock
                .Setup(s => s.RegisterAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Register(request, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedResponse = Assert.IsType<RegisterResponse>(okResult.Value);
            Assert.Equal(response.Email, returnedResponse.Email);
            Assert.Equal(response.DisplayName, returnedResponse.DisplayName);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Required");
            var request = new RegisterRequest();

            // Act
            var result = await _controller.Register(request, CancellationToken.None);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
