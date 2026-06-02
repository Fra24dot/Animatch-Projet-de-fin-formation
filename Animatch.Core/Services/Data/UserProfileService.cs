using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Services.Data
{
    public class UserProfileService(IUserProfileRepository _profileRepository) : IUserProfileService
    {
        public async Task<(UserFamilyCondition? Family, UserExperience? Experience, UserLifestyle? Lifestyle)> GetFullProfileEntitiesAsync(Guid userId)
        {
            var family = await _profileRepository.GetFamilyConditionByUserIdAsync(userId);
            var experience = await _profileRepository.GetExperienceByUserIdAsync(userId);
            var lifestyle = await _profileRepository.GetLifestyleByUserIdAsync(userId);

            return (family, experience, lifestyle);
        }

        public async Task<bool> SaveFullProfileAsync(Guid userId, UserFamilyCondition family, UserExperience experience, UserLifestyle lifestyle)
        {
            
            family.UserId = userId;
            experience.UserId = userId;
            lifestyle.UserId = userId;

            await _profileRepository.SaveFamilyConditionAsync(family);
            await _profileRepository.SaveExperienceAsync(experience);
            await _profileRepository.SaveLifestyleAsync(lifestyle);

            return true;
        }
    }
}
