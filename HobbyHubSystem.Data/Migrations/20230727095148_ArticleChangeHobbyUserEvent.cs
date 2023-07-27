using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHubSystem.Data.Migrations
{
    public partial class ArticleChangeHobbyUserEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HubId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "hobby to which the article belongs",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HubId",
                table: "Articles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "hobby to which the article belongs");
        }
    }
}
