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
        public Enums.DogGender Gender { get; set; }
        public DogStatus Status { get; set; }
        public DogAgeRange AgeRange { get; set; }
        public DogSizePreference Size { get; set; }
        public EnergyLevel EnergyLevel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        
        public Guid ShelterId { get; set; }

        public Shelter Shelter { get; set; }
        public ICollection<Media> Medias { get; set; }
        public ICollection<SpecialNeeds> SpecialNeeds { get; set; }
        public ICollection<Personality> Personalities { get; set; }
        public ICollection<Compatibility> Compatibilities { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
    }
}
