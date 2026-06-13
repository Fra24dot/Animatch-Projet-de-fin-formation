using System.ComponentModel.DataAnnotations;

namespace Animatch.Api.Dtos.Request
{
    public class SavePreferencesRequestDto
    {
        [Required]
        [Range(1, 500, ErrorMessage = "La distance doit être comprise entre 1 et 500 km.")]
        public int MaxDistance { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner au moins une taille.")]
        public List<int> DogSizeIds { get; set; } = new();

        [Required(ErrorMessage = "Veuillez sélectionner au moins un genre.")]
        public List<int> DogGenderIds { get; set; } = new();

        [Required(ErrorMessage = "Veuillez sélectionner au moins une tranche d'âge.")]
        public List<int> DogAgeIds { get; set; } = new();

        [Required(ErrorMessage = "Veuillez sélectionner au moins un niveau d'énergie.")]
        public List<int> EnergyLevelIds { get; set; } = new();

        [Required(ErrorMessage = "Veuillez sélectionner au moins un type de race.")]
        public List<int> DogRaceIds { get; set; } = new();
    }
}
