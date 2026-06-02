namespace Animatch.Api.Dtos.Response
{
    public class UserProfileResponse
    {
        public Guid UserId { get; set; }
        public bool AccountCompleted { get; set; }

        public FamilyConditionResponse? FamilyCondition { get; set; }
        public ExperienceResponse? Experience { get; set; }
        public LifestyleResponse? Lifestyle { get; set; }
    }
}
