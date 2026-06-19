using Animatch.Api.Dtos.Response;
using Animatch.Api.Mappers;
using Animatch.Core.Interfaces.Repositories.Tools;
using Animatch.Infrastructure.Repositories.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Animatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(IMessageRepository messageRepository) : ControllerBase
    {
        /// <summary>
        /// Récupère la liste des conversations d'un adoptant (User)
        /// </summary>
        [HttpGet("conversations/user/{userId}")]
        public async Task<ActionResult<List<ConversationDto>>> GetUserConversations(Guid userId)
        {
            var models = await messageRepository.GetConversationsForUserAsync(userId);

            
            var dtos = models.ToDtoList();

            return Ok(dtos);
        }

        /// <summary>
        /// Récupère la liste des conversations d'un refuge (Shelter)
        /// </summary>
        [HttpGet("conversations/shelter/{shelterId}")]
        public async Task<ActionResult<List<ConversationDto>>> GetShelterConversations(Guid shelterId)
        {
            var models = await messageRepository.GetConversationsForShelterAsync(shelterId);

            
            var dtos = models.ToDtoList();

            return Ok(dtos);
        }

        /// <summary>
        /// Récupère l'historique des messages pour un match spécifique
        /// </summary>
        /// <param name="matchId">L'ID du match/conversation</param>
        /// <param name="currentUserId">L'ID de l'utilisateur connecté (pour déterminer le IsFromUser)</param>
        [HttpGet("history/{matchId}")]
        public async Task<ActionResult<List<MessageDto>>> GetMessageHistory(Guid matchId, [FromQuery] Guid currentUserId)
        {
            var messages = await messageRepository.GetMessageHistoryAsync(matchId);

            
            var dtos = messages.ToDtoList(currentUserId);

            return Ok(dtos);
        }
    }
}
}
