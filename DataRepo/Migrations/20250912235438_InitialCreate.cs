using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataRepo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskEfforts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEfforts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KimboTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Task = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false),
                    EffortId = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateOnly>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KimboTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KimboTasks_TaskEfforts_EffortId",
                        column: x => x.EffortId,
                        principalTable: "TaskEfforts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KimboTasks_EffortId",
                table: "KimboTasks",
                column: "EffortId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KimboTasks");

            migrationBuilder.DropTable(
                name: "TaskEfforts");
        }
    }
}
