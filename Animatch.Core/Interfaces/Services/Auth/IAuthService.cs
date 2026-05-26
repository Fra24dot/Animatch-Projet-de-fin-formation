using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        
        Task<User> LoginAsync(string email, string password);

        Task<User> RegisterUserAsync(string firstName, string lastName, string email, string password, UserGender gender, DateTime birthDate);

        Task<Shelter> RegisterShelterAsync(string name, string email, string password, string companyNumber, string phoneNumber, string address, string city, string postalCode);
    }
}
