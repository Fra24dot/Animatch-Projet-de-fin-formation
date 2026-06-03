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
        public async Task<IEnumerable<Shelter>> GetPendingSheltersAsync()
        {
            
            return await animatchDbContext.Shelters
                .Where(s => !s.IsVerified)
                .ToListAsync();
        }

        public async Task<Shelter?> GetByIdAsync(Guid id)
        {
            return await animatchDbContext.Shelters.FindAsync(id);
        }

        public async Task<Shelter> UpdateAsync(Shelter shelter)
        {
            animatchDbContext.Shelters.Update(shelter);
            await animatchDbContext.SaveChangesAsync();
            return shelter;
        }


        public async Task<Shelter> CreateAsync(Shelter shelter)
        {
            if (shelter is null) return null;
            animatchDbContext.Shelters.Add(shelter);
            await animatchDbContext.SaveChangesAsync();
            return shelter;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await animatchDbContext.Shelters
            .AnyAsync(s => s.Email == email);
        }

        public async Task<Shelter?> GetByEmailAsync(string email)
        {
            return await animatchDbContext.Shelters
            .FirstOrDefaultAsync(s => s.Email == email);
        }
    }
}
