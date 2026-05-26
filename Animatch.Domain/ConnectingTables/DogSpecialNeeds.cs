using System;
using System.Collections.Generic;
using System.Text;
using Animatch.Domain.Entities;

namespace Animatch.Domain.ConnectingTables
{
    public class DogSpecialNeeds
    {
        public Guid DogId { get; set; }
        public int SpecialNeedsId { get; set; }


        public Dog Dog { get; set; } = null!;
        public SpecialNeeds SpecialNeeds { get; set; } = null!;


    }
}
