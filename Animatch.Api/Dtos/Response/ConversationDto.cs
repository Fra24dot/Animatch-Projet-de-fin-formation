namespace Animatch.Api.Dtos.Response
{
    public class ConversationDto
    {
        public Guid MatchId { get; set; }
        public Guid DogId { get; set; }
        public string DogName { get; set; } = null!;
        public string? DogPictureUrl { get; set; } 
        public string InterlocutorName { get; set; } = null!; 
        public string? LastMessageContent { get; set; }
        public DateTime? LastMessageCreatedAt { get; set; }
        public bool IsLastMessageRead { get; set; }
    }
}
