using Animatch.Api.Dtos.Request;
using Animatch.Api.Mappers;
using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Animatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController(IUserProfileService _profileService,
        IUserRepository _userRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId)) return Unauthorized();

            // Récupération des entités de profil depuis le service
            var (family, experience, lifestyle) = await _profileService.GetFullProfileEntitiesAsync(userId);

            // Récupération de l'utilisateur via ton Repository existant
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

            return Ok(new { message = "Profil mis à jour avec succès !" });
        }
    }
}

