using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class SpecialNeedsConfiguration : IEntityTypeConfiguration<SpecialNeeds>
    {
        public void Configure(EntityTypeBuilder<SpecialNeeds> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new SpecialNeeds { Id = 1, Name = "Anxiety" },
                new SpecialNeeds { Id = 2, Name = "Afraid of men" },
                new SpecialNeeds { Id = 3, Name = "Afraid of noises" }      
            );
        }
    }
}
