using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Status)
                .IsRequired();

            builder.Property(m => m.AdopterLikedAt)
                .IsRequired();

            builder.Property(m => m.ConversationEnabled)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(m => m.CreatedAt)
                .IsRequired();

            builder.Property(m => m.UpdatedAt);
            builder.Property(m => m.DeletedAt);
            builder.Property(m => m.ShelterResponseAt);

            builder.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Dog)
                .WithMany()
                .HasForeignKey(m => m.DogId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
