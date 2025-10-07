using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapisPowerPracticeApi.Migrations
{
    /// <inheritdoc />
    public partial class appuser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "WorkoutLogs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_ApplicationUserId",
                table: "WorkoutLogs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutLogs_AspNetUsers_ApplicationUserId",
                table: "WorkoutLogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutLogs_AspNetUsers_ApplicationUserId",
                table: "WorkoutLogs");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutLogs_ApplicationUserId",
                table: "WorkoutLogs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "WorkoutLogs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");
        }
    }
}
