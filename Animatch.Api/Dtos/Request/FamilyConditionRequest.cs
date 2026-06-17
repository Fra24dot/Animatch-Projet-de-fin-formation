namespace Animatch.Api.Dtos.Request
{
    public class FamilyConditionRequest
    {
        public string City { get; set; } = null!;
        public double? Latitude { get; set; }  
        public double? Longitude { get; set; }

        public int HousingType { get; set; } 
        public int PeopleCount { get; set; }
        public bool HasChildren { get; set; }
        public bool PetsAllowed { get; set; }
    }
}
