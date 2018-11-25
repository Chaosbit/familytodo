using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyToDo.Data.Migrations
{
    public partial class MovedUserFieldToList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoModel_AspNetUsers_UserId",
                table: "ToDoModel");

            migrationBuilder.DropIndex(
                name: "IX_ToDoModel_UserId",
                table: "ToDoModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDoModel");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "ToDoList",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoList_UserID",
                table: "ToDoList",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoList_AspNetUsers_UserID",
                table: "ToDoList",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoList_AspNetUsers_UserID",
                table: "ToDoList");

            migrationBuilder.DropIndex(
                name: "IX_ToDoList_UserID",
                table: "ToDoList");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "ToDoList");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ToDoModel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoModel_UserId",
                table: "ToDoModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoModel_AspNetUsers_UserId",
                table: "ToDoModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
