using Animatch.Core.Interfaces.Repositories.Data;
using Animatch.Core.Interfaces.Services.Auth;
using Animatch.Core.Interfaces.Services.Tools;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Animatch.Security.Services.Auth
{
    public class AuthService(
    IUserRepository userRepository,
    IShelterRepository shelterRepository,
    IJwtService jwtService,
    IPasswordHacherService passwordHacher) : IAuthService
    {
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetByEmailAsync(email);

            if (user == null || !passwordHacher.VerifyPassword(password, user.Password)) 
                throw new UnauthorizedAccessException("Invalid credentials");
            
            return user;
        }

        public async Task<User> RegisterUserAsync(string firstName, string lastName, string email, 
            string password, UserGender gender, DateTime birthDate)
        {
            if (await userRepository.EmailExistsAsync(email))
                throw new InvalidOperationException("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = passwordHacher.HachPassword(password),
                Gender = gender,
                BirthDate = birthDate,
                AccountType = AccountType.User,
                AccountCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            return await userRepository.CreateAsync(user);
        }

        public async Task<Shelter> RegisterShelterAsync(string name, string email, string password, 
            string companyNumber, string phoneNumber, string address, string city, string postalCode)
        {
            if (await shelterRepository.EmailExistsAsync(email))
                throw new InvalidOperationException("Email already exists");

            var shelter = new Shelter
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = passwordHacher.HachPassword(password),
                CompanyNumber = companyNumber,
                PhoneNumber = phoneNumber,
                Address = address,
                City = city,
                PostalCode = postalCode,
                ShelterStatus = ShelterStatus.Pending,
                IsVerified = false,
                IsActive = false,
                CreatedAt = DateTime.UtcNow
            };

            return await shelterRepository.CreateAsync(shelter);
        }
    }
}
