using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class Admin
    { 
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties

        public Guid UserId { get; set; }
    }
}
