using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserPersonalityConfiguration : IEntityTypeConfiguration<UserPersonality>
    {
        public void Configure(EntityTypeBuilder<UserPersonality> builder)
        {
            builder.HasKey(up => new { up.UserId, up.PersonalityId });

            builder.HasOne(up => up.User)
                .WithMany()
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(up => up.Personality)
                .WithMany()
                .HasForeignKey(up => up.PersonalityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
