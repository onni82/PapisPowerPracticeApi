using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Services.IServices
{
    public interface ICalorieCalculatorService
    {
        CalorieResult Calculate(CalorieData data);
        List<string> GetActivityLevels();
    }
}