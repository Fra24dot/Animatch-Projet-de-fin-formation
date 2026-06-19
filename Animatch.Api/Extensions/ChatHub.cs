using Animatch.Core.Interfaces.Repositories.Tools;
using Animatch.Domain.Entities;
using Animatch.Infrastructure.Repositories.Tools;
using Microsoft.AspNetCore.SignalR;

namespace Animatch.Api.Extensions
{
    public class ChatHub(IMessageRepository messageRepository) : Hub
    {
        public async Task JoinChat(string matchId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, matchId);
        }

       
        public async Task SendMessage(string matchId, string senderId, string messageText, bool isFromUser)
        {
            var matchGuid = Guid.Parse(matchId);
            var senderGuid = Guid.Parse(senderId);

            var match = await messageRepository.GetMatchWithDetailsAsync(matchGuid);
            if (match == null || !match.ConversationEnabled)
            {
                throw new HubException("La conversation n'est pas activée ou le match n'existe pas.");
            }

            
            var newMessage = new Message
            {
                Id = Guid.NewGuid(),
                Content = messageText,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                MatchId = matchGuid,
                UserId = match.UserId,
                ShelterId = match.Dog.ShelterId 
            };

            
            await messageRepository.AddMessageAsync(newMessage);
            await messageRepository.SaveChangesAsync();

            
            await Clients.Group(matchId).SendAsync("ReceiveMessage", new
            {
                Id = newMessage.Id,
                Content = newMessage.Content,
                CreatedAt = newMessage.CreatedAt,
                SenderId = senderId,
                IsFromUser = isFromUser 
            });
        }
    }
}
