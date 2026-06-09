using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Data
{
    public interface IShelterDogService
    {
        Task<Dog> AddDogAsync(Dog dog,
            byte[]? imageBlob,
            string? fileName,
            List<int> personalityIds,
            List<int> specialNeedsIds,
            List<int> compatibilityIds,
            List<int> medicalHistoryIds);
    }
}
