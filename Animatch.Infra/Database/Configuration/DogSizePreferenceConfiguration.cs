using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogSizePreferenceConfiguration : IEntityTypeConfiguration<DogSizePreference>
    {
        public void Configure(EntityTypeBuilder<DogSizePreference> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new DogSizePreference { Id = 1, Name = "Small" },
                new DogSizePreference { Id = 2, Name = "Medium" },
                new DogSizePreference { Id = 3, Name = "Large" },
                new DogSizePreference { Id = 4, Name = "XLarge" }
            );
        }
    }
}
