using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogAgeConfiguration : IEntityTypeConfiguration<DogAge>
    {
        public void Configure(EntityTypeBuilder<DogAge> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new DogAge { Id = 1, Name = "Puppy" },
                new DogAge { Id = 2, Name = "Young" },
                new DogAge { Id = 3, Name = "Adult" },
                new DogAge { Id = 4, Name = "Senior" }
            );
        }
    }
}
