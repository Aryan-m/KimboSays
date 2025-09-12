using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataRepo.Migrations
{
    /// <inheritdoc />
    public partial class Effort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Effort",
                table: "KimboTasks");

            migrationBuilder.AddColumn<int>(
                name: "EffortId",
                table: "KimboTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TaskEfforts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEfforts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KimboTasks_EffortId",
                table: "KimboTasks",
                column: "EffortId");

            migrationBuilder.AddForeignKey(
                name: "FK_KimboTasks_TaskEfforts_EffortId",
                table: "KimboTasks",
                column: "EffortId",
                principalTable: "TaskEfforts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KimboTasks_TaskEfforts_EffortId",
                table: "KimboTasks");

            migrationBuilder.DropTable(
                name: "TaskEfforts");

            migrationBuilder.DropIndex(
                name: "IX_KimboTasks_EffortId",
                table: "KimboTasks");

            migrationBuilder.DropColumn(
                name: "EffortId",
                table: "KimboTasks");

            migrationBuilder.AddColumn<int>(
                name: "Effort",
                table: "KimboTasks",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);
        }
    }
}
