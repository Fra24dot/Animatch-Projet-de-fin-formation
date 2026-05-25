using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class Match
    {
        public Guid Id { get; set; }
        public DateTime AdopterLikedAt { get; set; }

        public MatchStatus Status{ get; set; }

        public DateTime? ShelterResponseAt { get; set; }
        public bool ConversationEnabled { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        
        public Guid UserId { get; set; }
        public Guid DogId { get; set; }


        public User User { get; set; }
        public Dog Dog { get; set; }

    }
}
