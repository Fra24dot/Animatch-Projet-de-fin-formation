using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserDogEnergyConfiguration : IEntityTypeConfiguration<UserDogEnergy>
    {
        public void Configure(EntityTypeBuilder<UserDogEnergy> builder)
        {
            builder.HasKey(ue => new { ue.UserId, ue.EnergyLevelId });

            builder.HasOne(ue => ue.User)
                .WithMany()
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ue => ue.EnergyLevel)
                .WithMany()
                .HasForeignKey(ue => ue.EnergyLevelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
