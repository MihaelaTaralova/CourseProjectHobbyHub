using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHubSystem.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "unique identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "name of the category"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "when it is false - the category is deleted"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "picture of the category")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "unique identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "name of the hub"),
                    About = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "short description of the hub"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "the creator of the group"),
                    HobbyId = table.Column<int>(type: "int", nullable: false, comment: "hobby to which this group belongs")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hubs_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "unique identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "title of the article"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false, comment: "content of the article"),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "the date on which the article is published"),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "author of the article"),
                    HubId = table.Column<int>(type: "int", nullable: false, comment: "hub where the article belongs"),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "before release the article should be approved by admin or moderator"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "when it is false - the article is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Hubs_HubId",
                        column: x => x.HubId,
                        principalTable: "Hubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscussionTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "unique identifier of the discussion topic")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "title of the discussion topic"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "the date when the topic is created"),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "author of the topic"),
                    HubId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscussionTopics_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionTopics_Hubs_HubId",
                        column: x => x.HubId,
                        principalTable: "Hubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "unique identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "name of the event"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "user who enters the event in the system"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false, comment: "details of the event"),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "date on which the event will happen"),
                    Location = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "location where the event will happen"),
                    HubId = table.Column<int>(type: "int", nullable: false, comment: "hub where the event belongs"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "when it is false - the event is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Hubs_HubId",
                        column: x => x.HubId,
                        principalTable: "Hubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "unique identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "name of the hobby"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "text with detailed description about the hobby"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "person who descibed the hobby in the system"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "category to which the hobby belongs"),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "before release the hobby should be approved by admin or moderator"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "when it is false - the hobby is deleted"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "picture of the hobby"),
                    HubId = table.Column<int>(type: "int", nullable: false, comment: "this is the group which belongs to the hobby")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hobbies_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hobbies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hobbies_Hubs_HubId",
                        column: x => x.HubId,
                        principalTable: "Hubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HobbyUserHubs",
                columns: table => new
                {
                    HobbyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyUserHubs", x => new { x.HobbyUserId, x.HubId });
                    table.ForeignKey(
                        name: "FK_HobbyUserHubs_AspNetUsers_HobbyUserId",
                        column: x => x.HobbyUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HobbyUserHubs_Hubs_HubId",
                        column: x => x.HubId,
                        principalTable: "Hubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "unique identifier of the question")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false, comment: "the body of the question"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "the date when the question is asked"),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "the creator of the question"),
                    DiscussionTopicId = table.Column<int>(type: "int", nullable: false, comment: "the discussion to which the question belongs"),
                    Views = table.Column<int>(type: "int", nullable: false, comment: "the count of users who saw the question")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_DiscussionTopics_DiscussionTopicId",
                        column: x => x.DiscussionTopicId,
                        principalTable: "DiscussionTopics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HobbyUserEvents",
                columns: table => new
                {
                    HobbyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyUserEvents", x => new { x.HobbyUserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_HobbyUserEvents_AspNetUsers_HobbyUserId",
                        column: x => x.HobbyUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HobbyUserEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "unique identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "this is the author of the question"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false, comment: "this is the body of the answer"),
                    QuestionId = table.Column<int>(type: "int", nullable: false, comment: "this is the question"),
                    RepliedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "the date when the answer is given"),
                    DiscussionTopicId = table.Column<int>(type: "int", nullable: false, comment: "the topic where the question belongs")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_DiscussionTopics_DiscussionTopicId",
                        column: x => x.DiscussionTopicId,
                        principalTable: "DiscussionTopics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "ImageUrl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegisteredOn", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2a29f172-6978-420f-a929-ca5678254935"), 0, "a3044f67-c762-4c40-9a89-d9c0dbd0ba21", "sami@abv.bg", true, "Sami", "male", "https://www.taylorherring.com/wp-content/uploads/2015/03/Archetypal-Male-Face-of-Beauty-embargoed-to-00.01hrs-30.03.15.jpg", "Sam4ov", false, null, "SAMI@ABV.BG", "SAMI SAM4OV", "AQAAAAEAACcQAAAAEAAXPpjMLbkI0W7o1IMG8kQLOQDlxlEt9ESIf+QuJ5IIiZRp+/vKoBQynL0y+AC5/g==", null, false, new DateTime(2023, 8, 14, 11, 47, 50, 689, DateTimeKind.Utc).AddTicks(9213), "", false, "Sami Sam4ov" },
                    { new Guid("c5e2081c-5052-4162-b0d7-1920163d6b9d"), 0, "6ccb59ba-f8e2-4dad-8fce-0e33cb69c040", "mihaela@abv.bg", true, "Mihaela", "female", "https://www.taylorherring.com/wp-content/uploads/2015/03/Archetypal-Female-Face-of-Beauty-embargoed-to-00.01hrs-30.03.15.jpg", "Mihael4ov", false, null, "MIHAELA@ABV.BG", "MIHAELA MIHAEL4OV", "AQAAAAEAACcQAAAAEO8797T74QxEt8GhEXbsL/F+E15pjEbHl+uqbhvYqtqrsZAIMc4E2KctUhoqqkQPXw==", null, false, new DateTime(2023, 8, 14, 11, 47, 50, 687, DateTimeKind.Utc).AddTicks(5694), "", false, "Mihaela Mihael4ov" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ImageUrl", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "https://dawnmagazines.com/wp-content/uploads/2020/10/Water-Sports.jpg", true, "Water" },
                    { 2, "https://upload.wikimedia.org/wikipedia/commons/e/e1/Pilates_Moscow.jpg", true, "Spiritual and Mental" }
                });

            migrationBuilder.InsertData(
                table: "Hubs",
                columns: new[] { "Id", "About", "CreatorId", "HobbyId", "Name" },
                values: new object[] { 1, "This hub belongs to hobby Water skiing and all articles, events and discussions are connected with Water skiing", new Guid("c5e2081c-5052-4162-b0d7-1920163d6b9d"), 1, "Water skiing" });

            migrationBuilder.InsertData(
                table: "Hubs",
                columns: new[] { "Id", "About", "CreatorId", "HobbyId", "Name" },
                values: new object[] { 2, "This hub belongs to hobby Reiki and all articles, events and discussions are connected with Reiki", new Guid("2a29f172-6978-420f-a929-ca5678254935"), 2, "Reiki" });

            migrationBuilder.InsertData(
                table: "Hobbies",
                columns: new[] { "Id", "CategoryId", "CreatorId", "Description", "HubId", "ImageUrl", "IsActive", "IsApproved", "Name" },
                values: new object[] { 1, 1, new Guid("c5e2081c-5052-4162-b0d7-1920163d6b9d"), "Water skiing (also waterskiing or water-skiing) is a surface water sport in which an individual is pulled behind a boat or a cable ski installation over a body of water, skimming the surface on two skis or one ski.", 1, "https://en.wikipedia.org/wiki/Water_skiing#/media/File:Water_skiing_on_the_yarra02.jpg", true, true, "Water skiing" });

            migrationBuilder.InsertData(
                table: "Hobbies",
                columns: new[] { "Id", "CategoryId", "CreatorId", "Description", "HubId", "ImageUrl", "IsActive", "IsApproved", "Name" },
                values: new object[] { 2, 2, new Guid("2a29f172-6978-420f-a929-ca5678254935"), "Reiki is a form of energy healing. Its roots can be traced back to the 1920’s in Japan. Pronounced “ray-kee”, Reiki can be roughly translated to mean “Universal Life Force” where Rei means Universal and Ki is Life force. ", 2, "https://www.tododisca.com/wp-content/uploads/2020/08/sesion-de-reiki-2-1.jpg", true, true, "Reiki" });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AuthorId",
                table: "Answers",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_DiscussionTopicId",
                table: "Answers",
                column: "DiscussionTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_HubId",
                table: "Articles",
                column: "HubId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionTopics_AuthorId",
                table: "DiscussionTopics",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionTopics_HubId",
                table: "DiscussionTopics",
                column: "HubId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_HubId",
                table: "Events",
                column: "HubId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_CategoryId",
                table: "Hobbies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_CreatorId",
                table: "Hobbies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_HubId",
                table: "Hobbies",
                column: "HubId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HobbyUserEvents_EventId",
                table: "HobbyUserEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_HobbyUserHubs_HubId",
                table: "HobbyUserHubs",
                column: "HubId");

            migrationBuilder.CreateIndex(
                name: "IX_Hubs_CreatorId",
                table: "Hubs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AuthorId",
                table: "Questions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_DiscussionTopicId",
                table: "Questions",
                column: "DiscussionTopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "HobbyUserEvents");

            migrationBuilder.DropTable(
                name: "HobbyUserHubs");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "DiscussionTopics");

            migrationBuilder.DropTable(
                name: "Hubs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
