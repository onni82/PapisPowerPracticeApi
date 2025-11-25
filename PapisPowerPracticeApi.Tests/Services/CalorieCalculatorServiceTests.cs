using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.CalorieIntake;
using Xunit;

public class CalorieCalculatorServiceTests
{
    private readonly CalorieCalculatorService _service = new();

    [Fact]
    public void Calculate_MaleSedentary_ReturnsCorrectValues()
    {
        var data = new CalorieData
        {
            Gender = "male",
            Weight = 80,
            Height = 180,
            Age = 30,
            ActivityLevel = "sedentary"
        };

        var result = _service.Calculate(data);

        Assert.Equal(1780, result.Bmr);     // 800 + 1125 - 150 + 5
        Assert.Equal(2136, result.Tdee);    // * 1.2
    }

    [Fact]
    public void Calculate_FemaleSedentary_ReturnsCorrectValues()
    {
        var data = new CalorieData
        {
            Gender = "female",
            Weight = 65,
            Height = 165,
            Age = 25,
            ActivityLevel = "sedentary"
        };

        var result = _service.Calculate(data);

        Assert.Equal(1395.25, result.Bmr);
        Assert.Equal(1674.3, result.Tdee);
    }

    [Theory]
    [InlineData("sedentary", 1.2)]
    [InlineData("lightly_active", 1.375)]
    [InlineData("moderately_active", 1.55)]
    [InlineData("active", 1.725)]
    [InlineData("very_active", 1.9)]
    public void Calculate_AppliesCorrectMultipliers(string activity, double multiplier)
    {
        var data = new CalorieData
        {
            Gender = "male",
            Weight = 70,
            Height = 170,
            Age = 25,
            ActivityLevel = activity
        };

        var result = _service.Calculate(data);

        var bmr = result.Bmr;
        Assert.Equal(Math.Round(bmr * multiplier, 2), result.Tdee);
    }

    [Fact]
    public void GetActivityLevels_ReturnsAllFiveKeys()
    {
        var levels = _service.GetActivityLevels();

        Assert.Equal(5, levels.Count);
        Assert.Contains("sedentary", levels);
        Assert.Contains("lightly_active", levels);
        Assert.Contains("moderately_active", levels);
        Assert.Contains("active", levels);
        Assert.Contains("very_active", levels);
    }
}

