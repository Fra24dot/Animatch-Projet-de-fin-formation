using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    
    public class UserLifestyleConfiguration : IEntityTypeConfiguration<UserLifestyle>
    {
        public void Configure(EntityTypeBuilder<UserLifestyle> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.JobType)
                .IsRequired();

            builder.Property(u => u.RemoteWork)
                .IsRequired();

            builder.Property(u => u.DogAloneHours)
                .IsRequired();

            builder.Property(u => u.ActiveLifestyle)
                .IsRequired();

            builder.Property(u => u.FinanciallyStable)
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
