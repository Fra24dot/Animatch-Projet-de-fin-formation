using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Models
{
   public class DogFeedModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string RaceName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string AgeRange { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string EnergyLevel { get; set; } = null!;
        public string? MainImageUrl { get; set; }
        public string ShelterName { get; set; } = null!;
        public double DistanceInKm { get; set; }

        public List<string> Personalities { get; set; } = new();
        public List<string> Compatibilities { get; set; } = new();
        public List<string> SpecialNeeds { get; set; } = new();
        public List<string> MedicalHistories { get; set; } = new();
    }
}
