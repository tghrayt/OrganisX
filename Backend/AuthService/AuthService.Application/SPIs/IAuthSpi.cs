using Shared.AuthService.DTOs;

namespace AuthService.Application.SPIs
{
    public interface IAuthSpi
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
    }
}
