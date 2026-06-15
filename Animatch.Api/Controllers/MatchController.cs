using Animatch.Api.Dtos.Request;
using Animatch.Api.Dtos.Response;
using Animatch.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Animatch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController(IMatchService matchService) : ControllerBase
    {
        [HttpGet("my-likes")]
        public async Task<IActionResult> GetMyLikes()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId)) return Unauthorized();

            var matches = await matchService.GetAdopterMatchesAsync(userId);

            var response = matches.Select(m => new AdopterMatchResponseDto
            {
                MatchId = m.Id,
                DogId = m.DogId,
                DogName = m.Dog.Name,  
                DogImageUrl = m.Dog.DogMedias?.FirstOrDefault()?.Media?.Url,
                Status = m.Status.ToString(),
                CreatedAt = m.CreatedAt
            }).ToList();

            return Ok(response);
        }
        [HttpGet("shelter-incoming")]
        public async Task<IActionResult> GetShelterIncoming()
        {
            
            var shelterIdClaim = User.FindFirst("ShelterId")?.Value ?? User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(shelterIdClaim, out Guid shelterId)) return Unauthorized();

            var incomingLikes = await matchService.GetShelterIncomingLikesAsync(shelterId);

            var response = incomingLikes.Select(m => new ShelterIncomingLikeResponseDto
            {
                MatchId = m.Id,
                DogId = m.DogId,
                DogName = m.Dog.Name,
                DogImageUrl = m.Dog.DogMedias?.FirstOrDefault()?.Media?.Url,
                AdopterId = m.UserId,
                AdopterFirstName = m.User.FirstName ?? "Adoptant",
                AdopterLastName = m.User.LastName ?? "Anonyme",
                LikedAt = m.CreatedAt
            }).ToList();

            return Ok(response);
        }

        
        [HttpPut("{id}/decision")]
        public async Task<IActionResult> EvaluateMatch(Guid id, [FromBody] UpdateMatchStatusRequestDto dto)
        {
            var result = await matchService.UpdateMatchStatusAsync(id, dto.Approve);
            if (!result) return NotFound(new { message = "Demande de match introuvable." });

            return Ok(new { message = dto.Approve ? "Félicitations, le match est validé ! Le chat est ouvert. 🎉" : "Demande refusée." });
        }
    }
}
