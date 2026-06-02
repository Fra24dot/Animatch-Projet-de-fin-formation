using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    
    public class UserExperienceConfiguration : IEntityTypeConfiguration<UserExperience>
    {
        public void Configure(EntityTypeBuilder<UserExperience> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.HasAnimals)
                .IsRequired();

            builder.Property(u => u.AnimalsCount)
                .IsRequired();

            builder.Property(u => u.AnimalType)
                .IsRequired();

            builder.Property(u => u.AlreadyAdopted)
                .IsRequired();

            builder.Property(u => u.AdoptionPermit)
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
