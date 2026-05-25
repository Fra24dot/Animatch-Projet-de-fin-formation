using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class Shelter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CreationYear { get; set; }
        public string CompanyNumber { get; set; }
        public ShelterStatus ShelterStatus { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string? ShelterAgreementProof { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? VerifiedAt { get; set; }

        public bool IsActive { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


    }
}
