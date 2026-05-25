using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class CompatibilityConfiguration : IEntityTypeConfiguration<Compatibility>
    {
        public void Configure(EntityTypeBuilder<Compatibility> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new Compatibility { Id = 1, Name = "Good with kids" },
                new Compatibility { Id = 2, Name = "Good with animals" },
                new Compatibility { Id = 3, Name = "Good with strangers" }
            );
        }
    }
}
