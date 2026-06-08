using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
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
            shelter.ShelterStatus = ShelterStatus.Approved; // Statut mis à jour

            shelter.IsActive = true;
            shelter.UpdatedAt = DateTime.UtcNow;

            await _shelterRepository.UpdateAsync(shelter);
            return true;
        }

        public async Task<bool> RejectShelterAsync(Guid shelterId)
        {
            var shelter = await _shelterRepository.GetByIdAsync(shelterId);
            if (shelter == null) return false;

            // L'admin refuse le refuge
            shelter.IsVerified = false;
            shelter.VerifiedAt = null; // Il n'est pas vérifié
            shelter.ShelterStatus = ShelterStatus.Rejected; // Statut mis à jour !

            shelter.IsActive = false; // Compte désactivé, il ne pourra pas se connecter
            shelter.UpdatedAt = DateTime.UtcNow;

            await _shelterRepository.UpdateAsync(shelter);
            return true;
        }
    }
}
