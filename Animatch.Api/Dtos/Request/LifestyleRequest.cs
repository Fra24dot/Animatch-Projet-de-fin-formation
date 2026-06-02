namespace Animatch.Api.Dtos.Request
{
    public class LifestyleRequest
    {
        public int JobType { get; set; } 
        public bool RemoteWork { get; set; }
        public int DogAloneHours { get; set; }
        public bool ActiveLifestyle { get; set; }
        public bool FinanciallyStable { get; set; }
    }
}
