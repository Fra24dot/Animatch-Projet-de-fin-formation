using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class UserFamilyCondition
    {
        public int Id { get; set; }
        public string City { get; set; }
        public HousingType HousingType { get; set; }
        public int PeopleCount { get; set; }
        public bool HasChildren { get; set; }
        public bool PetAllowed { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public Guid UserId { get; set; }

    }
}
