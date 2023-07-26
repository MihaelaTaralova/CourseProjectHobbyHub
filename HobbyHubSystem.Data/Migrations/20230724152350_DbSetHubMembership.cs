using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHubSystem.Data.Migrations
{
    public partial class DbSetHubMembership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HobbyUserHub_AspNetUsers_HobbyUserId",
                table: "HobbyUserHub");

            migrationBuilder.DropForeignKey(
                name: "FK_HobbyUserHub_Hubs_HubId",
                table: "HobbyUserHub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HobbyUserHub",
                table: "HobbyUserHub");

            migrationBuilder.RenameTable(
                name: "HobbyUserHub",
                newName: "HubMembership");

            migrationBuilder.RenameIndex(
                name: "IX_HobbyUserHub_HubId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "HobbyUserHub");

            migrationBuilder.RenameIndex(
                name: "IX_HubMembership_HubId",
                table: "HobbyUserHub",
                newName: "IX_HobbyUserHub_HubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HobbyUserHub",
                table: "HobbyUserHub",
                columns: new[] { "HobbyUserId", "HubId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HobbyUserHub_AspNetUsers_HobbyUserId",
                table: "HobbyUserHub",
                column: "HobbyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HobbyUserHub_Hubs_HubId",
                table: "HobbyUserHub",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
