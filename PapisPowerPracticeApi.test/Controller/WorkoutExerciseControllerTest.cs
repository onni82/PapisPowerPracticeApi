using Castle.Core.Configuration;
using FakeItEasy;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.Controllers;
using PapisPowerPracticeApi.DTOs.Exercise.Response;
using PapisPowerPracticeApi.DTOs.WorkoutExercise.Request;
using PapisPowerPracticeApi.DTOs.WorkoutExercise.Response;
using PapisPowerPracticeApi.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapisPowerPracticeApi.test.Controller
{
    public class WorkoutExerciseControllerTest
    {
        private readonly IWorkoutExerciseService _workOutService;
        private readonly WorkoutExerciseController _controller;
        public WorkoutExerciseControllerTest()
        {
            _workOutService = A.Fake<IWorkoutExerciseService>();
            _controller = new WorkoutExerciseController(_workOutService);
        }

        //[Fact]
        //public async Task CreateWorkoutExercise_ReturnOK()
        //{
        //    //Arrange
        //    var dto = new CreateWorkoutExerciseDTO { ExerciseId = 1, Reps = 1, Sets = 2, Weight = 2, WorkoutLogId = 1 };
        //    int returnId = 1;
        //    A.CallTo(() => _workOutService.CreateWorkoutExerciseAsync(dto))
        //        .Returns(Task.FromResult(returnId));

        //    //Act
        //    var actionResult = await _controller.CreateWorkoutExercise(dto);

        //    //Assert
        //    var result = Assert.IsType<OkObjectResult>(actionResult);
        //    Assert.Equal(returnId, result.Value);
        //}

        [Fact]
        public async Task GetWorkoutExerciseById_ReturnOk()
        {
            //Arrange

            A.CallTo(() => _workOutService.GetWorkoutExerciseByIdAsync(1)).Returns(new WorkoutExerciseDTO
            {
                Id = 1,
                ExerciseId = 1,
                Name = "Squats",
                Sets = new List<WorkoutSetDTO>()
            });

            //Act
            var result = await _controller.GetWorkoutExerciseById(1);
            //Assert
            var actionResult = Assert.IsType<ActionResult<WorkoutExerciseDTO>>(result);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedDto = Assert.IsType<WorkoutExerciseDTO>(okResult.Value);

            Assert.Equal(1, returnedDto.Id);
            Assert.Equal(1, returnedDto.ExerciseId);
            Assert.Equal("Squats", returnedDto.Name);


            A.CallTo(() => _workOutService.GetWorkoutExerciseByIdAsync(1))
                .MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task GetWorkoutExerciseId_ReturnNotFound()
        {
            // Arrange

            A.CallTo(() => _workOutService.GetWorkoutExerciseByIdAsync(1)).Returns(Task.FromResult<WorkoutExerciseDTO?>(null));


            // Act
            var result = await _controller.GetWorkoutExerciseById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WorkoutExerciseDTO>>(result);
            
            Assert.IsType<NotFoundResult>(actionResult.Result);

            A.CallTo(() => _workOutService.GetWorkoutExerciseByIdAsync(1))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteWorkoutExercise_ReturnsNoContent()
        {

            A.CallTo(() => _workOutService.DeleteWorkoutExerciseAsync(1)).Returns(true);

            var actionResult = await _controller.DeleteWorkoutExercise(1);

            Assert.IsType<NoContentResult>(actionResult);
        }
        [Fact]
        public async Task DeleteWorkoutExercise_ReturnsNotFound()
        {
            A.CallTo(() => _workOutService.DeleteWorkoutExerciseAsync(1)).Returns(false);

            var result = await _controller.DeleteWorkoutExercise(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
