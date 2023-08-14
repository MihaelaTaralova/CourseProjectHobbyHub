using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHubSystem.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a29f172-6978-420f-a929-ca5678254935"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn" },
                values: new object[] { "11af791e-0233-4e00-8398-181007143a42", "AQAAAAEAACcQAAAAEFPfCPfynrcKhEwH+WsUja862MaIKsos9vDyzpKA+Ed9jwUaqjQRTr8gVg5JdGAd2w==", new DateTime(2023, 8, 14, 12, 29, 50, 248, DateTimeKind.Utc).AddTicks(1824) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5e2081c-5052-4162-b0d7-1920163d6b9d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn" },
                values: new object[] { "871c35f1-856a-480e-9f48-fad5e6839c2d", "AQAAAAEAACcQAAAAEBCpr++Rrwxg7Eu97ij+pi9s+58+v3yRfvtSxvywL+UR/BLRO+qOT/y+HmBl6B29Ng==", new DateTime(2023, 8, 14, 12, 29, 50, 245, DateTimeKind.Utc).AddTicks(8120) });

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://worldwaterskiers.com/wp-content/uploads/2019/06/im-waterskiing.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a29f172-6978-420f-a929-ca5678254935"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn" },
                values: new object[] { "a3044f67-c762-4c40-9a89-d9c0dbd0ba21", "AQAAAAEAACcQAAAAEAAXPpjMLbkI0W7o1IMG8kQLOQDlxlEt9ESIf+QuJ5IIiZRp+/vKoBQynL0y+AC5/g==", new DateTime(2023, 8, 14, 11, 47, 50, 689, DateTimeKind.Utc).AddTicks(9213) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5e2081c-5052-4162-b0d7-1920163d6b9d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn" },
                values: new object[] { "6ccb59ba-f8e2-4dad-8fce-0e33cb69c040", "AQAAAAEAACcQAAAAEO8797T74QxEt8GhEXbsL/F+E15pjEbHl+uqbhvYqtqrsZAIMc4E2KctUhoqqkQPXw==", new DateTime(2023, 8, 14, 11, 47, 50, 687, DateTimeKind.Utc).AddTicks(5694) });

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://en.wikipedia.org/wiki/Water_skiing#/media/File:Water_skiing_on_the_yarra02.jpg");
        }
    }
}
