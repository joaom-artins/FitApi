using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_ForId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_ForId",
                table: "Workouts");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ForId",
                table: "Workouts",
                column: "ForId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_ForId",
                table: "Workouts",
                column: "ForId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_ForId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_ForId",
                table: "Workouts");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ForId",
                table: "Workouts",
                column: "ForId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_ForId",
                table: "Workouts",
                column: "ForId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
