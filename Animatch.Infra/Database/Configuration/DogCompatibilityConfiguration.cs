using Animatch.Domain.ConnectingTables;
using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogCompatibilityConfiguration : IEntityTypeConfiguration<DogCompatibility>
    {
        public void Configure(EntityTypeBuilder<DogCompatibility> builder)
        {
            builder.HasKey(dc => new { dc.DogId, dc.CompatibilityId });

            builder.HasOne(dc => dc.Dog)
                .WithMany(d => d.DogCompatibilities)
                .HasForeignKey(dc => dc.DogId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dc => dc.Compatibility)
                .WithMany()
                .HasForeignKey(dc => dc.CompatibilityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
