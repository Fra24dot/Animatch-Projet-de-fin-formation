namespace Animatch.Api.Dtos.Response
{
    public class ExperienceResponse
    {
        public bool HasAnimals { get; set; }
        public int AnimalsCount { get; set; }
        public int AnimalType { get; set; }
        public bool AlreadyAdopted { get; set; }
        public bool AdoptionPermit { get; set; }
    }
}
