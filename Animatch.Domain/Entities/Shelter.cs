using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class Shelter
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int CreationYear { get; set; }
        public string CompanyNumber { get; set; } = null!;
        public ShelterStatus ShelterStatus { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string ShelterAgreementProof { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public bool IsVerified { get; set; }
        public DateTime? VerifiedAt { get; set; }

        public bool IsActive { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


    }
}
