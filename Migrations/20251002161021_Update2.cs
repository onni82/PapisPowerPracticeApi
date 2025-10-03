using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapisPowerPracticeApi.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercises_exerciseId",
                table: "WorkoutExercises");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "WorkoutLogs",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "exerciseId",
                table: "WorkoutExercises",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExercises_exerciseId",
                table: "WorkoutExercises",
                newName: "IX_WorkoutExercises_ExerciseId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "WorkoutLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "WorkoutLogs");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "WorkoutLogs",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "WorkoutExercises",
                newName: "exerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                newName: "IX_WorkoutExercises_exerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercises_exerciseId",
                table: "WorkoutExercises",
                column: "exerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
