namespace Animatch.Api.Dtos.Request
{
    public class DogInteractionRequestDto
    {
        public Guid DogId { get; set; }
        public bool IsLike { get; set; }
    }
}
