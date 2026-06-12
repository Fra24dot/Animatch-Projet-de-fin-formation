using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Core.Interfaces.Services.Tools;
using Animatch.Domain.ConnectingTables;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Services.Data
{
    public class ShelterDogService(IDogRepository _dogRepository, IAzureBlobService _blobService) : IShelterDogService
    {

        /// <summary>
        /// Adds a new dog to a shelter, uploads its image, and associates
        /// its personality traits, special needs, compatibilities,
        /// and medical history records.
        /// </summary>
        /// <param name="dog">The dog to add.</param>
        /// <param name="imageBlob">The image content.</param>
        /// <param name="fileName">The image file name.</param>
        /// <param name="personalityIds">Selected personality identifiers.</param>
        /// <param name="specialNeedsIds">Selected special needs identifiers.</param>
        /// <param name="compatibilityIds">Selected compatibility identifiers.</param>
        /// <param name="medicalHistoryIds">Selected medical history identifiers.</param>
        /// <returns>The created dog.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a matching non-adopted dog already exists in the shelter.
        /// </exception>
        public async Task<Dog> AddDogAsync(
            Dog dog,
            byte[]? imageBlob,
            string? fileName,
            List<int> personalityIds,
            List<int> specialNeedsIds,
            List<int> compatibilityIds,
            List<int> medicalHistoryIds)
        {
            
            var dogExisteDeja = await _dogRepository.ExistsAsync(d =>
                d.ShelterId == dog.ShelterId &&
                d.Name.ToLower() == dog.Name.ToLower() &&
                d.Race == dog.Race &&
                d.Gender == dog.Gender &&
                d.Status != DogStatus.Adopted
            );

            if (dogExisteDeja)
            {
                throw new InvalidOperationException($"Un chien nommé '{dog.Name}' avec la même race existe déjà dans votre refuge.");
            }

            
            dog.Id = Guid.NewGuid();
            dog.Status = DogStatus.Available;
            dog.CreatedAt = DateTime.UtcNow;

            
            dog.DogMedias = new List<DogMedia>();
            if (imageBlob != null && imageBlob.Length > 0 && !string.IsNullOrEmpty(fileName))
            {
                string azureImageUrl = await _blobService.UploadImageAsync(imageBlob, fileName);

                dog.DogMedias.Add(new DogMedia
                {
                    Media = new Media { Url = azureImageUrl }
                });
            }

            
            await _dogRepository.AddAsync(dog);
            

            
            if (personalityIds != null && personalityIds.Any())
            {
                dog.DogPersonalities = personalityIds.Select(id => new DogPersonality
                {
                    DogId = dog.Id, 
                    PersonalityId = id
                }).ToList();
            }

            if (specialNeedsIds != null && specialNeedsIds.Any())
            {
                dog.DogSpecialNeeds = specialNeedsIds.Select(id => new DogSpecialNeeds
                {
                    DogId = dog.Id,
                    SpecialNeedsId = id
                }).ToList();
            }

            if (medicalHistoryIds != null && medicalHistoryIds.Any())
            {
                dog.DogMedicalHistories = medicalHistoryIds.Select(id => new DogMedicalHistory
                {
                    DogId = dog.Id,
                    MedicalHistoryId = id
                }).ToList();
            }

            if (compatibilityIds != null && compatibilityIds.Any())
            {
                dog.DogCompatibilities = compatibilityIds.Select(id => new DogCompatibility
                {
                    DogId = dog.Id,
                    CompatibilityId = id
                }).ToList();
            }

            
            await _dogRepository.UpdateAsync(dog);
            

            return dog;
        }

     } 
}