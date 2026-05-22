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
        public bool HasAlreadyAdopted { get; set; }
        public bool AdoptionPermit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation property
        public Guid UserId { get; set; }

    }
}
