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
        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The user's unique identifier.</param>
        /// <returns>
        /// The matching user if found; otherwise, null.
        /// </returns>
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await animatchDbContext.Users.FindAsync(id);

        }

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>
        /// The created user.
        /// </returns>
        public async Task<User> CreateAsync(User user)
        {
            if(user is null) return null;
            animatchDbContext.Users.Add(user);
            await animatchDbContext.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Checks whether a user account already exists for the specified email address.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>
        /// True if a user with the specified email exists; otherwise, false.
        /// </returns>
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await animatchDbContext.Users
         .AnyAsync(u => u.Email == email);
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <returns>
        /// The matching user if found; otherwise, null.
        /// </returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            if (email is null) return null;
            return await animatchDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
