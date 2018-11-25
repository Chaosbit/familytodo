using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyToDo.Data.Migrations
{
    public partial class AddedCompletedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Completed",
                table: "ToDoModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "ToDoModel");
        }
    }
}
