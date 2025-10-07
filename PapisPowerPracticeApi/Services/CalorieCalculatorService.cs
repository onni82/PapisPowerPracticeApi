using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Services.CalorieIntake
{
    public class CalorieCalculatorService : ICalorieCalculatorService
    {
        private readonly Dictionary<string, double> _activityMultipliers = new()
        {
            { "sedentary", 1.2 },
            { "lightly_active", 1.375 },
            { "moderately_active", 1.55 },
            { "active", 1.725 },
            { "very_active", 1.9 }
        };

        public CalorieResult Calculate(CalorieData data)
        {
            double bmr = (10 * data.Weight) + (6.25 * data.Height) - (5 * data.Age);
            bmr += (data.Gender.ToLower() == "female") ? -161 : 5;

            var multiplier = _activityMultipliers[data.ActivityLevel.ToLower()];
            var tdee = bmr * multiplier;

            return new CalorieResult
            {
                Gender = data.Gender,
                ActivityLevel = data.ActivityLevel,
                Bmr = Math.Round(bmr, 2),
                Tdee = Math.Round(tdee, 2)
            };
        }

        public List<string> GetActivityLevels()
        {
            return _activityMultipliers.Keys.ToList();
        }
    }
}
