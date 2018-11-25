using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyToDo.Data.Migrations
{
    public partial class AddedMasterTodoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MasterTodoID",
                table: "RepeatingTodos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MasterTodoID",
                table: "RepeatingTodos");
        }
    }
}
