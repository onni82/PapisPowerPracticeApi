using Microsoft.AspNetCore.Mvc;
using Moq;
using PapisPowerPracticeApi.Controllers;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.IServices;
using Xunit;

public class CalorieIntakeControllerTests
{
    [Fact]
    public void CalculateCalories_InvalidModel_ReturnsBadRequest()
    {
        var service = new Mock<ICalorieCalculatorService>();
        var controller = new CalorieIntakeController(service.Object);
        controller.ModelState.AddModelError("Gender", "Required");

        var data = new CalorieData();

        var result = controller.CalculateCalories(data);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void CalculateCalories_ValidModel_ReturnsOkWithResult()
    {
        var data = new CalorieData
        {
            Gender = "male",
            Weight = 70,
            Height = 175,
            Age = 25,
            ActivityLevel = "active"
        };

        var fakeResult = new CalorieResult
        {
            Gender = "male",
            ActivityLevel = "active",
            Bmr = 1600,
            Tdee = 2800
        };

        var service = new Mock<ICalorieCalculatorService>();
        service.Setup(s => s.Calculate(It.IsAny<CalorieData>())).Returns(fakeResult);

        var controller = new CalorieIntakeController(service.Object);

        var result = controller.CalculateCalories(data) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(fakeResult, result.Value);
    }

    [Fact]
    public void GetActivityLevels_ReturnsOkWithData()
    {
        var activityLevels = new List<string> { "sedentary", "active" };

        var service = new Mock<ICalorieCalculatorService>();
        service.Setup(s => s.GetActivityLevels()).Returns(activityLevels);

        var controller = new CalorieIntakeController(service.Object);

        var result = controller.GetActivityLevels() as OkObjectResult;

        Assert.NotNull(result);
        var returned = Assert.IsAssignableFrom<IEnumerable<string>>(result.Value);
        Assert.Equal(activityLevels, returned);
    }
}
