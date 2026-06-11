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
            // 1. Vérification d'existence (Inchangé)
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

            // 2. Initialisation des données de base
            dog.Id = Guid.NewGuid();
            dog.Status = DogStatus.Available;
            dog.CreatedAt = DateTime.UtcNow;

            // 3. Gestion de l'unique Média (On le fait avant pour l'ajouter à l'arbre de l'entité)
            dog.DogMedias = new List<DogMedia>();
            if (imageBlob != null && imageBlob.Length > 0 && !string.IsNullOrEmpty(fileName))
            {
                string azureImageUrl = await _blobService.UploadImageAsync(imageBlob, fileName);

                dog.DogMedias.Add(new DogMedia
                {
                    Media = new Media { Url = azureImageUrl }
                });
            }

            // 🌟 ÉTAPE CRUCIALE 1 : On ajoute et on persiste le Chien et son Média d'abord !
            // Cela garantit que l'ID du chien existe physiquement dans SQL Server.
            await _dogRepository.AddAsync(dog);
            // Note : Si ton `_dogRepository.AddAsync` ne fait pas de `_context.SaveChangesAsync()`, 
            // assure-toi d'en appeler un ici ou via ton Unit of Work.

            // 🌟 ÉTAPE CRUCIALE 2 : On ajoute les entités de liaison avec l'ID du chien désormais validé
            if (personalityIds != null && personalityIds.Any())
            {
                dog.DogPersonalities = personalityIds.Select(id => new DogPersonality
                {
                    DogId = dog.Id, // On lie explicitement l'ID
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

            // 🌟 ÉTAPE CRUCIALE 3 : On met à jour l'entité pour sauvegarder les listes intermédiaires
            await _dogRepository.UpdateAsync(dog);
            // Idem ici, un SaveChangesAsync doit être appliqué pour valider les tables intermédiaires.

            return dog;
        }

     } 
}