using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class Admin
    { 
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        

        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
