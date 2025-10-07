using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapisPowerPracticeApi.Migrations
{
    /// <inheritdoc />
    public partial class jwt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_MuscleGroups_ExerciseId",
                table: "WorkoutExercises");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_MuscleGroups_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
