using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Data
{
    public interface IMatchService
    {
        Task RegisterSwipeAsync(Guid userId, Guid dogId, bool isLike);
    }
}
