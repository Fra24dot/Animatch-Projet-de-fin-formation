using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class UserDogGenderConfiguration : IEntityTypeConfiguration<UserDogGender>
    {
        public void Configure(EntityTypeBuilder<UserDogGender> builder)
        {
            builder.HasKey(ug => new { ug.UserId, ug.DogGenderId });

            builder.HasOne(ug => ug.User)
                .WithMany()
                .HasForeignKey(ug => ug.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ug => ug.DogGender)
                .WithMany()
                .HasForeignKey(ug => ug.DogGenderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
