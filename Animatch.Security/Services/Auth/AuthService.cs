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
        /// <summary>
        /// Authenticates a user using their email address and password.
        /// For shelter accounts, verifies the shelter approval status before allowing access.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's plain text password.</param>
        /// <returns>
        /// A tuple containing the authenticated user and the associated shelter identifier,
        /// if the account belongs to a shelter.
        /// </returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the provided credentials are invalid.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a shelter account is pending approval or has been rejected.
        /// </exception>
        public async Task<(User User, Guid? ShelterId)> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetByEmailAsync(email);

            if (user == null || !passwordHacher.VerifyPassword(password, user.Password))
                throw new UnauthorizedAccessException("Identifiants invalides.");

            Guid? shelterId = null; 

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

                    
                    shelterId = shelter.Id;
                }
            }

            
            return (user, shelterId);
        }

        /// <summary>
        /// Registers a new user account and creates the corresponding user profile.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="gender">The user's gender.</param>
        /// <param name="birthDate">The user's date of birth.</param>
        /// <returns>
        /// The newly created user.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the user is under 18 years old or when the email address is already in use.
        /// </exception>
        public async Task<User> RegisterUserAsync(string firstName, string lastName, string email, 
            string password, UserGender gender, DateTime birthDate)
        {
            var today = DateTime.UtcNow.Date;
            var age = today.Year - birthDate.Year;

            
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

        /// <summary>
        /// Registers a new shelter account and creates the associated login credentials.
        /// The shelter is initially created with a pending status and must be approved
        /// by an administrator before gaining access.
        /// </summary>
        /// <param name="name">The shelter name.</param>
        /// <param name="email">The shelter email address.</param>
        /// <param name="password">The shelter password.</param>
        /// <param name="companyNumber">The shelter company registration number.</param>
        /// <param name="phoneNumber">The shelter phone number.</param>
        /// <param name="address">The shelter address.</param>
        /// <param name="city">The shelter city.</param>
        /// <param name="postalCode">The shelter postal code.</param>
        /// <param name="creationYear">The year the shelter was established.</param>
        /// <returns>
        /// The newly created shelter.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the email address is already in use.
        /// </exception>

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
