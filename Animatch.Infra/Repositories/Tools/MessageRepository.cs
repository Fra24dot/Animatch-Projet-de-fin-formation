using Animatch.Core.Interfaces.Repositories.Tools;
using Animatch.Domain.Entities;
using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Repositories.Tools
{
    public class MessageRepository(AnimatchDbContext _context) : IMessageRepository
    {
        
        public async Task<Match?> GetMatchWithDetailsAsync(Guid matchId)
        {
            return await _context.Matches
                .Include(m => m.Dog) 
                .FirstOrDefaultAsync(m => m.Id == matchId);
        }

        public async Task AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
