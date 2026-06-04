using Animatch.Api.Dtos.Response;
using Animatch.Domain.Entities;

namespace Animatch.Api.Mappers
{
    public static class AdminMapper
    {
        public static PendingShelterResponseDto ToPendingResponseDto(this Shelter shelter)
        {
            return new PendingShelterResponseDto
            {
                Id = shelter.Id,
                Name = shelter.Name,
                Email = shelter.Email,
                PhoneNumber = shelter.PhoneNumber,
                Address = shelter.Address,
                CompanyNumber = shelter.CompanyNumber
            };
        }
    }
}
