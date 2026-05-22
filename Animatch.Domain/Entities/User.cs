using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserGender Gender { get; set; }
        public AccountType AccountType { get; set; }
        public bool AccountCompleted { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
