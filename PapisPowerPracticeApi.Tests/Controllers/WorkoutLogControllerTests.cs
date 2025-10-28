using Moq;
using PapisPowerPracticeApi.Controllers;
using PapisPowerPracticeApi.DTOs.WorkoutLog;
using PapisPowerPracticeApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PapisPowerPracticeApi.Tests.Controllers
{
    public class WorkoutLogControllerTests
    {
        private readonly Mock<IWorkoutLogService> _serviceMock;
        private readonly WorkoutLogController _controller;
        
        public WorkoutLogControllerTests()
        {
            _serviceMock = new Mock<IWorkoutLogService>();
            _controller = new WorkoutLogController(_serviceMock.Object);
        }
        
        [Fact]
        public async Task GetAllWorkoutLogs_ReturnsListOfWorkoutLogs()
        {
            // Arrange
            var workoutLogs = new List<GetWorkoutLogDTO>
            {
                new GetWorkoutLogDTO { Id = 1, StartTime = DateTime.Now, EndTime = DateTime.Now, Notes = "Test 1"},
                new GetWorkoutLogDTO { Id = 2, StartTime = DateTime.Now, EndTime = DateTime.Now, Notes = "Test 2"}
            };
            _serviceMock.Setup(service => service.GetAllWorkoutLogsAsync()).ReturnsAsync(workoutLogs);

            // Act
            var result = await _controller.GetAllWorkoutLogsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(workoutLogs, okResult.Value);
        }
    }
}
