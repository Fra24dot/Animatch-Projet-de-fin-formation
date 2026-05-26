using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Content)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(m => m.IsRead)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(m => m.CreatedAt)
                .IsRequired();

            builder.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Shelter)
                .WithMany()
                .HasForeignKey(m => m.ShelterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Match)
                .WithMany()
                .HasForeignKey(m => m.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
 }

