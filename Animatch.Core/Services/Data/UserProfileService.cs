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
        /// <summary>
        /// Retrieves all profile-related information for a user,
        /// including family condition, experience, and lifestyle data.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// A tuple containing the user's family condition, experience,
        /// and lifestyle information.
        /// </returns>
        public async Task<(UserFamilyCondition? Family, UserExperience? Experience, UserLifestyle? Lifestyle)> GetFullProfileEntitiesAsync(Guid userId)
        {
            var family = await _profileRepository.GetFamilyConditionByUserIdAsync(userId);
            var experience = await _profileRepository.GetExperienceByUserIdAsync(userId);
            var lifestyle = await _profileRepository.GetLifestyleByUserIdAsync(userId);

            return (family, experience, lifestyle);
        }

        /// <summary>
        /// Saves all profile sections for a user, including family condition,
        /// experience, and lifestyle information.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="family">The user's family condition information.</param>
        /// <param name="experience">The user's experience information.</param>
        /// <param name="lifestyle">The user's lifestyle information.</param>
        /// <returns>
        /// True when all profile sections have been successfully saved.
        /// </returns>

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
