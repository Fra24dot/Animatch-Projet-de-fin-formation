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

        // GET TABLES BY USER ID
        public async Task<UserFamilyCondition?> GetFamilyConditionByUserIdAsync(Guid userId)
        => await animatchDbContext.UserFamilyConditions.FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<UserExperience?> GetExperienceByUserIdAsync(Guid userId)
            => await animatchDbContext.UserExperiences.FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<UserLifestyle?> GetLifestyleByUserIdAsync(Guid userId)
            => await animatchDbContext.UserLifestyles.FirstOrDefaultAsync(x => x.UserId == userId);



        // SAVE OR UPDATE TABLES
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

        public async Task CheckAndUpdateUserCompletionStatusAsync(Guid userId)
        {
            // On vérifie si l'utilisateur a rempli les 3 sections
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
