using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserDistanceConfiguration : IEntityTypeConfiguration<UserDistance>
    {
        public void Configure(EntityTypeBuilder<UserDistance> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.MaxDistance)
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
