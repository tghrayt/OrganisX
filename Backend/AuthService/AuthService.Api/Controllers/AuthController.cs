using AuthService.Domain.APIs;
using Microsoft.AspNetCore.Mvc;
using Shared.AuthService.DTOs;

namespace AuthService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        /// <summary>
        /// Enregistre un nouvel utilisateur.
        /// </summary>
        /// <param name="request">Les informations d'inscription de l'utilisateur.</param>
        /// <returns>Le résultat de l'inscription.</returns>
        /// <response code="200">Inscription réussie.</response>
        /// <response code="400">Requête invalide.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }
    }
}
