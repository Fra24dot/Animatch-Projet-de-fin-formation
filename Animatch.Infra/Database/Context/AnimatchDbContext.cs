using Animatch.Domain.ConnectingTables;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Infrastructure.Database.Context
{
    public class AnimatchDbContext(DbContextOptions<AnimatchDbContext> options) : DbContext(options)
    {
        // Core entities
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Shelter> Shelters { get; set; } = null!;
        public DbSet<Dog> Dogs { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Media> Medias { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;

        // User preferences
        public DbSet<UserFamilyCondition> UserFamilyConditions { get; set; } = null!;
        public DbSet<UserExperience> UserExperiences { get; set; } = null!;
        public DbSet<UserLifestyle> UserLifestyles { get; set; } = null!;
        public DbSet<UserDistance> UserDistances { get; set; } = null!;

        // Reference tables
        public DbSet<DogRacePreference> Races { get; set; } = null!;
        public DbSet<Compatibility> Compatibilities { get; set; } = null!;
        public DbSet<Personality> Personalities { get; set; } = null!;
        public DbSet<SpecialNeeds> SpecialNeeds { get; set; } = null!;
        public DbSet<MedicalHistory> MedicalHistories { get; set; } = null!;
        public DbSet<DogSizePreference> DogSizes { get; set; } = null!;
        public DbSet<DogAge> DogAges { get; set; } = null!;
        public DbSet<DogGenderPreference> DogGenders { get; set; } = null!;
        public DbSet<DogEnergyLevel> DogEnergyLevels { get; set; } = null!;

        // Connecting tables
        public DbSet<DogCompatibility> DogCompatibilities { get; set; } = null!;
        public DbSet<DogPersonality> DogPersonalities { get; set; } = null!;
        public DbSet<DogSpecialNeeds> DogSpecialNeeds { get; set; } = null!;
        public DbSet<DogMedicalHistory> DogMedicalHistories { get; set; } = null!;
        public DbSet<Media> Medias { get; set; } = null!;
        public DbSet<UserCompatibility> UserCompatibilities { get; set; } = null!;
        public DbSet<UserPersonality> UserPersonalities { get; set; } = null!;
        public DbSet<UserDogSize> UserDogSizes { get; set; } = null!;
        public DbSet<UserDogAge> UserDogAges { get; set; } = null!;
        public DbSet<UserDogGender> UserDogGenders { get; set; } = null!;
        public DbSet<UserDogEnergy> UserDogEnergies { get; set; } = null!;
        public DbSet<UserRace> UserDogRaces { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnimatchDbContext).Assembly);
        }
    }
}

