using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Domain.Entities;
using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Repositories.Data
{
    public class ShelterRepository(AnimatchDbContext animatchDbContext) : IShelterRepository
    {
        /// <summary>
        /// Retrieves all shelters that are awaiting verification.
        /// </summary>
        /// <returns>
        /// A collection of shelters that have not yet been verified.
        /// </returns>
        public async Task<IEnumerable<Shelter>> GetPendingSheltersAsync()
        {
            
            return await animatchDbContext.Shelters
                .Where(s => !s.IsVerified)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a shelter by its unique identifier.
        /// </summary>
        /// <param name="id">The shelter identifier.</param>
        /// <returns>
        /// The matching shelter if found; otherwise, null.
        /// </returns>
        public async Task<Shelter?> GetByIdAsync(Guid id)
        {
            return await animatchDbContext.Shelters.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing shelter in the database.
        /// </summary>
        /// <param name="shelter">The shelter to update.</param>
        /// <returns>
        /// The updated shelter.
        /// </returns>
        public async Task<Shelter> UpdateAsync(Shelter shelter)
        {
            animatchDbContext.Shelters.Update(shelter);
            await animatchDbContext.SaveChangesAsync();
            return shelter;
        }

        /// <summary>
        /// Creates a new shelter in the database.
        /// </summary>
        /// <param name="shelter">The shelter to create.</param>
        /// <returns>
        /// The created shelter.
        /// </returns>
        public async Task<Shelter?> CreateAsync(Shelter shelter)
        {
            if (shelter is null) return null;
            animatchDbContext.Shelters.Add(shelter);
            await animatchDbContext.SaveChangesAsync();
            return shelter;
        }

        /// <summary>
        /// Checks whether a shelter account already exists for the specified email address.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>
        /// True if a shelter with the specified email exists; otherwise, false.
        /// </returns>
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await animatchDbContext.Shelters
            .AnyAsync(s => s.Email == email);
        }

        /// <summary>
        /// Retrieves a shelter by its email address.
        /// </summary>
        /// <param name="email">The shelter email address.</param>
        /// <returns>
        /// The matching shelter if found; otherwise, null.
        /// </returns>
        public async Task<Shelter?> GetByEmailAsync(string email)
        {
            return await animatchDbContext.Shelters
            .FirstOrDefaultAsync(s => s.Email == email);
        }
    }
}
