using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.ConnectingTables
{
    public class UserRace
    {
        public Guid UserId { get; set; }
        public int RaceId { get; set; }

        public User User { get; set; }
        public DogRacePreference DogRace { get; set; }
    }
}
