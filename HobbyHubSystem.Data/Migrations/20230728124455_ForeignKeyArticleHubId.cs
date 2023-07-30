using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHubSystem.Data.Migrations
{
    public partial class ForeignKeyArticleHubId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Hubs_HubId",
                table: "Articles");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Hubs_HubId",
                table: "Articles",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Hubs_HubId",
                table: "Articles");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Hubs_HubId",
                table: "Articles",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id");
        }
    }
}
