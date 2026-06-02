using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Data
{
    public interface IUserProfileService
    {
        Task<(UserFamilyCondition? Family, UserExperience? Experience, UserLifestyle? Lifestyle)> GetFullProfileEntitiesAsync(Guid userId);

        
        Task<bool> SaveFullProfileAsync(Guid userId, UserFamilyCondition family, UserExperience experience, UserLifestyle lifestyle);
    }
}
