using Animatch.Api.Dtos.Request;
using Animatch.Api.Dtos.Response;
using Animatch.Domain.Entities;
using Animatch.Domain.Enums;

namespace Animatch.Api.Mappers
{
    public static class UserProfileMapper
    {
        public static UserFamilyCondition ToEntity(this FamilyConditionRequest request, Guid userId)
        {
            return new UserFamilyCondition
            {
                UserId = userId,
                City = request.City,
                HousingType = (HousingType)request.HousingType, // Cast du int vers l'Enum
                PeopleCount = request.PeopleCount,
                HasChildren = request.HasChildren,
                PetsAllowed = request.PetsAllowed
            };
        }

        public static UserExperience ToEntity(this ExperienceRequest request, Guid userId)
        {
            return new UserExperience
            {
                UserId = userId,
                HasAnimals = request.HasAnimals,
                AnimalsCount = request.AnimalsCount,
                AnimalType = (AnimalType)request.AnimalType, // Cast du int vers l'Enum
                AlreadyAdopted = request.AlreadyAdopted,
                AdoptionPermit = request.AdoptionPermit
            };
        }

        public static UserLifestyle ToEntity(this LifestyleRequest request, Guid userId)
        {
            return new UserLifestyle
            {
                UserId = userId,
                JobType = (JobType)request.JobType, // Cast du int vers l'Enum
                RemoteWork = request.RemoteWork,
                DogAloneHours = request.DogAloneHours,
                ActiveLifestyle = request.ActiveLifestyle,
                FinanciallyStable = request.FinanciallyStable
            };
        }

      
        

        public static UserProfileResponse ToResponse(
            Guid userId,
            bool AccountCompleted,
            UserFamilyCondition? family,
            UserExperience? experience,
            UserLifestyle? lifestyle)
        {
            return new UserProfileResponse
            {
                UserId = userId,
                AccountCompleted = AccountCompleted,
                FamilyCondition = family?.ToResponse(),
                Experience = experience?.ToResponse(),
                Lifestyle = lifestyle?.ToResponse()
            };
        }

        private static FamilyConditionResponse ToResponse(this UserFamilyCondition entity)
        {
            return new FamilyConditionResponse
            {
                City = entity.City,
                HousingType = (int)entity.HousingType, 
                PeopleCount = entity.PeopleCount,
                HasChildren = entity.HasChildren,
                PetsAllowed = entity.PetsAllowed
            };
        }

        private static ExperienceResponse ToResponse(this UserExperience entity)
        {
            return new ExperienceResponse
            {
                HasAnimals = entity.HasAnimals,
                AnimalsCount = entity.AnimalsCount,
                AnimalType = (int)entity.AnimalType,
                AlreadyAdopted = entity.AlreadyAdopted,
                AdoptionPermit = entity.AdoptionPermit
            };
        }

        private static LifestyleResponse ToResponse(this UserLifestyle entity)
        {
            return new LifestyleResponse
            {
                JobType = (int)entity.JobType,
                RemoteWork = entity.RemoteWork,
                DogAloneHours = entity.DogAloneHours,
                ActiveLifestyle = entity.ActiveLifestyle,
                FinanciallyStable = entity.FinanciallyStable
            };
        }
    }
}

