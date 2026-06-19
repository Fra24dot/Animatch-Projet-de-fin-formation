using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Models
{
    public class ConversationModel
    {
        public Guid MatchId { get; set; }
        public Guid DogId { get; set; }
        public string DogName { get; set; } = null!;
        public string InterlocutorName { get; set; } = null!;
        public string? LastMessageContent { get; set; }
        public DateTime? LastMessageCreatedAt { get; set; }
    }
}
