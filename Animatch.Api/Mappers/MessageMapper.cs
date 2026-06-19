using Animatch.Api.Dtos.Response;
using Animatch.Core.Models;
using Animatch.Domain.Entities;

namespace Animatch.Api.Mappers
{
    public static class MessageMapper
    {
        /// <summary>
        /// Convertit un modèle de conversation du Core en DTO pour l'API
        /// </summary>
        public static ConversationDto ToDto(this ConversationModel model)
        {
            return new ConversationDto
            {
                MatchId = model.MatchId,
                DogId = model.DogId,
                DogName = model.DogName,
                InterlocutorName = model.InterlocutorName,
                LastMessageContent = model.LastMessageContent,
                LastMessageCreatedAt = model.LastMessageCreatedAt,
                IsLastMessageRead = false,
                DogPictureUrl = null
            };
        }

        /// <summary>
        /// Convertit une liste de modèles en liste de DTOs
        /// </summary>
        public static List<ConversationDto> ToDtoList(this List<ConversationModel> models)
        {
            return models.Select(m => m.ToDto()).ToList();
        }

        /// <summary>
        /// Convertit une entité Message de la BDD en DTO pour l'historique du chat
        /// </summary>
        public static MessageDto ToDto(this Message message, Guid currentUserId)
        {
            return new MessageDto
            {
                Id = message.Id,
                Content = message.Content,
                IsRead = message.IsRead,
                CreatedAt = message.CreatedAt,
                UserId = message.UserId,
                ShelterId = message.ShelterId,
                MatchId = message.MatchId,
                IsFromUser = message.UserId == currentUserId
            };
        }

        /// <summary>
        /// Convertit une liste de messages BDD en liste de DTOs
        /// </summary>
        public static List<MessageDto> ToDtoList(this List<Message> messages, Guid currentUserId)
        {
            return messages.Select(m => m.ToDto(currentUserId)).ToList();
        }
    }
}
