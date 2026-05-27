namespace Animatch.Api.Dtos.Response
{
    public class RegisterUserResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AccountType { get; set; } = null!;
    }
}
