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
        /// <summary>
        /// Retrieves all shelters that are currently awaiting administrative approval.
        /// </summary>
        /// <returns>
        /// A collection of shelters with a pending verification status.
        /// </returns>
        public async Task<IEnumerable<Shelter>> GetPendingSheltersAsync()
        {
            return await _shelterRepository.GetPendingSheltersAsync();
        }


        /// <summary>
        /// Approves a shelter and updates its status to approved.
        /// The shelter is marked as verified and activated.
        /// </summary>
        /// <param name="shelterId">The unique identifier of the shelter to approve.</param>
        /// <returns>
        /// True if the shelter was found and successfully approved; otherwise, false.
        /// </returns>
        public async Task<bool> ApproveShelterAsync(Guid shelterId)
        {
            var shelter = await _shelterRepository.GetByIdAsync(shelterId);
            if (shelter == null) return false;

            // L'admin valide le refuge
            shelter.IsVerified = true;
            shelter.VerifiedAt = DateTime.UtcNow;
            shelter.ShelterStatus = ShelterStatus.Approved; 

            shelter.IsActive = true;
            shelter.UpdatedAt = DateTime.UtcNow;

            await _shelterRepository.UpdateAsync(shelter);
            return true;
        }

        /// <summary>
        /// Rejects a shelter and updates its status to rejected.
        /// The shelter remains unverified and its account is deactivated.
        /// </summary>
        /// <param name="shelterId">The unique identifier of the shelter to reject.</param>
        /// <returns>
        /// True if the shelter was found and successfully rejected; otherwise, false.
        /// </returns>
        public async Task<bool> RejectShelterAsync(Guid shelterId)
        {
            var shelter = await _shelterRepository.GetByIdAsync(shelterId);
            if (shelter == null) return false;

            // L'admin refuse le refuge
            shelter.IsVerified = false;
            shelter.VerifiedAt = null; 
            shelter.ShelterStatus = ShelterStatus.Rejected; 

            shelter.IsActive = false; // Compte désactivé
            shelter.UpdatedAt = DateTime.UtcNow;

            await _shelterRepository.UpdateAsync(shelter);
            return true;
        }
    }
}
