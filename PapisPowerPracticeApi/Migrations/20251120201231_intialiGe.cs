using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapisPowerPracticeApi.Migrations
{
    /// <inheritdoc />
    public partial class intialiGe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcerciseId",
                table: "WorkoutExercises");

            migrationBuilder.RenameColumn(
                name: "IsWarmUp",
                table: "WorkoutSet",
                newName: "IsWarmup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsWarmup",
                table: "WorkoutSet",
                newName: "IsWarmUp");

            migrationBuilder.AddColumn<int>(
                name: "ExcerciseId",
                table: "WorkoutExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
