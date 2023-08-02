using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHubSystem.Data.Migrations
{
    public partial class AddHubIdEventColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Hubs_HubId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "HubId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "hub where the event belongs",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HubId",
                table: "Articles",
                type: "int",
                nullable: false,
                comment: "hub where the article belongs",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "hobby to which the article belongs");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Hubs_HubId",
                table: "Events",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Hubs_HubId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "HubId",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "hub where the event belongs");

            migrationBuilder.AlterColumn<int>(
                name: "HubId",
                table: "Articles",
                type: "int",
                nullable: false,
                comment: "hobby to which the article belongs",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "hub where the article belongs");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Hubs_HubId",
                table: "Events",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id");
        }
    }
}
