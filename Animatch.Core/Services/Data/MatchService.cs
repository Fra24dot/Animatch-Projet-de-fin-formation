using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Services.Data
{
    public class MatchService(IMatchRepository matchRepository) : IMatchService
    {
        public async Task RegisterSwipeAsync(Guid userId, Guid dogId, bool isLike)
        {
            
            var newMatch = new Match
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DogId = dogId,
                Status = isLike ? MatchStatus.Pending : MatchStatus.Refused,
                AdopterLikedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                ConversationEnabled = false
            };

            
            await matchRepository.AddAsync(newMatch);
            await matchRepository.SaveChangesAsync();
        }
    }
}
