namespace Animatch.Api.Dtos.Response
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AccountType { get; set; } = null!;
    }
}
