using System;
using System.Collections.Generic;
using System.Text;
using Animatch.Domain.Entities;

namespace Animatch.Domain.ConnectingTables
{
    public class DogPersonality
    {
        public Guid DogId { get; set; }
        public int PersonalityId { get; set; }

        public Dog Dog { get; set; }
        public Personality Personality { get; set; }

    }
}
