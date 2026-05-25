using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.ConnectingTables
{
    public class UserDogGender
    {
        public Guid UserId { get; set; }
        public int DogGenderId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public DogGenderPreference DogGender { get; set; }
    }
}
