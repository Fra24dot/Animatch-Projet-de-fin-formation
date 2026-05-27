using Animatch.Api.Dtos.Response;
using Animatch.Domain.Entities;

namespace Animatch.Api.Mappers
{
    public static class UserMapper
    {
        public static RegisterUserResponseDto ToRegisterResponseDto(this User user)
        {
            return new RegisterUserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AccountType = user.AccountType.ToString()
            };
        }

        public static LoginResponseDto ToLoginResponseDto(this User user, string token)
        {
            return new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                AccountType = user.AccountType.ToString()
            };
        }
    }
}
