using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class ShelterConfiguration : IEntityTypeConfiguration<Shelter>
    {
            public void Configure(EntityTypeBuilder<Shelter> builder)
            {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(s => s.Email)
                .IsUnique();

            builder.Property(s => s.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.CompanyNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.PostalCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(s => s.ShelterStatus)
                .IsRequired();

            builder.Property(s => s.CreationYear)
            .IsRequired();

            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.IsVerified)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.Property(s => s.UpdatedAt);
            builder.Property(s => s.VerifiedAt);
            builder.Property(s => s.Latitude);
            builder.Property(s => s.Longitude);
            
            }
    }
}
    
