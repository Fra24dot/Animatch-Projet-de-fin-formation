using Animatch.Domain.ConnectingTables;
using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class Dog
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Race Race { get; set; }
        public string Description { get; set; } = null!;
        public DogGender Gender { get; set; }
        public DogStatus Status { get; set; } = DogStatus.Available;
        public DogAgeRange AgeRange { get; set; }
        public DogSize Size { get; set; }
        public EnergyLevelEnum EnergyLevelEnum { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        
        public Guid ShelterId { get; set; }

        public Shelter Shelter { get; set; } = null!;
        public ICollection<DogMedia> DogMedias { get; set; } = new List<DogMedia>();
        public ICollection<DogSpecialNeeds> DogSpecialNeeds { get; set; } = null!;
        public ICollection<DogPersonality> DogPersonalities { get; set; } = null!;
        public ICollection<DogCompatibility> DogCompatibilities { get; set; } = null!;
        public ICollection<DogMedicalHistory> DogMedicalHistories { get; set; } = null!;
    }
}
