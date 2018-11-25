using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyToDo.Data.Migrations
{
    public partial class AddedRepeatingTimers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ToDoModel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "RepeatingTodoID",
                table: "ToDoModel",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RepeatingTodos",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    Modus = table.Column<int>(nullable: false),
                    RepeatingValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepeatingTodos", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoModel_RepeatingTodoID",
                table: "ToDoModel",
                column: "RepeatingTodoID");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoModel_RepeatingTodos_RepeatingTodoID",
                table: "ToDoModel",
                column: "RepeatingTodoID",
                principalTable: "RepeatingTodos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoModel_RepeatingTodos_RepeatingTodoID",
                table: "ToDoModel");

            migrationBuilder.DropTable(
                name: "RepeatingTodos");

            migrationBuilder.DropIndex(
                name: "IX_ToDoModel_RepeatingTodoID",
                table: "ToDoModel");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ToDoModel");

            migrationBuilder.DropColumn(
                name: "RepeatingTodoID",
                table: "ToDoModel");
        }
    }
}
