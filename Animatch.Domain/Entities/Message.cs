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

        // Navigation properties
        public Guid UserId { get; set; }
        public Guid ShelterId { get; set; } 
        public Guid MatchId { get; set; }
    }
}
