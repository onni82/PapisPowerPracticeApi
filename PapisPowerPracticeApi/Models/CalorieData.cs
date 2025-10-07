using System.ComponentModel.DataAnnotations;

namespace PapisPowerPracticeApi.Models
{
    public class CalorieData
    {
        [Required]
        [RegularExpression("^(male|female)$", ErrorMessage = "Gender must be 'male' or 'female'.")]
        public string Gender { get; set; } = "";

        [Required, Range(20, 300)]
        public double Weight { get; set; }  // in kg

        [Required, Range(100, 250)]
        public double Height { get; set; }  // in cm

        [Required, Range(5, 120)]
        public int Age { get; set; }

        [Required]
        [RegularExpression("^(sedentary|lightly_active|moderately_active|active|very_active)$",
            ErrorMessage = "Invalid activity level.")]
        public string ActivityLevel { get; set; } = "";
    }
}
