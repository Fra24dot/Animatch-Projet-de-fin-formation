using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogGenderPreferenceConfiguration : IEntityTypeConfiguration<DogGenderPreference>
    {
        public void Configure(EntityTypeBuilder<DogGenderPreference> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new DogGenderPreference { Id = 1, Name = "Male" },
                new DogGenderPreference { Id = 2, Name = "Female" }
            );
        }
    }
}
