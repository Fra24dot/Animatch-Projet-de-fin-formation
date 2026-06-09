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
        public async Task AddAsync(Dog dog)
        {
            await _context.Dogs.AddAsync(dog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var dog = await _context.Dogs.FindAsync(id);
            if (dog != null)
            {
                _context.Dogs.Remove(dog);
                await _context.SaveChangesAsync();
            }
        }

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

        public async Task<IEnumerable<Dog>> GetDogsByShelterIdAsync(Guid shelterId)
        {
            return await _context.Dogs
            .Include(d => d.DogMedias)
            .Where(d => d.ShelterId == shelterId)
            .ToListAsync();
        }

        public async Task UpdateAsync(Dog dog)
        {
            _context.Dogs.Update(dog);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Dog, bool>> predicate)
        {
            return await _context.Dogs.AnyAsync(predicate);
        }
    }
}
