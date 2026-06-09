using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogConfiguration : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(d => d.Gender)
                .IsRequired();

            builder.Property(d => d.Status)
                .IsRequired();

            builder.Property(d => d.AgeRange)
                .IsRequired();

            builder.Property(d => d.Size)
                .IsRequired();

            builder.Property(d => d.Race)
                .IsRequired();

            builder.Property(d => d.EnergyLevelEnum)
                .IsRequired();

            builder.Property(d => d.CreatedAt)
                .IsRequired();

            builder.Property(d => d.UpdatedAt);

            builder.HasOne(d => d.Shelter)
                .WithMany()
                .HasForeignKey(d => d.ShelterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

