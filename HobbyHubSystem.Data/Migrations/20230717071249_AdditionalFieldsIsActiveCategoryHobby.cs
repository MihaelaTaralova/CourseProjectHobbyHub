using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHubSystem.Data.Migrations
{
    public partial class AdditionalFieldsIsActiveCategoryHobby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Hobbies",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "when it is false - the hobby is deleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "when it is false - the category is deleted");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Hobbies");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Categories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
