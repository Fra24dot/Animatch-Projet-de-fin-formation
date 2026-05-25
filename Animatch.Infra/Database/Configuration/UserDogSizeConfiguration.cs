using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserDogSizeConfiguration : IEntityTypeConfiguration<UserDogSize>
    {
        public void Configure(EntityTypeBuilder<UserDogSize> builder)
        {
            builder.HasKey(us => new { us.UserId, us.DogSizeId });

            builder.HasOne(us => us.User)
                .WithMany()
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(us => us.DogSize)
                .WithMany()
                .HasForeignKey(us => us.DogSizeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
