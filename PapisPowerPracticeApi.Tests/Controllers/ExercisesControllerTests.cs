using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.Controllers;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.IServices;
using PapisPowerPracticeApi.DTOs.Exercise.Request;
using PapisPowerPracticeApi.DTOs.Exercise.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PapisPowerPracticeApi.Tests.Controllers
{
	public class ExercisesControllerTests
	{
		private readonly Mock<IExerciseService> _serviceMock;
		private readonly ExercisesController _controller;

		public ExercisesControllerTests()
		{
			_serviceMock = new Mock<IExerciseService>();
			_controller = new ExercisesController(_serviceMock.Object);
		}

		[Fact]
		public async Task GetAllExercise_ReturnsOk_WithListOfExercises()
		{
			// Arrange
			var exercises = new List<Exercise>
			{
				new() { Id = 1, Name = "Bänkpress", Description = "Bröstövning", MuscleGroups = new List<MuscleGroup> { new() { Name = "Bröst" } } },
				new() { Id = 2, Name = "Knäböj", Description = "Benövning", MuscleGroups = new List<MuscleGroup> { new() { Name = "Ben" } } }
			};

			_serviceMock.Setup(s => s.GetAllExercisesAsync()).ReturnsAsync(exercises);

			// Act
			var result = await _controller.GetAllExercise();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedList = Assert.IsAssignableFrom<IEnumerable<ExerciseDto>>(okResult.Value);
			Assert.Equal(2, returnedList.Count());
		}

		[Fact]
		public async Task GetExerciseById_ReturnsOk_WhenFound()
		{
			// Arrange
			var exercise = new Exercise
			{
				Id = 1,
				Name = "Marklyft",
				Description = "Ryggövning",
				VideoUrl = "https://youtu.be/example",
				MuscleGroups = new List<MuscleGroup> { new() { Name = "Rygg" } }
			};

			_serviceMock.Setup(s => s.GetExerciseByIdAsync(1)).ReturnsAsync(exercise);

			// Act
			var result = await _controller.GetExerciseById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var dto = Assert.IsType<ExerciseDto>(okResult.Value);
			Assert.Equal("Marklyft", dto.Name);
			Assert.Contains("Rygg", dto.MuscleGroups);
		}

		[Fact]
		public async Task GetExerciseById_ReturnsNotFound_WhenMissing()
		{
			// Arrange
			_serviceMock.Setup(s => s.GetExerciseByIdAsync(99)).ReturnsAsync((Exercise?)null);

			// Act
			var result = await _controller.GetExerciseById(99);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task CreateExercise_ReturnsCreated_WhenValid()
		{
			// Arrange
			var input = new ExerciseCreateUpdateDto
			{
				Name = "Pullups",
				Description = "Rygg och armar",
				VideoUrl = "https://youtu.be/pullup",
				MuscleGroupIds = new List<int> { 1, 2 }
			};

			var created = new Exercise
			{
				Id = 10,
				Name = "Pullups",
				Description = "Rygg och armar",
				VideoUrl = "https://youtu.be/pullup",
				MuscleGroups = new List<MuscleGroup> { new() { Id = 1, Name = "Rygg" }, new() { Id = 2, Name = "Armar" } }
			};

			_serviceMock.Setup(s => s.CreateExerciseAsync(It.IsAny<Exercise>())).ReturnsAsync(created);

			// Act
			var result = await _controller.CreateExercise(input);

			// Assert
			var createdResult = Assert.IsType<CreatedAtActionResult>(result);
			var dto = Assert.IsType<ExerciseDto>(createdResult.Value);
			Assert.Equal(10, dto.Id);
			Assert.Equal("Pullups", dto.Name);
		}

		[Fact]
		public async Task DeleteExercise_ReturnsNoContent_WhenDeleted()
		{
			// Arrange
			_serviceMock.Setup(s => s.DeleteExerciseAsync(1)).ReturnsAsync(true);

			// Act
			var result = await _controller.DeleteExercise(1);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task DeleteExercise_ReturnsNotFound_WhenMissing()
		{
			// Arrange
			_serviceMock.Setup(s => s.DeleteExerciseAsync(1)).ReturnsAsync(false);

			// Act
			var result = await _controller.DeleteExercise(1);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}
	}
}
