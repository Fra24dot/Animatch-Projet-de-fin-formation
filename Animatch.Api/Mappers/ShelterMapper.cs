using Animatch.Api.Dtos.Response;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;

namespace Animatch.Api.Mappers
{
    public static class ShelterMapper
    {
        public static RegisterShelterResponseDto ToRegisterResponseDto(this Shelter shelter)
        {
            return new RegisterShelterResponseDto
            {
                Id = shelter.Id,
                Name = shelter.Name,
                Email = shelter.Email,
                ShelterStatus = shelter.ShelterStatus.ToString()
            };
        }

        public static LoginResponseDto ToLoginResponseDto(this Shelter shelter, string token)
        {
            return new LoginResponseDto
            {
                Token = token,
                Email = shelter.Email,
                AccountType = AccountType.Shelter.ToString()
            };
        }
    }
}
