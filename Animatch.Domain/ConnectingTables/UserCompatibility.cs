using System;
using System.Collections.Generic;
using System.Text;
using Animatch.Domain.Entities;

namespace Animatch.Domain.ConnectingTables
{
    public class UserCompatibility
    {
        public Guid UserId { get; set; }
        public int CompatibilityId { get; set; }


        public User User { get; set; } = null!;
        public Compatibility Compatibility { get; set; }
    }
}
