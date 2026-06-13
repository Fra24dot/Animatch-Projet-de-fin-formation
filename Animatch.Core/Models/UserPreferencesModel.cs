using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Models
{
    public class UserPreferencesModel
    {
        public int MaxDistance { get; set; }
        public List<int> DogSizeIds { get; set; } = new();
        public List<int> DogGenderIds { get; set; } = new();
        public List<int> DogAgeIds { get; set; } = new();
        public List<int> EnergyLevelIds { get; set; } = new();
        public List<int> DogRaceIds { get; set; } = new();
    }
}
