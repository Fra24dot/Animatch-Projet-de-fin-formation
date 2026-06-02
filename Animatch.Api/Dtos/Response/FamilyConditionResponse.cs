namespace Animatch.Api.Dtos.Response
{
    public class FamilyConditionResponse
    {
        public string City { get; set; } = null!;
        public int HousingType { get; set; }
        public int PeopleCount { get; set; }
        public bool HasChildren { get; set; }
        public bool PetsAllowed { get; set; }
    }
}
