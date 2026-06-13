using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Domain.Entities;
using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Repositories.Data
{
    public class UserProfileRepository(AnimatchDbContext animatchDbContext) : IUserProfileRepository
    {

        /// <summary>
        /// Retrieves the family condition information associated with a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// The user's family condition if found; otherwise, null.
        /// </returns>
        public async Task<UserFamilyCondition?> GetFamilyConditionByUserIdAsync(Guid userId)
        => await animatchDbContext.UserFamilyConditions.FirstOrDefaultAsync(x => x.UserId == userId);

        /// <summary>
        /// Retrieves the experience information associated with a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// The user's experience information if found; otherwise, null.
        /// </returns>
        public async Task<UserExperience?> GetExperienceByUserIdAsync(Guid userId)
            => await animatchDbContext.UserExperiences.FirstOrDefaultAsync(x => x.UserId == userId);

        /// <summary>
        /// Retrieves the lifestyle information associated with a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// The user's lifestyle information if found; otherwise, null.
        /// </returns>
        public async Task<UserLifestyle?> GetLifestyleByUserIdAsync(Guid userId)
            => await animatchDbContext.UserLifestyles.FirstOrDefaultAsync(x => x.UserId == userId);



        /// <summary>
        /// Creates or updates a user's family condition information.
        /// </summary>
        /// <param name="familyCondition">The family condition data to save.</param>
        /// <returns>
        /// The saved family condition entity.
        /// </returns>
        public async Task<UserFamilyCondition> SaveFamilyConditionAsync(UserFamilyCondition familyCondition)
        {
            var existing = await GetFamilyConditionByUserIdAsync(familyCondition.UserId);
            if (existing == null)
            {
                familyCondition.CreatedAt = DateTime.UtcNow;
                await animatchDbContext.UserFamilyConditions.AddAsync(familyCondition);
            }
            else
            {
                animatchDbContext.Entry(existing).CurrentValues.SetValues(familyCondition);
                existing.UpdatedAt = DateTime.UtcNow;
            }

            await animatchDbContext.SaveChangesAsync();
            await CheckAndUpdateUserCompletionStatusAsync(familyCondition.UserId);
            return existing ?? familyCondition;
        }

        /// <summary>
        /// Creates or updates a user's experience information.
        /// </summary>
        /// <param name="experience">The experience data to save.</param>
        /// <returns>
        /// The saved experience entity.
        /// </returns>
        public async Task<UserExperience> SaveExperienceAsync(UserExperience experience)
        {
            var existing = await GetExperienceByUserIdAsync(experience.UserId);
            if (existing == null)
            {
                experience.CreatedAt = DateTime.UtcNow;
                await animatchDbContext.UserExperiences.AddAsync(experience);
            }
            else
            {
                animatchDbContext.Entry(existing).CurrentValues.SetValues(experience);
                existing.UpdatedAt = DateTime.UtcNow;
            }

            await animatchDbContext.SaveChangesAsync();
            await CheckAndUpdateUserCompletionStatusAsync(experience.UserId);
            return existing ?? experience;
        }

        /// <summary>
        /// Creates or updates a user's lifestyle information.
        /// </summary>
        /// <param name="lifestyle">The lifestyle data to save.</param>
        /// <returns>
        /// The saved lifestyle entity.
        /// </returns>
        public async Task<UserLifestyle> SaveLifestyleAsync(UserLifestyle lifestyle)
        {
            var existing = await GetLifestyleByUserIdAsync(lifestyle.UserId);
            if (existing == null)
            {
                lifestyle.CreatedAt = DateTime.UtcNow;
                await animatchDbContext.UserLifestyles.AddAsync(lifestyle);
            }
            else
            {
                animatchDbContext.Entry(existing).CurrentValues.SetValues(lifestyle);
                existing.UpdatedAt = DateTime.UtcNow;
            }

            await animatchDbContext.SaveChangesAsync();
            await CheckAndUpdateUserCompletionStatusAsync(lifestyle.UserId);
            return existing ?? lifestyle;
        }

        /// <summary>
        /// Verifies whether the user has completed all onboarding sections
        /// (family condition, experience, and lifestyle) and marks the account
        /// as completed when all required information is available.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        public async Task CheckAndUpdateUserCompletionStatusAsync(Guid userId)
        {
            
            var hasFamily = await animatchDbContext.UserFamilyConditions.AnyAsync(x => x.UserId == userId);
            var hasExperience = await animatchDbContext.UserExperiences.AnyAsync(x => x.UserId == userId);
            var hasLifestyle = await animatchDbContext.UserLifestyles.AnyAsync(x => x.UserId == userId);

            if (hasFamily && hasExperience && hasLifestyle)
            {
                var user = await animatchDbContext.Users.FindAsync(userId);
                if (user != null && !user.AccountCompleted)
                {
                    user.AccountCompleted = true;
                    user.UpdatedAt = DateTime.UtcNow;
                    await animatchDbContext.SaveChangesAsync();
                }
            }
        }
    }
}
