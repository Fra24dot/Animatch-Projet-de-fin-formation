using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserCompatibilityConfiguration : IEntityTypeConfiguration<UserCompatibility>
    {
        public void Configure(EntityTypeBuilder<UserCompatibility> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.CompatibilityId });

            builder.HasOne(uc => uc.User)
                .WithMany()
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uc => uc.Compatibility)
                .WithMany()
                .HasForeignKey(uc => uc.CompatibilityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
