namespace Animatch.Api.Dtos.Request
{
    public class ExperienceRequest
    {
        public bool HasAnimals { get; set; }
        public int AnimalsCount { get; set; }
        public int AnimalType { get; set; } 
        public bool AlreadyAdopted { get; set; }
        public bool AdoptionPermit { get; set; }
    }
}
