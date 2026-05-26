using Animatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Configuration
{
    
    public class MedicalHistoryConfiguration : IEntityTypeConfiguration<MedicalHistory>
    {
        public void Configure(EntityTypeBuilder<MedicalHistory> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new MedicalHistory { Id = 1, Name = "Allergies" },
                new MedicalHistory { Id = 2, Name = "Vaccinated" },
                new MedicalHistory { Id = 3, Name = "Microchipped" },
                new MedicalHistory { Id = 4, Name = "Sterilized" },
                new MedicalHistory { Id = 5, Name = "Medical problems" }

            );
        }
    }
}
