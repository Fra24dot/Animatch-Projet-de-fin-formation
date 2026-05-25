using Animatch.Core.Interfaces.Services.Tools;
using Animatch.Core.Services;
using Animatch.Core.Settings;
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

            services.AddScoped<IEmailService, EmailService>();
        }

    }
}
