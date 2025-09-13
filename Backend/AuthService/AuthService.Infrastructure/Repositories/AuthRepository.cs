using AuthService.Application.SPIs;
using AuthService.Infrastructure.CommandQueryModels;
using MediatR;
using Shared.AuthService.DTOs;

namespace AuthService.Infrastructure.Repositories
{
    public class AuthRepository(IMediator mediator) : IAuthSpi
    {
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
           return await mediator.Send(new RegisterRequestCommand(request), CancellationToken.None);
        }
    }
}
