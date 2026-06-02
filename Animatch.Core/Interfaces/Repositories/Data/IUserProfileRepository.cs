using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Repositories.Data
{
    public interface IUserProfileRepository
    {

            Task<UserFamilyCondition?> GetFamilyConditionByUserIdAsync(Guid userId);
            Task<UserExperience?> GetExperienceByUserIdAsync(Guid userId);
            Task<UserLifestyle?> GetLifestyleByUserIdAsync(Guid userId);

           
            Task<UserFamilyCondition> SaveFamilyConditionAsync(UserFamilyCondition familyCondition);
            Task<UserExperience> SaveExperienceAsync(UserExperience experience);
            Task<UserLifestyle> SaveLifestyleAsync(UserLifestyle lifestyle);

           
            Task CheckAndUpdateUserCompletionStatusAsync(Guid userId);
        }
    }

