using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogMediaConfiguration : IEntityTypeConfiguration<DogMedia>
    {
        public void Configure(EntityTypeBuilder<DogMedia> builder)
        {
            builder.HasKey(dm => new { dm.DogId, dm.MediaId });

            builder.HasOne(dm => dm.Dog)
                .WithMany(d => d.DogMedias)
                .HasForeignKey(dm => dm.DogId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dm => dm.Media)
                .WithMany()
                .HasForeignKey(dm => dm.MediaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
