using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public static readonly Guid AdminId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Admin
            {
                Id = AdminId,
                UserId = UserConfiguration.AdminUserId,
                CreatedAt = new DateTime(2026, 1, 1)
            });
        }
    }
}
