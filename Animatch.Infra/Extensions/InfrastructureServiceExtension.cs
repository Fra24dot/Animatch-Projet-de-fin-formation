using Animatch.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AnimatchDbContext>(options => options.UseSqlServer(connectionString));

        }
    }
}
