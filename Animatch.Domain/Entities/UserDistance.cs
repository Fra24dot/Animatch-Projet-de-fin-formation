using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class UserDistance
    {
        public int Id { get; set; }

        public int MaxDistance { get; set; }

        public Guid UserId { get; set; }

        

        public User User { get; set; }
    }
}
