using Animatch.Core.Models;
using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Repositories.Tools
{
    public interface IMessageRepository
    {
        Task<Match?> GetMatchWithDetailsAsync(Guid matchId);
        Task AddMessageAsync(Message message);
        Task SaveChangesAsync();
        Task<List<ConversationModel>> GetConversationsForUserAsync(Guid userId);
        Task<List<ConversationModel>> GetConversationsForShelterAsync(Guid shelterId);
        Task<List<Message>> GetMessageHistoryAsync(Guid matchId);
    }
}
