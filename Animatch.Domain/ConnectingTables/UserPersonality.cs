using System;
using System.Collections.Generic;
using System.Text;
using Animatch.Domain.Entities;

namespace Animatch.Domain.ConnectingTables
{
    public class UserPersonality
    {
        public Guid UserId { get; set; }
        public int PersonalityId { get; set; }


        public User User { get; set; }
        public Personality Personality { get; set; }
    }
}
