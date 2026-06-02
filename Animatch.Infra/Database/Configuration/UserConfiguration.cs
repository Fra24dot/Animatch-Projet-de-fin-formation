using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public static readonly Guid AdminUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.AccountType)
                .IsRequired();

            builder.Property(u => u.Gender)
                .IsRequired();

            builder.Property(u => u.BirthDate)
                .IsRequired();

            builder.Property(u => u.AccountCompleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.UpdatedAt);
            builder.Property(u => u.DeletedAt);

            builder.Property(u => u.Longitude);
            builder.Property(u => u.Latitude);

            builder.HasData(
                new User
                {
                    Id = AdminUserId,
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "admin@animatch.be",
                    Password = "wNPUpqDi7HH1EjwwPggiRrnUlZUlJlwBUu25Sh9rqvJsfKkMN5lFa5/bcHE2yrqw", 
                    Gender = UserGender.Other,
                    BirthDate = new DateTime(1990, 1, 1),
                    AccountCompleted = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                });
        }
    }
}
