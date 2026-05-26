using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Domain.Entities;
using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Repositories.Data
{
    internal class ShelterRepository(AnimatchDbContext animatchDbContext) : IShelterRepository
    {
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
