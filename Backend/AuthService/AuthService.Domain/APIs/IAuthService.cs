using Shared.AuthService.DTOs;

namespace AuthService.Domain.APIs
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken );
    }
}
