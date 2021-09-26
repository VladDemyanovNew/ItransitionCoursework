using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VDemyanov.MathWars.DAL.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MathProblems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Topic = table.Column<int>(type: "int", nullable: true),
                    TopicNavigationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MathProblems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MathProblems_Topics_TopicNavigationId",
                        column: x => x.TopicNavigationId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MathProblem = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MathProblemNavigationId = table.Column<int>(type: "int", nullable: true),
                    UserNavigationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_AspNetUsers_UserNavigationId",
                        column: x => x.UserNavigationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievements_MathProblems_MathProblemNavigationId",
                        column: x => x.MathProblemNavigationId,
                        principalTable: "MathProblems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MathProblem = table.Column<int>(type: "int", nullable: true),
                    MathProblemNavigationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_MathProblems_MathProblemNavigationId",
                        column: x => x.MathProblemNavigationId,
                        principalTable: "MathProblems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MathProblem = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MathProblemNavigationId = table.Column<int>(type: "int", nullable: true),
                    UserNavigationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserNavigationId",
                        column: x => x.UserNavigationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_MathProblems_MathProblemNavigationId",
                        column: x => x.MathProblemNavigationId,
                        principalTable: "MathProblems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MathProblem = table.Column<int>(type: "int", nullable: true),
                    MathProblemNavigationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_MathProblems_MathProblemNavigationId",
                        column: x => x.MathProblemNavigationId,
                        principalTable: "MathProblems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MathProblemTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<int>(type: "int", nullable: true),
                    MathProblem = table.Column<int>(type: "int", nullable: true),
                    MathProblemNavigationId = table.Column<int>(type: "int", nullable: true),
                    TagNavigationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MathProblemTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MathProblemTags_MathProblems_MathProblemNavigationId",
                        column: x => x.MathProblemNavigationId,
                        principalTable: "MathProblems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MathProblemTags_Tags_TagNavigationId",
                        column: x => x.TagNavigationId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_MathProblemNavigationId",
                table: "Achievements",
                column: "MathProblemNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_UserNavigationId",
                table: "Achievements",
                column: "UserNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_MathProblemNavigationId",
                table: "Answers",
                column: "MathProblemNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MathProblemNavigationId",
                table: "Comments",
                column: "MathProblemNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserNavigationId",
                table: "Comments",
                column: "UserNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MathProblemNavigationId",
                table: "Images",
                column: "MathProblemNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_MathProblems_TopicNavigationId",
                table: "MathProblems",
                column: "TopicNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_MathProblemTags_MathProblemNavigationId",
                table: "MathProblemTags",
                column: "MathProblemNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_MathProblemTags_TagNavigationId",
                table: "MathProblemTags",
                column: "TagNavigationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "MathProblemTags");

            migrationBuilder.DropTable(
                name: "MathProblems");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
