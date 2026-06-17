using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Repositories.Data
{
    public interface IMatchRepository
    {
        Task AddAsync(Match match);
        Task SaveChangesAsync();
        Task<List<Match>> GetMatchesByAdopterIdAsync(Guid userId);
        Task<List<Match>> GetIncomingLikesByShelterIdAsync(Guid shelterId);
        Task<Match?> GetByIdAsync(Guid matchId);
    }
}
