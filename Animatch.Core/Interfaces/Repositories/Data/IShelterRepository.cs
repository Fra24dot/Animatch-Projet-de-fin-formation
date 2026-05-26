using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Repositories.Data
{
    public interface IShelterRepository
    {
        
        Task<Shelter?> GetByEmailAsync(string email);

        
        Task<bool> EmailExistsAsync(string email);

        
        Task<Shelter> CreateAsync(Shelter shelter);
    }
}
