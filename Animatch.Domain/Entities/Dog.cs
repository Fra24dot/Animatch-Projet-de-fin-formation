using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class Dog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Race Race { get; set; }

        public string Description { get; set; }
        public DogGender Gender { get; set; }
        public DogStatus Status { get; set; }
        public DogAgeRange AgeRange { get; set; }
        public DogSize Size { get; set; }
        public EnergyLevel EnergyLevel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Guid ShelterId { get; set; }
    }
}
