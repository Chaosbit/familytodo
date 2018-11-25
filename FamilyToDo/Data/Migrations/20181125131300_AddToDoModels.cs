using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyToDo.Data.Migrations
{
    public partial class AddToDoModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoList",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoList", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ToDoModel",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ToDoListID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ToDoModel_ToDoList_ToDoListID",
                        column: x => x.ToDoListID,
                        principalTable: "ToDoList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDoModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoModel_ToDoListID",
                table: "ToDoModel",
                column: "ToDoListID");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoModel_UserId",
                table: "ToDoModel",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoModel");

            migrationBuilder.DropTable(
                name: "ToDoList");
        }
    }
}
