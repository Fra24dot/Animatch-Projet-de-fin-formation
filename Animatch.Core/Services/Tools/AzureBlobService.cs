using Animatch.Core.Interfaces.Services.Tools;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Services.Tools
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public AzureBlobService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureBlobStorage");
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadImageAsync(byte[] fileBytes, string fileName, string containerName = "dog-images")
        {
            // Récupérer ou créer le conteneur sur Azure (équivalent d'un dossier)
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            // Générer un nom de fichier unique pour éviter les collisions
            string uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var blobClient = containerClient.GetBlobClient(uniqueFileName);

            // Uploader les octets
            using var stream = new MemoryStream(fileBytes);
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "image/jpeg" });

            // Retourner l'URL absolue du fichier stocké sur le Cloud
            return blobClient.Uri.ToString();
        }
    }
}
