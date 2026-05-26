using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogSpecialNeedsConfiguration : IEntityTypeConfiguration<DogSpecialNeeds>
    {
        public void Configure(EntityTypeBuilder<DogSpecialNeeds> builder)
        {
            builder.HasKey(ds => new { ds.DogId, ds.SpecialNeedsId });

            builder.HasOne(ds => ds.Dog)
                .WithMany(d => d.DogSpecialNeeds)
                .HasForeignKey(ds => ds.DogId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ds => ds.SpecialNeeds)
                .WithMany()
                .HasForeignKey(ds => ds.SpecialNeedsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
