using Animatch.Core.Interfaces.Services.Auth;
using Animatch.Core.Interfaces.Services.Tools;
using Animatch.Security.Services.Auth;
using Animatch.Security.Services.Tools;
using Animatch.Security.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Security.Extensions
{
    public static class SecurityServiceExtension
    {
        public static void AddSecurityServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration
            .GetSection("JwtSettings")
            .Get<JwtSettings>();
            services.AddSingleton(jwtSettings);


            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHacherService, PasswordHacherService>();
            services.AddScoped<IAuthService, AuthService>();

        }
    }
}
