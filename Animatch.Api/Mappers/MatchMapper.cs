using Animatch.Api.Dtos.Request;

namespace Animatch.Api.Mappers
{
    public static class MatchMapper
    {
        public static (Guid DogId, bool IsLike) ToCore(this DogInteractionRequestDto dto)
        {
            return (dto.DogId, dto.IsLike);
        }
    }
}
