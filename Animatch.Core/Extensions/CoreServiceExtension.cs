using Animatch.Core.Interfaces.Services.Auth;
using Animatch.Core.Interfaces.Services.Data;
using Animatch.Core.Interfaces.Services.Tools;
using Animatch.Core.Services.Data;
using Animatch.Core.Services.Tools;
using Animatch.Core.Settings;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Extensions
{
    public static class CoreServiceExtension
    {
        public static void AddCoreServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var emailSettings = configuration
            .GetSection("EmailSettings").Get<EmailSettings>();
            services.AddSingleton(emailSettings);

            services.AddScoped(x => new BlobServiceClient(
            configuration.GetConnectionString("AzureBlobStorage")
        ));

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IShelterDogService, ShelterDogService>();
            services.AddScoped<IAzureBlobService, AzureBlobService>();
        }

    }
}
