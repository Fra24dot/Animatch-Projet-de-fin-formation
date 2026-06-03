using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Services.Data
{
    public class AdminService(IShelterRepository _shelterRepository) : IAdminService
    {
        public async Task<IEnumerable<Shelter>> GetPendingSheltersAsync()
        {
            return await _shelterRepository.GetPendingSheltersAsync();
        }

        public async Task<bool> ApproveShelterAsync(Guid shelterId)
        {
            var shelter = await _shelterRepository.GetByIdAsync(shelterId);
            if (shelter == null) return false;

            // L'admin valide le refuge
            shelter.IsVerified = true;
            shelter.VerifiedAt = DateTime.UtcNow;

            
            shelter.IsActive = true;

            shelter.UpdatedAt = DateTime.UtcNow;

            await _shelterRepository.UpdateAsync(shelter);
            return true;
        }
    }
}
