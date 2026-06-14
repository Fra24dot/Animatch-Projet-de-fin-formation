using Animatch.Api.Dtos.Request;
using Animatch.Api.Dtos.Response;
using Animatch.Api.Mappers;
using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Animatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController(IUserProfileService _profileService,
        IUserRepository _userRepository, IUserPreferencesRepository preferencesRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId)) return Unauthorized();

            var (family, experience, lifestyle) = await _profileService.GetFullProfileEntitiesAsync(userId);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return NotFound(new { message = "Utilisateur introuvable." });

            var responseDto = UserProfileMapper.ToResponse(userId, user.AccountCompleted, family, experience, lifestyle);
            return Ok(responseDto);
        }

        [HttpPut]
        public async Task<IActionResult> SaveProfile([FromBody] UpdateUserProfileRequest request)
        {
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId)) return Unauthorized();

            var familyEntity = request.FamilyCondition.ToEntity(userId);
            var experienceEntity = request.Experience.ToEntity(userId);
            var lifestyleEntity = request.Lifestyle.ToEntity(userId);

           
            var isSaved = await _profileService.SaveFullProfileAsync(userId, familyEntity, experienceEntity, lifestyleEntity);
            if (!isSaved) return BadRequest(new { message = "Erreur de sauvegarde." });

            
            return Ok(new { message = "Profil mis à jour avec succès !", accountCompleted = true });
        }

        [HttpGet("my-preferences")]
        public async Task<IActionResult> GetMyPreferences()
        {
            
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId)) return Unauthorized(new { message = "Session invalide." });

            try
            {
                var preferencesModel = await preferencesRepository.GetPreferencesByUserIdAsync(userId);

                if (preferencesModel == null)
                {
                    return Ok(new UserPreferencesResponseDto
                    {
                        MaxDistance = 50,
                        DogSizeIds = new(),
                        DogGenderIds = new(),
                        DogAgeIds = new(),
                        EnergyLevelIds = new(),
                        DogRaceIds = new()
                    });
                }

                var responseDto = preferencesModel.ToResponseDto();
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur de récupération des préférences.", details = ex.Message });
            }
        }

        [HttpPost("my-preferences")]
        public async Task<IActionResult> SaveMyPreferences([FromBody] SavePreferencesRequestDto dto)
        {
            
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId)) return Unauthorized(new { message = "Session invalide." });

            try
            {
                await preferencesRepository.SavePreferencesAsync(
                    userId,
                    dto.MaxDistance,
                    dto.DogSizeIds,
                    dto.DogGenderIds,
                    dto.DogAgeIds,
                    dto.EnergyLevelIds,
                    dto.DogRaceIds
                );

                return Ok(new { message = "Vos préférences de recherche ont bien été enregistrées !" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors de la sauvegarde des préférences.", details = ex.Message });
            }
        }
    }
}

