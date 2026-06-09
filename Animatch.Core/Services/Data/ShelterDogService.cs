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


            dog.DogPersonalities = personalityIds.Select(id => new DogPersonality { PersonalityId = id }).ToList();
            dog.DogSpecialNeeds = specialNeedsIds.Select(id => new DogSpecialNeeds { SpecialNeedsId = id }).ToList();
            dog.DogMedicalHistories = medicalHistoryIds.Select(id => new DogMedicalHistory { MedicalHistoryId = id }).ToList();
            dog.DogCompatibilities = compatibilityIds.Select(id => new DogCompatibility { CompatibilityId = id }).ToList();

            // Gestion de l'unique Média
            dog.DogMedias = new List<DogMedia>();
            if (imageBlob != null && imageBlob.Length > 0 && !string.IsNullOrEmpty(fileName))
            {
                // On upload sur Azure et on récupère la vraie URL HTTP
                string azureImageUrl = await _blobService.UploadImageAsync(imageBlob, fileName);

                dog.DogMedias.Add(new DogMedia
                {
                    Media = new Media { Url = azureImageUrl } // Sauvegarde de l'URL Azure en BDD !
                });
            }


            await _dogRepository.AddAsync(dog);
            return dog;
        }

    } 
}