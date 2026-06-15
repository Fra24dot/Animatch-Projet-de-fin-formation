namespace Animatch.Api.Dtos.Response
{
    public class ShelterIncomingLikeResponseDto
    {
        public Guid MatchId { get; set; }
        public Guid DogId { get; set; }
        public string DogName { get; set; } = null!;
        public string? DogImageUrl { get; set; }
        public Guid AdopterId { get; set; }
        public string AdopterFirstName { get; set; } = null!;
        public string AdopterLastName { get; set; } = null!;
        public DateTime LikedAt { get; set; }
    }
}
