using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHubSystem.Data.Migrations
{
    public partial class ChangeNameHobbyUserHubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HubMembership_AspNetUsers_HobbyUserId",
                table: "HubMembership");

            migrationBuilder.DropForeignKey(
                name: "FK_HubMembership_Hubs_HubId",
                table: "HubMembership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HubMembership",
                table: "HubMembership");

            migrationBuilder.RenameTable(
                name: "HubMembership",
                newName: "HobbyUserHubs");

            migrationBuilder.RenameIndex(
                name: "IX_HubMembership_HubId",
                table: "HobbyUserHubs",
                newName: "IX_HobbyUserHubs_HubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HobbyUserHubs",
                table: "HobbyUserHubs",
                columns: new[] { "HobbyUserId", "HubId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HobbyUserHubs_AspNetUsers_HobbyUserId",
                table: "HobbyUserHubs",
                column: "HobbyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HobbyUserHubs_Hubs_HubId",
                table: "HobbyUserHubs",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HobbyUserHubs_AspNetUsers_HobbyUserId",
                table: "HobbyUserHubs");

            migrationBuilder.DropForeignKey(
                name: "FK_HobbyUserHubs_Hubs_HubId",
                table: "HobbyUserHubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HobbyUserHubs",
                table: "HobbyUserHubs");

            migrationBuilder.RenameTable(
                name: "HobbyUserHubs",
                newName: "HubMembership");

            migrationBuilder.RenameIndex(
                name: "IX_HobbyUserHubs_HubId",
                table: "HubMembership",
                newName: "IX_HubMembership_HubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HubMembership",
                table: "HubMembership",
                columns: new[] { "HobbyUserId", "HubId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HubMembership_AspNetUsers_HobbyUserId",
                table: "HubMembership",
                column: "HobbyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HubMembership_Hubs_HubId",
                table: "HubMembership",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
