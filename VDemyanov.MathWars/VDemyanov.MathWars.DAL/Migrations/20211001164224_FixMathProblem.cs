using Microsoft.EntityFrameworkCore.Migrations;

namespace VDemyanov.MathWars.DAL.Migrations
{
    public partial class FixMathProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MathProblems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MathProblems_UserId",
                table: "MathProblems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MathProblems_AspNetUsers_UserId",
                table: "MathProblems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MathProblems_AspNetUsers_UserId",
                table: "MathProblems");

            migrationBuilder.DropIndex(
                name: "IX_MathProblems_UserId",
                table: "MathProblems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MathProblems");
        }
    }
}
