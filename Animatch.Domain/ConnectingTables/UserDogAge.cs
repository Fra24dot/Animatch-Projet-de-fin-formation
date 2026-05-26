using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.ConnectingTables
{
    public class UserDogAge
    {
        public Guid UserId { get; set; }
        public int DogAgeId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public DogAge DogAge { get; set; } = null!;
    }
}
