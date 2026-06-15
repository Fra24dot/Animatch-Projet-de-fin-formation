namespace Animatch.Api.Dtos.Response
{
    public class AdopterMatchResponseDto
    {
        public Guid MatchId { get; set; }
        public Guid DogId { get; set; }
        public string DogName { get; set; } = null!;
        public string? DogImageUrl { get; set; }
        public string Status { get; set; } = null!; 
        public DateTime CreatedAt { get; set; }
    }
}
