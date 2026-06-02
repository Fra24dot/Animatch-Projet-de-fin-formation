using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogPersonalityConfiguration : IEntityTypeConfiguration<DogPersonality>
    {
        public void Configure(EntityTypeBuilder<DogPersonality> builder)
        {
            builder.HasKey(dp => new { dp.DogId, dp.PersonalityId });

            builder.HasOne(dp => dp.Dog)
                .WithMany(d => d.DogPersonalities)
                .HasForeignKey(dp => dp.DogId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dp => dp.Personality)
                .WithMany()
                .HasForeignKey(dp => dp.PersonalityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
