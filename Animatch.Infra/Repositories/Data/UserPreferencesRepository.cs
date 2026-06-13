using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Models;
using Animatch.Domain.ConnectingTables;
using Animatch.Domain.Entities;
using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Repositories.Data
{
    public class UserPreferencesRepository(AnimatchDbContext _context) : IUserPreferencesRepository
    {
        public async Task<UserPreferencesModel?> GetPreferencesByUserIdAsync(Guid userId)
        {
            var maxDistanceEntity = await _context.UserDistances
          .FirstOrDefaultAsync(p => p.UserId == userId);

            if (maxDistanceEntity == null) return null;

            var sizeIds = await _context.UserDogSizes.Where(u => u.UserId == userId).Select(u => u.DogSizeId).ToListAsync();
            var genderIds = await _context.UserDogGenders.Where(u => u.UserId == userId).Select(u => u.DogGenderId).ToListAsync();
            var ageIds = await _context.UserDogAges.Where(u => u.UserId == userId).Select(u => u.DogAgeId).ToListAsync();

            
            var energyIds = await _context.UserDogEnergies
                .Where(u => u.UserId == userId)
                .Select(u => u.EnergyLevelId)
                .ToListAsync();

            var raceIds = await _context.UserDogRaces.Where(u => u.UserId == userId).Select(u => u.RaceId).ToListAsync();

            return new UserPreferencesModel
            {
                MaxDistance = maxDistanceEntity.MaxDistance,
                DogSizeIds = sizeIds,
                DogGenderIds = genderIds,
                DogAgeIds = ageIds,
                EnergyLevelIds = energyIds,
                DogRaceIds = raceIds
            };
        }

        public async Task SavePreferencesAsync(Guid userId, int maxDistance, List<int> sizeIds, 
            List<int> genderIds, List<int> ageIds, List<int> energyIds, List<int> raceIds)
        {
            var existingDistance = await _context.UserDistances
        .FirstOrDefaultAsync(p => p.UserId == userId);

            if (existingDistance != null)
            {
                existingDistance.MaxDistance = maxDistance; 
            }
            else
            {
                
                await _context.UserDistances.AddAsync(new UserDistance
                {
                    UserId = userId,
                    MaxDistance = maxDistance
                });
            }

            var oldSizes = _context.UserDogSizes.Where(x => x.UserId == userId);
            var oldGenders = _context.UserDogGenders.Where(x => x.UserId == userId);
            var oldAges = _context.UserDogAges.Where(x => x.UserId == userId);
            var oldEnergies = _context.UserDogEnergies.Where(x => x.UserId == userId);
            var oldRaces = _context.UserDogRaces.Where(x => x.UserId == userId);

            _context.UserDogSizes.RemoveRange(oldSizes);
            _context.UserDogGenders.RemoveRange(oldGenders);
            _context.UserDogAges.RemoveRange(oldAges);
            _context.UserDogEnergies.RemoveRange(oldEnergies);
            _context.UserDogRaces.RemoveRange(oldRaces);

            foreach (var sizeId in sizeIds)
            {
                await _context.UserDogSizes.AddAsync(new UserDogSize 
                { 
                    UserId = userId, 
                    DogSizeId = sizeId 
                });
            }

            foreach (var genderId in genderIds)
            {
                await _context.UserDogGenders.AddAsync(new UserDogGender 
                { 
                    UserId = userId, 
                    DogGenderId = genderId 
                });
            }

            foreach (var ageId in ageIds)
            {
                await _context.UserDogAges.AddAsync(new UserDogAge 
                { 
                    UserId = userId, 
                    DogAgeId = ageId 
                });
            }

            
            foreach (var energyId in energyIds)
            {
                await _context.UserDogEnergies.AddAsync(new UserDogEnergy
                {
                    UserId = userId,
                    EnergyLevelId = energyId
                });
            }

            foreach (var raceId in raceIds)
            {
                
                await _context.UserDogRaces.AddAsync(new UserRace
                {
                    UserId = userId,
                    RaceId = raceId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}

