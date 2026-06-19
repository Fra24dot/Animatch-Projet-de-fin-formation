using Animatch.Core.Interfaces.Repositories.Tools;
using Animatch.Core.Models;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Repositories.Tools
{
    public class MessageRepository(AnimatchDbContext _context) : IMessageRepository
    {
        public async Task<List<ConversationModel>> GetConversationsForUserAsync(Guid userId)
        {
            return await _context.Matches
                .Where(m => m.UserId == userId && m.Status == MatchStatus.Accepted)
                .Select(m => new ConversationModel
                {
                    MatchId = m.Id,
                    DogId = m.DogId,
                    DogName = m.Dog.Name,
                    InterlocutorName = m.Dog.Shelter.Name, 
                    LastMessageContent = _context.Messages
                        .Where(msg => msg.MatchId == m.Id)
                        .OrderByDescending(msg => msg.CreatedAt)
                        .Select(msg => msg.Content)
                        .FirstOrDefault(),
                    LastMessageCreatedAt = _context.Messages
                        .Where(msg => msg.MatchId == m.Id)
                        .OrderByDescending(msg => msg.CreatedAt)
                        .Select(msg => (DateTime?)msg.CreatedAt)
                        .FirstOrDefault()
                })
                .OrderByDescending(c => c.LastMessageCreatedAt ?? DateTime.MinValue) 
                .ToListAsync();
        }

        
        public async Task<List<ConversationModel>> GetConversationsForShelterAsync(Guid shelterId)
        {
            return await _context.Matches
                .Where(m => m.Dog.ShelterId == shelterId && m.Status == MatchStatus.Accepted)
                .Select(m => new ConversationModel
                {
                    MatchId = m.Id,
                    DogId = m.DogId,
                    DogName = m.Dog.Name,
                    InterlocutorName = m.User.FirstName + " " + m.User.LastName, 
                    LastMessageContent = _context.Messages
                        .Where(msg => msg.MatchId == m.Id)
                        .OrderByDescending(msg => msg.CreatedAt)
                        .Select(msg => msg.Content)
                        .FirstOrDefault(),
                    LastMessageCreatedAt = _context.Messages
                        .Where(msg => msg.MatchId == m.Id)
                        .OrderByDescending(msg => msg.CreatedAt)
                        .Select(msg => (DateTime?)msg.CreatedAt)
                        .FirstOrDefault()
                })
                .OrderByDescending(c => c.LastMessageCreatedAt ?? DateTime.MinValue)
                .ToListAsync();
        }

        
        public async Task<List<Message>> GetMessageHistoryAsync(Guid matchId)
        {
            return await _context.Messages
                .Where(msg => msg.MatchId == matchId)
                .OrderBy(msg => msg.CreatedAt) 
                .ToListAsync();
        }

        public async Task<Match?> GetMatchWithDetailsAsync(Guid matchId)
        {
            return await _context.Matches
                .Include(m => m.Dog) 
                .FirstOrDefaultAsync(m => m.Id == matchId);
        }

        public async Task AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
