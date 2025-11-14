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
    public class PapisPowerPracticeControllerTest
    {
        private readonly IWorkoutExerciseService _workOutService;
        private readonly WorkoutExerciseController _controller;
        public PapisPowerPracticeControllerTest()
        {
            _workOutService = A.Fake<IWorkoutExerciseService>();
            _controller = new WorkoutExerciseController(_workOutService);
        }

        [Fact]
        public async Task CreateWorkoutExercise_ReturnOK()
        {
            //Arrange
            var dto = new CreateWorkoutExerciseDTO { ExerciseId = 1, Reps = 1, Sets = 2, Weight = 2, WorkoutLogId = 1 };
            int returnId = 1;
            A.CallTo(() => _workOutService.CreateWorkoutExerciseAsync(dto))
                .Returns(Task.FromResult(returnId));

            //Act
            var actionResult = await _controller.CreateWorkoutExercise(dto);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(returnId, result.Value);
        }

        [Fact]
        public async Task GetWorkoutExerciseById_ReturnOk()
        {
            //Arrange
            var id = 1;
            var workoutDTO = new WorkoutExerciseDTO { Id = 1, ExerciseId = 1, Name = "Axelpress", Reps = 1, Sets = 1, Weight = 50 };

            A.CallTo(() => _workOutService.GetWorkoutExerciseByIdAsync(1)).Returns(Task.FromResult(workoutDTO));


            //Act
            var actionResult = await _controller.GetWorkoutExerciseById(id);
            //Assert

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedDto = Assert.IsType<WorkoutExerciseDTO>(okResult.Value);

            Assert.Equal(workoutDTO.Id, returnedDto.Id);
            Assert.Equal(workoutDTO.ExerciseId, returnedDto.ExerciseId);
            Assert.Equal(workoutDTO.Name, returnedDto.Name);
            Assert.Equal(workoutDTO.Reps, returnedDto.Reps);
            Assert.Equal(workoutDTO.Sets, returnedDto.Sets);
            Assert.Equal(workoutDTO.Weight, returnedDto.Weight);
        }

        [Fact]
        public async Task DeleteWorkoutExercise_ReturnsNoContent()
        {
            A.CallTo(() => _workOutService.DeleteWorkoutExerciseAsync(1)).Returns(true);

            var actionResult = await _controller.DeleteWorkoutExercise(1);

            Assert.IsType<NoContentResult>(actionResult);
        }
    }
}
