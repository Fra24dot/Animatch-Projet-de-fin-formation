namespace Animatch.Api.Dtos.Request
{
    public class UpdateUserProfileRequest
    {
        public FamilyConditionRequest FamilyCondition { get; set; } = null!;
        public ExperienceRequest Experience { get; set; } = null!;
        public LifestyleRequest Lifestyle { get; set; } = null!;
    }
}
