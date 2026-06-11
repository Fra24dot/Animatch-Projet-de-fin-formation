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
        public async Task<(User User, Guid? ShelterId)> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetByEmailAsync(email);

            if (user == null || !passwordHacher.VerifyPassword(password, user.Password))
                throw new UnauthorizedAccessException("Identifiants invalides.");

            Guid? shelterId = null; // 🌟 On prépare la variable pour stocker l'ID

            if (string.Equals(user.AccountType.ToString(), "Shelter", StringComparison.OrdinalIgnoreCase))
            {
                var shelter = await shelterRepository.GetByEmailAsync(email);

                if (shelter != null)
                {
                    if (shelter.ShelterStatus == ShelterStatus.Rejected)
                    {
                        throw new InvalidOperationException("Votre demande d'inscription a été rejetée par l'administrateur.");
                    }

                    if (shelter.ShelterStatus == ShelterStatus.Pending)
                    {
                        throw new InvalidOperationException("Votre compte refuge est en attente de validation par un administrateur. Patience ! 🐾");
                    }

                    // 🌟 Si le refuge est validé, on récupère son vrai ID !
                    shelterId = shelter.Id;
                }
            }

            // 🌟 On retourne les deux informations
            return (user, shelterId);
        }

        public async Task<User> RegisterUserAsync(string firstName, string lastName, string email, 
            string password, UserGender gender, DateTime birthDate)
        {
            var today = DateTime.UtcNow.Date;
            var age = today.Year - birthDate.Year;

            // Si l'anniversaire n'est pas encore passé cette année on retire un an
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                throw new InvalidOperationException("You must be at least 18 years old to register.");
            }

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
            string companyNumber, string phoneNumber, string address, string city, string postalCode, int creationYear)
        {
            if (await shelterRepository.EmailExistsAsync(email))
                throw new InvalidOperationException("Email already exists");

            var userConnection = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = passwordHacher.HachPassword(password), 
                AccountType = AccountType.Shelter, 
                CreatedAt = DateTime.UtcNow,
                AccountCompleted = true
            };
            await userRepository.CreateAsync(userConnection);

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
                CreationYear = creationYear,                   
                ShelterStatus = ShelterStatus.Pending,
                IsVerified = false,
                IsActive = false,
                CreatedAt = DateTime.UtcNow
            };

            return await shelterRepository.CreateAsync(shelter);
        }
    }
}
