using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHubSystem.Data.Migrations
{
    public partial class EventConfigurationHubEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Hubs_HubId",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Hubs_HubId",
                table: "Events",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Hubs_HubId",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Hubs_HubId",
                table: "Events",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
