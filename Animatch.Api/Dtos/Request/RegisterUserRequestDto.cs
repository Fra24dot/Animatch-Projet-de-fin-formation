using Animatch.Domain.Enums;

namespace Animatch.Api.Dtos.Request
{
    public class RegisterUserRequestDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserGender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
