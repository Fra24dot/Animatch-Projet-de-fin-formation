using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Models;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
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



        public async Task<List<(Dog Dog, double Distance)>> GetDogsByPreferencesAsync(Guid userId, UserPreferencesModel pref)
        {

            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Latitude == null || user.Longitude == null)
                return new List<(Dog Dog, double Distance)>();


            var excludedDogIds = await _context.Matches
                .Where(m => m.UserId == userId)
                .Select(m => m.DogId)
                .ToListAsync();

            
            var query = _context.Dogs
                .Include(d => d.Shelter)
                .Include(d => d.DogMedias)
                .Include(d => d.DogPersonalities)
                .Include(d => d.DogCompatibilities)
                .Include(d => d.DogSpecialNeeds)
                .Include(d => d.DogMedicalHistories)
                .Where(d => d.Status == DogStatus.Available)
                
                .Where(d => !excludedDogIds.Contains(d.Id))
                .AsQueryable();

            
            if (pref.DogSizeIds != null && pref.DogSizeIds.Any())
                query = query.Where(d => pref.DogSizeIds.Contains((int)d.Size));

            if (pref.DogGenderIds != null && pref.DogGenderIds.Any())
                query = query.Where(d => pref.DogGenderIds.Contains((int)d.Gender));

            if (pref.DogAgeIds != null && pref.DogAgeIds.Any())
                query = query.Where(d => pref.DogAgeIds.Contains((int)d.AgeRange));

            if (pref.EnergyLevelIds != null && pref.EnergyLevelIds.Any())
                query = query.Where(d => pref.EnergyLevelIds.Contains((int)d.EnergyLevelEnum));

            if (pref.DogRaceIds != null && pref.DogRaceIds.Any())
                query = query.Where(d => pref.DogRaceIds.Contains((int)d.Race));

            var potentialDogs = await query.ToListAsync();
            var filteredDogs = new List<(Dog Dog, double Distance)>(); 

            foreach (var dog in potentialDogs)
            {
                if (dog.Shelter == null || dog.Shelter.Latitude == null || dog.Shelter.Longitude == null)
                    continue;

                double distance = CalculateDistance(
                    user.Latitude.Value,
                    user.Longitude.Value,
                    dog.Shelter.Latitude.Value,
                    dog.Shelter.Longitude.Value
                );

                if (distance <= pref.MaxDistance)
                {
                    
                    filteredDogs.Add((dog, distance));
                }
            }

            return filteredDogs;
        }

        
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var r = 6371; 
            var dLat = (lat2 - lat1) * Math.PI / 180;
            var dLon = (lon2 - lon1) * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return r * c;
        }
    }
}
