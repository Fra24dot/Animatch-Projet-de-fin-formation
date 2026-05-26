using Animatch.Domain.ConnectingTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    public class DogMedicalHistoryConfiguration : IEntityTypeConfiguration<DogMedicalHistory>
    {
        public void Configure(EntityTypeBuilder<DogMedicalHistory> builder)
        {
            builder.HasKey(dm => new { dm.DogId, dm.MedicalHistoryId });

            builder.HasOne(dm => dm.Dog)
                .WithMany(d => d.DogMedicalHistories)
                .HasForeignKey(dm => dm.DogId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dm => dm.MedicalHistory)
                .WithMany()
                .HasForeignKey(dm => dm.MedicalHistoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
