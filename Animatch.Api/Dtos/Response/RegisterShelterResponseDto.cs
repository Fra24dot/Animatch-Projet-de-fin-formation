namespace Animatch.Api.Dtos.Response
{
    public class RegisterShelterResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ShelterStatus { get; set; } = null!;
    }
}
