using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Data
{
    public interface IAdminService
    {
        Task<IEnumerable<Shelter>> GetPendingSheltersAsync();
        Task<bool> ApproveShelterAsync(Guid shelterId);
    }
}
