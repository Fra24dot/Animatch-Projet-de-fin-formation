using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserDogAgeConfiguration : IEntityTypeConfiguration<UserDogAge>
    {
        public void Configure(EntityTypeBuilder<UserDogAge> builder)
        {
            builder.HasKey(ua => new { ua.UserId, ua.DogAgeId });

            builder.HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ua => ua.DogAge)
                .WithMany()
                .HasForeignKey(ua => ua.DogAgeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
