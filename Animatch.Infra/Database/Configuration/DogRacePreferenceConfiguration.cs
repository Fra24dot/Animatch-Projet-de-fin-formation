using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogRacePreferenceConfiguration : IEntityTypeConfiguration<DogRacePreference>
    {
        public void Configure(EntityTypeBuilder<DogRacePreference> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new DogRacePreference { Id = 1, Name = "Pure breed" },
                new DogRacePreference { Id = 2, Name = "Mixed breed" }
                
            );
        }
    }
}
