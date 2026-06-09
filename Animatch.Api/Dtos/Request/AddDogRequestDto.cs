using Animatch.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Animatch.Api.Dtos.Request
{
    public class AddDogRequestDto
    {
        [Required] 
        public string Name { get; set; } = null!;
        [Required] 
        public Race Race { get; set; }
        [Required] 
        public string Description { get; set; } = null!;
        [Required] 
        public DogGender Gender { get; set; }
        [Required] 
        public DogAgeRange AgeRange { get; set; }
        [Required] 
        public DogSize Size { get; set; }
        [Required] 
        public EnergyLevelEnum EnergyLevel { get; set; }

        public IFormFile? MediaFile { get; set; }

        public List<int> PersonalityIds { get; set; } = new();
        public List<int> SpecialNeedsIds { get; set; } = new();
        public List<int> CompatibilityIds { get; set; } = new();
        public List<int> MedicalHistoryIds { get; set; } = new();
    }
}
