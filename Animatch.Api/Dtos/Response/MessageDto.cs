namespace Animatch.Api.Dtos.Response
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid ShelterId { get; set; }
        public Guid MatchId { get; set; }
        public bool IsFromUser { get; set; }
    }
}
