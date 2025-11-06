using Moq;
using PapisPowerPracticeApi.Controllers;
using PapisPowerPracticeApi.DTOs.WorkoutLog;
using PapisPowerPracticeApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
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
            
            // Mock user context for authorized endpoints
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id")
            }));
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
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
        
        [Fact]
        public async Task GetWorkoutLogById_ReturnsOk_WhenFound()
        {
            // Arrange
            var workoutLog = new GetWorkoutLogDTO { Id = 1, StartTime = DateTime.Now, EndTime = DateTime.Now, Notes = "Test 1"};

            _serviceMock.Setup(service => service.GetWorkoutLogByIdAsync(1)).ReturnsAsync(workoutLog);

            // Act
            var result = await _controller.GetWorkoutLogById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<GetWorkoutLogDTO>(okResult.Value);
            Assert.Equal("Test 1", dto.Notes);
        }
        
        [Fact]
        public async Task GetWorkoutLogById_ReturnsNotFound_WhenMissing()
        {
            // Arrange
            _serviceMock.Setup(service => service.GetWorkoutLogByIdAsync(99)).ReturnsAsync((GetWorkoutLogDTO?)null);

            // Act
            var result = await _controller.GetWorkoutLogById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        
        [Fact]
        public async Task CreateWorkoutLog_ReturnsOk_WhenValid()
        {
            // Arrange
            var input = new CreateWorkoutLogDTO
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Notes = "Test 1"
            };

            _serviceMock.Setup(s => s.CreateWorkoutLogAsync(It.IsAny<CreateWorkoutLogDTO>(), It.IsAny<string>())).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateWorkoutLog(input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }
        [Fact]
        public async Task DeleteWorkoutLog_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeleteWorkoutLogAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteWorkoutLog(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteWorkoutLog_ReturnsNotFound_WhenMissing()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeleteWorkoutLogAsync(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteWorkoutLog(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}