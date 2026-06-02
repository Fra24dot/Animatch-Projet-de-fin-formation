using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.ConnectingTables
{
    public class UserDogSize
    {
        public Guid UserId { get; set; }
        public int DogSizeId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public DogSizePreference DogSize { get; set; } = null!;
    }
}
