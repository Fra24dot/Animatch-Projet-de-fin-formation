using Animatch.Api.Mappers;
using Animatch.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Animatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController(IAdminService _adminService) : ControllerBase
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
    }
}
