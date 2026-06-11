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
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;
            if (identity == null || !identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "L'utilisateur n'est pas reconnu authentifié par .NET", claimsCount = User.Claims.Count() });
            }

            // 2. Vérification du type de compte
            var accountType = User.FindFirst("accountType")?.Value;
            if (!string.Equals(accountType, "Shelter", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }

            // 3. Extraction de la Claim 'sub'
            var sub = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
          ?? User.FindFirst("sub")?.Value;

            if (sub == null)
            {
                return Unauthorized(new { message = "Token valide mais la claim 'sub' est introuvable.", disponible = User.Claims.Select(c => c.Type).ToList() });
            }

            // 🌟 LA CORRECTION ICI : On parse la chaîne 'sub' en Guid 'shelterId' pour le Mapper
            if (!Guid.TryParse(sub, out Guid shelterId))
            {
                return Unauthorized(new { message = "L'identifiant 'sub' du token n'est pas un GUID valide.", valeurRecue = sub });
            }

            try
            {
                // On passe maintenant le shelterId correctement extrait et typé !
                var (dogEntity, imageBlob, fileName) = await DogMapper.ToEntityAsync(dto, shelterId);

                var createdDog = await _shelterDogService.AddDogAsync(
                    dogEntity,
                    imageBlob,
                    fileName,
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
                var fullMessage = ex.InnerException != null ? $"{ex.Message} -> INNER: {ex.InnerException.Message}" : ex.Message;
                // 🌟 On ajoute l'ID extrait dans la réponse pour pouvoir le copier/coller
                return BadRequest(new
                {
                    message = "Erreur lors de la création du chien.",
                    details = fullMessage,
                    idShelterTente = shelterId // 👈 Regarde cette valeur dans l'onglet Network d'Angular
                });
            }

        }
    }
}
