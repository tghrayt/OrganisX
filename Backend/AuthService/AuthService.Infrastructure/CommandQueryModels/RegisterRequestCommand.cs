using MediatR;
using Shared.AuthService.DTOs;

namespace AuthService.Infrastructure.CommandQueryModels
{
    public sealed class RegisterRequestCommand : RegisterRequest, IRequest<RegisterResponse>
    {
        public RegisterRequestCommand(RegisterRequest request) : base(request.Email, request.Password, request.FirstName, request.LastName, request.Local, request.TimeZone)
        {
        }
    }
}
