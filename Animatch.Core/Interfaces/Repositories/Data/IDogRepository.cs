using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Animatch.Core.Models;

namespace Animatch.Core.Interfaces.Repositories.Data
{
    public interface IDogRepository
    {
        Task<Dog?> GetByIdAsync(Guid id);
        Task<IEnumerable<Dog>> GetDogsByShelterIdAsync(Guid shelterId);
        Task AddAsync(Dog dog);
        Task UpdateAsync(Dog dog);
        Task DeleteAsync(Guid id);

        Task<List<(Dog Dog, double Distance)>> GetDogsByPreferencesAsync(Guid userId, UserPreferencesModel pref,
            double userLat, double userLng);
        Task<bool> ExistsAsync(Expression<Func<Dog, bool>> predicate);
    }
}
