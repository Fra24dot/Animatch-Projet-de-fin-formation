using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Repositories.Data
{
    public class MatchRepository(AnimatchDbContext context) : IMatchRepository
    {
        public async Task AddAsync(Match match)
        {
            await context.Matches.AddAsync(match);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<Match>> GetMatchesByAdopterIdAsync(Guid userId)
        {
            return await context.Matches
            .Include(m => m.Dog)
                .ThenInclude(d => d.DogMedias)  
                    .ThenInclude(dm => dm.Media) 
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
        }

        public async Task<List<Match>> GetIncomingLikesByShelterIdAsync(Guid shelterId)
        {

            return await context.Matches
             .Include(m => m.User) 
             .Include(m => m.Dog)  
                 .ThenInclude(d => d.DogMedias)  
                     .ThenInclude(dm => dm.Media) 
             .Where(m => m.Dog.ShelterId == shelterId && m.Status == MatchStatus.Pending)
             .OrderByDescending(m => m.CreatedAt)
             .ToListAsync();
        }

        public async Task<Match?> GetByIdAsync(Guid matchId)
        {
            return await context.Matches.FirstOrDefaultAsync(m => m.Id == matchId);
        }
    }
}
