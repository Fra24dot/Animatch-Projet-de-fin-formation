using System;
using System.Collections.Generic;
using System.Text;
using Animatch.Domain.Entities;

namespace Animatch.Domain.ConnectingTables
{
    public class DogCompatibility
    {
        public Guid DogId { get; set; }
        public int CompatibilityId { get; set; }


        public Dog Dog { get; set; } = null!;
        public Compatibility Compatibility { get; set; } = null!;
    }
}
