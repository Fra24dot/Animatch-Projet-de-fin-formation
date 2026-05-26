using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.ConnectingTables
{
    public class UserDogEnergy
    {
        public Guid UserId { get; set; }
        public int EnergyLevelId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public EnergyLevel EnergyLevel { get; set; } = null!;
    }
}
