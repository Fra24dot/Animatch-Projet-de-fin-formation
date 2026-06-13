using Animatch.Api.Dtos.Request;
using Animatch.Api.Dtos.Response;
using Animatch.Domain.Entities;

namespace Animatch.Api.Mappers
{
    public static class DogMapper
    {
        public static async Task<(Dog DogEntity, byte[]? ImageBlob, string? FileName)> ToEntityAsync(AddDogRequestDto dto, Guid shelterId)
        {
            byte[]? imageBlob = null;
            string? fileName = null;

            if (dto.MediaFile != null)
            {
                using var memoryStream = new MemoryStream();
                await dto.MediaFile.CopyToAsync(memoryStream);
                imageBlob = memoryStream.ToArray();
                fileName = dto.MediaFile.FileName;
            }

            var dog = new Dog
            {
                Name = dto.Name,
                Race = dto.Race,
                Description = dto.Description,
                Gender = dto.Gender,
                AgeRange = dto.AgeRange,
                Size = dto.Size,
                EnergyLevelEnum = dto.EnergyLevel,
                ShelterId = shelterId
            };

            return (dog, imageBlob, fileName);
        }

        
        public static DogDetailResponseDto ToDetailResponseDto(this Dog dog)
        {
            
            var uniqueImageUrl = dog.DogMedias?
                .Where(dm => dm.Media != null)
                .Select(dm => dm.Media.Url)
                .FirstOrDefault();

            return new DogDetailResponseDto
            {
                Id = dog.Id,
                Name = dog.Name,
                Race = dog.Race.ToString(),
                Description = dog.Description,
                Gender = dog.Gender.ToString(),
                Status = dog.Status.ToString(),
                AgeRange = dog.AgeRange.ToString(),
                Size = dog.Size.ToString(),
                EnergyLevel = dog.EnergyLevelEnum.ToString(),
                CreatedAt = dog.CreatedAt,

                
                ImageUrl = uniqueImageUrl,

                PersonalityIds = dog.DogPersonalities?.Select(p => p.PersonalityId).ToList() ?? new List<int>(),
                SpecialNeedsIds = dog.DogSpecialNeeds?.Select(s => s.SpecialNeedsId).ToList() ?? new List<int>(),
                MedicalHistoryIds = dog.DogMedicalHistories?.Select(m => m.MedicalHistoryId).ToList() ?? new List<int>(),
                CompatibilityIds = dog.DogCompatibilities?.Select(c => c.CompatibilityId).ToList() ?? new List<int>()
            };
        }
    }
}
