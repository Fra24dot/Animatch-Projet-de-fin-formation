using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserRaceConfiguration : IEntityTypeConfiguration<UserRace>
    {
        public void Configure(EntityTypeBuilder<UserRace> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RaceId });

            builder.HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.DogRace)
                .WithMany()
                .HasForeignKey(ur => ur.RaceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
