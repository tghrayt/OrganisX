using AuthService.Application.SPIs;
using AuthService.Domain.APIs;
using Shared.AuthService.DTOs;

namespace AuthService.Application.Services
{
    public class AuthServiceImpl(IAuthSpi authSpi) : IAuthService
    {

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            return await authSpi.RegisterAsync(request, cancellationToken);
        }
    }
}
