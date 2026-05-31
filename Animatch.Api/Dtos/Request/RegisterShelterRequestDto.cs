namespace Animatch.Api.Dtos.Request
{
    public class RegisterShelterRequestDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string CompanyNumber { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public int CreationYear { get; set; }
        public string ShelterAgreementProof { get; set; }= null!;
    }
}
