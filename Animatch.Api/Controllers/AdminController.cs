using Animatch.Api.Mappers;
using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Domain.Enums;
using Animatch.Infrastructure.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Animatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController(IAdminService _adminService, IShelterRepository _shelterRepository) : ControllerBase
    {
        [HttpGet("pending-shelters")]
        public async Task<IActionResult> GetPendingShelters()
        {
            
            var accountType = User.FindFirst("accountType")?.Value;
            if (accountType != "Admin")
            {
                return Forbid(); // Retourne un code 403 Interdit
            }

            var shelters = await _adminService.GetPendingSheltersAsync();

            // Mapping des entités vers le DTO de l'API
            var response = shelters.Select(s => s.ToPendingResponseDto());

            return Ok(response);
        }

        
        [HttpPut("approve-shelter/{id}")]
        public async Task<IActionResult> ApproveShelter(Guid id)
        {
            var accountType = User.FindFirst("accountType")?.Value;
            if (accountType != "Admin") return Forbid();

            var success = await _adminService.ApproveShelterAsync(id);
            if (!success) return NotFound(new { message = "Refuge introuvable." });

            return Ok(new { message = "Le refuge a été activé avec succès !" });
        }

        [HttpPost("reject-shelter/{id}")]
        public async Task<IActionResult> RejectShelter(Guid id) 
        {

            var accountType = User.FindFirst("accountType")?.Value;
            if (!string.Equals(accountType, "Admin", StringComparison.OrdinalIgnoreCase))
                return Forbid();

            try
            {
                
                var shelter = await _shelterRepository.GetByIdAsync(id);
                if (shelter == null)
                    return NotFound(new { message = "Refuge introuvable." });

                
                shelter.ShelterStatus = ShelterStatus.Rejected;
                await _shelterRepository.UpdateAsync(shelter);

                return Ok(new { message = $"Le refuge '{shelter.Name}' a été rejeté avec succès." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erreur lors du rejet du refuge.", details = ex.Message });
            }
        }
    }
}
