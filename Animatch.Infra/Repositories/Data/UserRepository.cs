using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Domain.Entities;
using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Repositories.Data
{
    public class UserRepository(AnimatchDbContext animatchDbContext) : IUserRepository
    {

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await animatchDbContext.Users.FindAsync(id);

        }
        public async Task<User> CreateAsync(User user)
        {
            if(user is null) return null;
            animatchDbContext.Users.Add(user);
            await animatchDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await animatchDbContext.Users
         .AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (email is null) return null;
            return await animatchDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
