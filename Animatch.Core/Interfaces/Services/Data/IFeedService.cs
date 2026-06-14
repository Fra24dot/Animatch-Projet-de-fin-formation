using Animatch.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Data
{
    public interface IFeedService
    {
        Task<List<DogFeedModel>> GetUserFeedAsync(Guid userId);
    }
}
