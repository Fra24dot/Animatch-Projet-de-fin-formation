using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class UserExperience
    {
        public int Id { get; set; } 
        public bool HasAnimals { get; set; } = false;
        public int AnimalsCount { get; set; }
        public AnimalType AnimalType { get; set; }
        public bool AlreadyAdopted { get; set; }
        public bool AdoptionPermit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
