using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Animatch.Core.Services.Data
{
    public class FeedService(
    IDogRepository dogRepository,
    IUserPreferencesRepository prefRepository,
    IUserRepository userRepository,
    IUserProfileRepository profileRepository) : IFeedService
    {
        public async Task<List<DogFeedModel>> GetUserFeedAsync(Guid userId)
        {

            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("USER_NOT_FOUND");

            if (!user.AccountCompleted)
            {
                throw new InvalidOperationException("PROFILE_INCOMPLETE");
            }

            var familyCondition = await profileRepository.GetFamilyConditionByUserIdAsync(userId);
            if (familyCondition == null || familyCondition.Latitude == null || familyCondition.Longitude == null)
                throw new InvalidOperationException("USER_COORDINATES_MISSING");

            var preferences = await prefRepository.GetPreferencesByUserIdAsync(userId);
            if (preferences == null || preferences.MaxDistance == 0)
                throw new InvalidOperationException("PREFERENCES_MISSING");

           
            var matchingData = await dogRepository.GetDogsByPreferencesAsync(
                userId,
                preferences,
                familyCondition.Latitude.Value,
                familyCondition.Longitude.Value);

           
            var resultList = matchingData?.ToList() ?? new();

            
            return resultList.Select(item =>
            {
                
                var dog = item.Dog;
                var distance = item.Distance;

                return new DogFeedModel
                {
                    Id = dog.Id,
                    Name = dog.Name,
                    Description = dog.Description,
                    RaceName = dog.Race != null ? dog.Race.ToString() : "Race inconnue",
                    Gender = dog.Gender.ToString(),
                    AgeRange = dog.AgeRange.ToString(),
                    Size = dog.Size.ToString(),
                    EnergyLevel = dog.EnergyLevelEnum.ToString(),
                    ShelterName = dog.Shelter?.Name ?? "Refuge partenaire",
                    DistanceInKm = Math.Round(distance, 1),
                    MainImageUrl = dog.DogMedias?.FirstOrDefault(m => m.Media != null)?.Media?.Url,
                    Personalities = dog.DogPersonalities?.Where(p => p.Personality != null).Select(p => p.Personality.Name).ToList() ?? new(),
                    Compatibilities = dog.DogCompatibilities?.Where(c => c.Compatibility != null).Select(c => c.Compatibility.Name).ToList() ?? new(),
                    SpecialNeeds = dog.DogSpecialNeeds?.Where(s => s.SpecialNeeds != null).Select(s => s.SpecialNeeds.Name).ToList() ?? new(),
                    MedicalHistories = dog.DogMedicalHistories?.Where(m => m.MedicalHistory != null).Select(m => m.MedicalHistory.Name).ToList() ?? new()
                };
            }).ToList();
        }

    }
}
