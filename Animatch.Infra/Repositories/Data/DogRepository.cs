using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Domain.Entities;
using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace Animatch.Infrastructure.Repositories.Data
{
    public class DogRepository(AnimatchDbContext _context) : IDogRepository
    {
        /// <summary>
        /// Adds a new dog to the database.
        /// </summary>
        /// <param name="dog">The dog to add.</param>
        public async Task AddAsync(Dog dog)
        {
            await _context.Dogs.AddAsync(dog);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a dog from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the dog to delete.</param>
        public async Task DeleteAsync(Guid id)
        {
            var dog = await _context.Dogs.FindAsync(id);
            if (dog != null)
            {
                _context.Dogs.Remove(dog);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves a dog by its unique identifier, including its media,
        /// characteristics, medical history, and shelter information.
        /// </summary>
        /// <param name="id">The unique identifier of the dog.</param>
        /// <returns>
        /// The matching dog if found; otherwise, null.
        /// </returns>
        public async Task<Dog?> GetByIdAsync(Guid id)
        {
            return await _context.Dogs
             .Include(d => d.DogMedias)
             .Include(d => d.DogPersonalities)
                 .ThenInclude(dp => dp.Personality) 
             .Include(d => d.DogSpecialNeeds)
                 .ThenInclude(ds => ds.SpecialNeeds)
             .Include(d => d.DogCompatibilities)
                 .ThenInclude(dc => dc.Compatibility)
             .Include(d => d.DogMedicalHistories)
                 .ThenInclude(dm => dm.MedicalHistory)
             .Include(d => d.Shelter) 
             .FirstOrDefaultAsync(d => d.Id == id);
        }

        /// <summary>
        /// Retrieves all dogs belonging to a specific shelter.
        /// </summary>
        /// <param name="shelterId">The unique identifier of the shelter.</param>
        /// <returns>
        /// A collection of dogs associated with the specified shelter.
        /// </returns>
        public async Task<IEnumerable<Dog>> GetDogsByShelterIdAsync(Guid shelterId)
        {
            return await _context.Dogs
            .Include(d => d.DogMedias)
            .Where(d => d.ShelterId == shelterId)
            .ToListAsync();
        }

        /// <summary>
        /// Updates an existing dog in the database.
        /// </summary>
        /// <param name="dog">The dog to update.</param>
        public async Task UpdateAsync(Dog dog)
        {
            _context.Dogs.Update(dog);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Determines whether any dog matches the specified condition.
        /// </summary>
        /// <param name="predicate">The condition used to evaluate dogs.</param>
        /// <returns>
        /// True if at least one dog matches the condition; otherwise, false.
        /// </returns>
        public async Task<bool> ExistsAsync(Expression<Func<Dog, bool>> predicate)
        {
            return await _context.Dogs.AnyAsync(predicate);
        }
    }
}
