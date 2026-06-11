using Animatch.Api.Dtos.Request;
using Animatch.Api.Dtos.Response;
using Animatch.Api.Mappers;
using Animatch.Core.Interfaces.Services.Auth;
using Animatch.Core.Interfaces.Services.Tools;
using Animatch.Security.Services.Auth;
using Animatch.Security.Services.Tools;
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
                // 🌟 CORRECTION : On utilise 'authService' (sans le '_')
                var (user, shelterId) = await authService.LoginAsync(dto.Email, dto.Password);

                // 🌟 CORRECTION : On utilise 'jwtService' (sans le '_')
                var token = jwtService.GenerateToken(user, shelterId);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
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

            return CreatedAtAction(null, null, shelter.ToRegisterResponseDto());
        }
    }
}
