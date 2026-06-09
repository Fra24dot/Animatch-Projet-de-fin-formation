using Animatch.Api.Dtos.Request;
using Animatch.Api.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Animatch.Core.Interfaces.Services.Data;

namespace Animatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelterDogController(IShelterDogService _shelterDogService) : ControllerBase
    {
        [HttpPost("add-dog")]
        public async Task<IActionResult> AddDog([FromForm] AddDogRequestDto dto) 
        {
            var accountType = User.FindFirst("accountType")?.Value;
            if (!string.Equals(accountType, "Shelter", StringComparison.OrdinalIgnoreCase)) return Forbid();

            var nameIdentifierClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (nameIdentifierClaim == null || !Guid.TryParse(nameIdentifierClaim, out Guid shelterId)) return Unauthorized();

            try
            {
                
                var (dogEntity, imageBlob, fileName) = await DogMapper.ToEntityAsync(dto, shelterId);

                var createdDog = await _shelterDogService.AddDogAsync(
                    dogEntity,
                    imageBlob,
                    fileName, // Envoi à Azure via le Core
                    dto.PersonalityIds,
                    dto.SpecialNeedsIds,
                    dto.CompatibilityIds,
                    dto.MedicalHistoryIds
                );

                var responseDto = createdDog.ToDetailResponseDto();

                return Ok(new { message = $"{responseDto.Name} a bien été ajouté !", dog = responseDto });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la création du chien.", details = ex.Message });
            }
        }
    }
}
