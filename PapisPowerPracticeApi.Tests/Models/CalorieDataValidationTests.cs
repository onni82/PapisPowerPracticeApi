using PapisPowerPracticeApi.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class CalorieDataValidationTests
{
    private IList<ValidationResult> Validate(CalorieData model)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, new ValidationContext(model), results, true);
        return results;
    }

    [Fact]
    public void InvalidGender_ReturnsValidationError()
    {
        var model = new CalorieData
        {
            Gender = "other",
            Weight = 70,
            Height = 170,
            Age = 30,
            ActivityLevel = "active"
        };

        var errors = Validate(model);

        Assert.Contains(errors, e => e.MemberNames.Contains("Gender"));
    }

    [Fact]
    public void ValidModel_PassesValidation()
    {
        var model = new CalorieData
        {
            Gender = "female",
            Weight = 65,
            Height = 165,
            Age = 22,
            ActivityLevel = "sedentary"
        };

        var errors = Validate(model);

        Assert.Empty(errors);
    }
}
