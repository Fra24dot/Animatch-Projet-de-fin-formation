using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Models;
using Animatch.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Repositories.Data
{
    public class UserPreferencesRepository(AnimatchDbContext animatchDbContext) : IUserPreferencesRepository
    {
        public Task<UserPreferencesModel?> GetPreferencesByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task SavePreferencesAsync(Guid userId, int maxDistance, List<int> sizeIds, List<int> genderIds, List<int> ageIds, List<int> energyIds, List<int> raceIds)
        {
            throw new NotImplementedException();
        }
    }
}
