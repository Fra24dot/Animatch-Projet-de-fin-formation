namespace Animatch.Api.Dtos.Response
{
    public class DogDetailResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Race { get; set; } = null!; 
        public string Description { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string AgeRange { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string EnergyLevel { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public string? ImageUrl { get; set; }

        public List<int> PersonalityIds { get; set; } = new();
        public List<int> SpecialNeedsIds { get; set; } = new();
        public List<int> CompatibilityIds { get; set; } = new();
        public List<int> MedicalHistoryIds { get; set; } = new();
    }
}
