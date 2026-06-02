using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class PersonalityConfiguration : IEntityTypeConfiguration<Personality>
    {
        public void Configure(EntityTypeBuilder<Personality> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new Personality { Id = 1, Name = "Playful" },
                new Personality { Id = 2, Name = "Sensitive" },
                new Personality { Id = 3, Name = "Protective" },
                new Personality { Id = 4, Name = "Affectionate" },
                new Personality { Id = 5, Name = "Independent" },
                new Personality { Id = 6, Name = "Smart" },
                new Personality { Id = 7, Name = "Shy" },
                new Personality { Id = 8, Name = "Sociable" },
                new Personality { Id = 9, Name = "Dominant" }

            );
        }
    }
}
