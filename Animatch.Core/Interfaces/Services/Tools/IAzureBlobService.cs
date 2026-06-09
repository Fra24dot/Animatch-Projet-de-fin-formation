using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Tools
{
    public interface IAzureBlobService
    {
        Task<string> UploadImageAsync(byte[] fileBytes, string fileName, string containerName = "dog-images");
    }
}
