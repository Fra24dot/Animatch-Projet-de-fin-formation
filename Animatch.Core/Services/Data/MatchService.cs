using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Services.Data
{
    public class MatchService(IMatchRepository matchRepository) : IMatchService
    {
        public async Task<Guid> RegisterSwipeAsync(Guid userId, Guid dogId, bool isLike)
        {
            var matchId = Guid.NewGuid(); 

            var newMatch = new Match
            {
                Id = matchId,
                UserId = userId,
                DogId = dogId,
                Status = isLike ? MatchStatus.Pending : MatchStatus.Refused,
                AdopterLikedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                ConversationEnabled = false
            };

            await matchRepository.AddAsync(newMatch);
            await matchRepository.SaveChangesAsync();

            return matchId;  
        }
        public async Task<List<Match>> GetAdopterMatchesAsync(Guid userId)
        {
            return await matchRepository.GetMatchesByAdopterIdAsync(userId);
        }

        public async Task<List<Match>> GetShelterIncomingLikesAsync(Guid shelterId)
        {
            return await matchRepository.GetIncomingLikesByShelterIdAsync(shelterId);
        }

        public async Task<bool> UpdateMatchStatusAsync(Guid matchId, bool approve)
        {
            var match = await matchRepository.GetByIdAsync(matchId);
            if (match == null) return false;

            
            match.Status = approve ? MatchStatus.Accepted : MatchStatus.Refused;

            match.ShelterResponseAt = DateTime.UtcNow;

            
            if (approve)
            {
                match.ConversationEnabled = true;
            }

            await matchRepository.SaveChangesAsync();
            return true;
        }
    }
}
