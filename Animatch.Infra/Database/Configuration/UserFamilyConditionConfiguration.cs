using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserFamilyConditionConfiguration : IEntityTypeConfiguration<UserFamilyCondition>
    {
        public void Configure(EntityTypeBuilder<UserFamilyCondition> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.HousingType)
                .IsRequired();

            builder.Property(u => u.PeopleCount)
                .IsRequired();

            builder.Property(u => u.HasChildren)
                .IsRequired();

            builder.Property(u => u.PetsAllowed)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.UpdatedAt);

            builder.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
