using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid ShelterId { get; set; } 
        public Guid MatchId { get; set; }

        public User User { get; set; }
        public Shelter Shelter { get; set; }
        public Match Match { get; set; }
    }
}
