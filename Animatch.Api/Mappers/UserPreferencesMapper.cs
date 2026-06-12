using Animatch.Api.Dtos.Response;
using Animatch.Core.Models;

namespace Animatch.Api.Mappers
{
    public static class UserPreferencesMapper
    {
        public static UserPreferencesResponseDto ToResponseDto(this UserPreferencesModel model)
        {
            return new UserPreferencesResponseDto
            {
                MaxDistance = model.MaxDistance,
                DogSizeIds = model.DogSizeIds,
                DogGenderIds = model.DogGenderIds,
                DogAgeIds = model.DogAgeIds,
                EnergyLevelIds = model.EnergyLevelIds,
                DogRaceIds = model.DogRaceIds
            };
        }
    }
}
