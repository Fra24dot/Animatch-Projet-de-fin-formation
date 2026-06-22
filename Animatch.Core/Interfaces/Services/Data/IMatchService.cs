using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Data
{
    public interface IMatchService
    {
        Task<Guid> RegisterSwipeAsync(Guid userId, Guid dogId, bool isLike);
        Task<List<Match>> GetAdopterMatchesAsync(Guid userId);
        Task<List<Match>> GetShelterIncomingLikesAsync(Guid shelterId);
        Task<bool> UpdateMatchStatusAsync(Guid matchId, bool approve);

    }
}
