using Animatch.Api.Dtos.Request;
using Animatch.Api.Dtos.Response;
using Animatch.Api.Mappers;
using Animatch.Core.Interfaces.Services.Auth;
using Animatch.Core.Interfaces.Services.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Animatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController (
    IAuthService authService,
    IJwtService jwtService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                // Appel du service (qui vérifie les identifiants et le statut du shelter)
                var user = await authService.LoginAsync(dto.Email, dto.Password);

                // Génération du token si tout est OK
                var token = jwtService.GenerateToken(user);

                
                return Ok(user.ToLoginResponseDto(token));
            }
            catch (UnauthorizedAccessException ex)
            {
                // Renvoie un 401 si le mot de passe ou l'email est faux
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                //  Renvoie un 400 avec le message personnalisé pour le shelter non validé !
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDto dto)
        {
            var user = await authService.RegisterUserAsync(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.Password,
                dto.Gender,
                dto.BirthDate);

            return CreatedAtAction(nameof(RegisterUser), user.ToRegisterResponseDto());
        }

        [HttpPost("register/shelter")]
        public async Task<IActionResult> RegisterShelter([FromBody] RegisterShelterRequestDto dto)
        {
            var shelter = await authService.RegisterShelterAsync(
                dto.Name,
                dto.Email,
                dto.Password,
                dto.CompanyNumber,
                dto.PhoneNumber,
                dto.Address,
                dto.City,
                dto.PostalCode,
                dto.CreationYear);

            return CreatedAtAction(nameof(RegisterShelter), shelter.ToRegisterResponseDto());
        }
    }
}
