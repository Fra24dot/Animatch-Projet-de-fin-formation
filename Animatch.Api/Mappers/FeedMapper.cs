using Animatch.Api.Dtos.Response;
using Animatch.Core.Models;

namespace Animatch.Api.Mappers
{
    public static class FeedMapper
    {
        public static DogFeedResponseDto ToResponseDto(this DogFeedModel model)
        {
            return new DogFeedResponseDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                RaceName = model.RaceName,
                Gender = model.Gender,
                AgeRange = model.AgeRange,
                Size = model.Size,
                EnergyLevel = model.EnergyLevel,
                MainImageUrl = model.MainImageUrl,
                ShelterName = model.ShelterName,
                DistanceInKm = model.DistanceInKm,
                Personalities = model.Personalities,
                Compatibilities = model.Compatibilities,
                SpecialNeeds = model.SpecialNeeds,
                MedicalHistories = model.MedicalHistories
            };
        }
    }
}
