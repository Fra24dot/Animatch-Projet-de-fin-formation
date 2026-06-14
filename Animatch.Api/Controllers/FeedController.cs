using Animatch.Api.Dtos.Request;
using Animatch.Api.Mappers;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Core.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Animatch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController(IFeedService feedService, IMatchService matchService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetFeed()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized(new { message = "Session invalide." });

            try
            {
                var feedModels = await feedService.GetUserFeedAsync(userId);
                var responseDtos = feedModels.Select(m => m.ToResponseDto()).ToList();
                return Ok(responseDtos);
            }
            catch (InvalidOperationException ex) when (ex.Message == "PROFILE_INCOMPLETE")
            {
               
                return StatusCode(403, new { code = "PROFILE_INCOMPLETE", message = "Veuillez compléter votre profil (Famille, Expérience, Style de vie) avant d'accéder au feed." });
            }
            catch (InvalidOperationException ex) when (ex.Message == "PREFERENCES_MISSING")
            {
                return StatusCode(403, new { code = "PREFERENCES_MISSING", message = "Veuillez configurer vos préférences de recherche avant d'accéder au feed." });
            }
            catch (InvalidOperationException ex) when (ex.Message == "USER_COORDINATES_MISSING")
            {
                return BadRequest(new { message = "Vos coordonnées de profil (Latitude/Longitude) sont manquantes." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors du chargement du feed.", details = ex.Message });
            }
        }

        [HttpPost("interaction")]
        public async Task<IActionResult> PostInteraction([FromBody] DogInteractionRequestDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized();

            try
            {
                
                await matchService.RegisterSwipeAsync(userId, dto.DogId, dto.IsLike);

                return Ok(new { message = dto.IsLike ? "Demande de match envoyée au refuge ! 🐾" : "Chien masqué avec succès." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Impossible d'enregistrer l'interaction.", details = ex.Message });
            }
        }
    }
}
