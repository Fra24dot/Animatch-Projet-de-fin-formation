using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogEnergyLevelConfiguration : IEntityTypeConfiguration<EnergyLevel>
    {
        public void Configure(EntityTypeBuilder<EnergyLevel> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new EnergyLevel { Id = 1, Name = "Low" },
                new EnergyLevel { Id = 2, Name = "Medium" },
                new EnergyLevel { Id = 3, Name = "High" }
            );
        }
    }
}
