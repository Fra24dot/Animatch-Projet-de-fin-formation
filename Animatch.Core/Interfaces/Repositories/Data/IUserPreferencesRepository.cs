using Animatch.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Repositories.Data
{
    public interface IUserPreferencesRepository
    {
        Task SavePreferencesAsync(Guid userId,int maxDistance,List<int> sizeIds,
        List<int> genderIds, List<int> ageIds, List<int> energyIds, List<int> raceIds);

        Task<UserPreferencesModel?> GetPreferencesByUserIdAsync(Guid userId);
    }
}
