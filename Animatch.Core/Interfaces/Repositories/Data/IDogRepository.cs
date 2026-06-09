using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Animatch.Core.Interfaces.Repositories.Data
{
    public interface IDogRepository
    {
        Task<Dog?> GetByIdAsync(Guid id);
        Task<IEnumerable<Dog>> GetDogsByShelterIdAsync(Guid shelterId);
        Task AddAsync(Dog dog);
        Task UpdateAsync(Dog dog);
        Task DeleteAsync(Guid id);

        Task<bool> ExistsAsync(Expression<Func<Dog, bool>> predicate);
    }
}
