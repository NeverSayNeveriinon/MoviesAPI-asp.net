using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "dbo",
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
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
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
                name: "Genres",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<byte>(type: "tinyint", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GenderName = table.Column<byte>(type: "tinyint", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    JobName = table.Column<byte>(type: "tinyint", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "dbo",
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
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "dbo",
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
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "dbo",
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
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PublishYear = table.Column<int>(type: "int", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Languages = table.Column<int>(type: "int", nullable: true),
                    IMDBPage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IMDBRating = table.Column<double>(type: "float", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    DirectorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PublishChannel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EpisodesCount = table.Column<int>(type: "int", nullable: true),
                    SeasonsCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Shows_Persons_DirectorID",
                        column: x => x.DirectorID,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesDirectorsJoin",
                schema: "dbo",
                columns: table => new
                {
                    DirectorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesDirectorsJoin", x => new { x.DirectorID, x.SerialID });
                    table.ForeignKey(
                        name: "FK_SeriesDirectorsJoin_Persons_DirectorID",
                        column: x => x.DirectorID,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SeriesDirectorsJoin_Shows_SerialID",
                        column: x => x.SerialID,
                        principalSchema: "dbo",
                        principalTable: "Shows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShowsArtistsJoin",
                schema: "dbo",
                columns: table => new
                {
                    ArtistID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowsArtistsJoin", x => new { x.ArtistID, x.ShowID });
                    table.ForeignKey(
                        name: "FK_ShowsArtistsJoin_Persons_ArtistID",
                        column: x => x.ArtistID,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShowsArtistsJoin_Shows_ShowID",
                        column: x => x.ShowID,
                        principalSchema: "dbo",
                        principalTable: "Shows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShowsGenresJoin",
                schema: "dbo",
                columns: table => new
                {
                    GenreID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowsGenresJoin", x => new { x.GenreID, x.ShowID });
                    table.ForeignKey(
                        name: "FK_ShowsGenresJoin_Genres_GenreID",
                        column: x => x.GenreID,
                        principalSchema: "dbo",
                        principalTable: "Genres",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShowsGenresJoin_Shows_ShowID",
                        column: x => x.ShowID,
                        principalSchema: "dbo",
                        principalTable: "Shows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShowsWritersJoin",
                schema: "dbo",
                columns: table => new
                {
                    WriterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowsWritersJoin", x => new { x.ShowID, x.WriterID });
                    table.ForeignKey(
                        name: "FK_ShowsWritersJoin_Persons_WriterID",
                        column: x => x.WriterID,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShowsWritersJoin_Shows_ShowID",
                        column: x => x.ShowID,
                        principalSchema: "dbo",
                        principalTable: "Shows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("9298e4a1-35e7-4702-a299-d1dbd68e18c8"), "7a4a539a-5eea-4fad-b198-cb300b77ddfb", "User", "USER" },
                    { new Guid("9fa250cf-7c4a-4547-bc3e-793886ee3333"), "7a58dcf2-3449-4827-a268-b253c86ef9de", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Genres",
                columns: new[] { "ID", "Name", "Summary" },
                values: new object[,]
                {
                    { new Guid("4f803afc-54bd-4ce7-a7c9-5e01801dc1d0"), (byte)9, "a show genre called Action" },
                    { new Guid("69f4ac8f-dfc1-4597-811b-7a7853540b91"), (byte)2, "a show genre called Thriller" },
                    { new Guid("81e8f391-5290-49db-a14d-3ddcdf0c0307"), (byte)6, "a show genre called Mystery" },
                    { new Guid("bf8c2d12-5271-4d54-ad9f-04c655a8657e"), (byte)0, "a show genre called Drama" },
                    { new Guid("c4535ccd-b984-46b5-a440-de61612684d0"), (byte)8, "a show genre called Sci_Fi" },
                    { new Guid("e51325d4-34f0-42b0-9ac5-a92fc028da7a"), (byte)7, "a show genre called Crime" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Persons",
                columns: new[] { "ID", "DateOfBirth", "FirstName", "GenderName", "JobName", "LastName", "Summary" },
                values: new object[,]
                {
                    { new Guid("063565a3-5879-4226-8a73-44105188ccbf"), new DateOnly(1969, 9, 14), "Bong", (byte)0, (byte)2, "Joon-ho", "this is Joon-ho as writer" },
                    { new Guid("24e28be2-d38a-43a0-8c4f-7112728e82ea"), new DateOnly(1963, 12, 18), "Brad", (byte)0, (byte)0, "Pitt", "this is Pitt" },
                    { new Guid("26fd500a-2a6b-454c-9d63-090d6208181d"), new DateOnly(1970, 7, 30), "Christopher", (byte)0, (byte)1, "Nolan", "this is Chris Nolan as director" },
                    { new Guid("63e90859-dbce-4436-877c-f5c4731c1ce2"), new DateOnly(1982, 11, 12), "Anne", (byte)1, (byte)0, "Hathaway", "this is Hathaway" },
                    { new Guid("73308ea6-f6f4-41c8-928b-ee6a4832357c"), new DateOnly(1970, 7, 30), "Christopher", (byte)0, (byte)2, "Nolan", "this is Chris Nolan as writer" },
                    { new Guid("76b25d7d-508e-4bc6-bccc-d696e2c08516"), new DateOnly(1964, 8, 14), "Andrew", (byte)0, (byte)2, " Kevin Walker", "this is Walker" },
                    { new Guid("9c4ee8f2-2919-48a3-9729-fdce1248da14"), new DateOnly(1969, 9, 14), "Bong", (byte)0, (byte)1, "Joon-ho", "this is Joon-ho as director" },
                    { new Guid("ac0e7576-52cf-42ec-b56e-035b9d72177e"), new DateOnly(1974, 1, 30), "Christian", (byte)0, (byte)0, "Bale", "this is Bale" },
                    { new Guid("b165c22a-cbb8-4fe9-8697-13d5400379b0"), new DateOnly(1962, 8, 28), "David", (byte)0, (byte)1, "Fincher", "this is Fincher" },
                    { new Guid("e5f623df-90df-4fab-831a-ce5547812810"), new DateOnly(1976, 6, 6), "Jonathan", (byte)0, (byte)2, "Nolan", "this is Jon Nolan" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Shows",
                columns: new[] { "ID", "CountryName", "Discriminator", "EpisodesCount", "IMDBPage", "IMDBRating", "ImagePath", "Languages", "Name", "PublishChannel", "PublishYear", "SeasonsCount", "Summary" },
                values: new object[] { new Guid("b8de724f-9910-4686-b7a0-7f79be0ae604"), "USA", "Serial", 19, "page11", 8.5999999999999996, "path11", 1, "Mindhunter", "Netflix", 2017, 2, "A series about serial killers" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SeriesDirectorsJoin",
                columns: new[] { "DirectorID", "SerialID" },
                values: new object[] { new Guid("b165c22a-cbb8-4fe9-8697-13d5400379b0"), new Guid("b8de724f-9910-4686-b7a0-7f79be0ae604") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Shows",
                columns: new[] { "ID", "CountryName", "DirectorID", "Discriminator", "IMDBPage", "IMDBRating", "ImagePath", "Languages", "Name", "PublishYear", "Summary", "Time" },
                values: new object[,]
                {
                    { new Guid("00cb4ff8-1cad-4da3-afc6-fd36cda5b1af"), "USA", new Guid("26fd500a-2a6b-454c-9d63-090d6208181d"), "Movie", "page3", 8.4000000000000004, "path3", 1, "Memento", 2000, "A movie about past memories", new TimeOnly(1, 53, 0) },
                    { new Guid("499ac217-491c-44e6-95a7-ecaaccf98c68"), "USA", new Guid("26fd500a-2a6b-454c-9d63-090d6208181d"), "Movie", "page2", 8.6999999999999993, "path2", 1, "Interstellar", 2014, "A movie about universe", new TimeOnly(2, 49, 0) },
                    { new Guid("5b0480af-6013-423d-8b20-90a979a87f10"), "USA", new Guid("b165c22a-cbb8-4fe9-8697-13d5400379b0"), "Movie", "page7", 8.8000000000000007, "path7", 1, "Fight Club", 1999, "A movie about ... (just perfect)", new TimeOnly(2, 19, 0) },
                    { new Guid("5e8887ac-fd4e-46c4-8f16-62e6ac1aad2a"), "USA", new Guid("b165c22a-cbb8-4fe9-8697-13d5400379b0"), "Movie", "page6", 8.5999999999999996, "path6", 1, "Seven", 1995, "A movie about guilt", new TimeOnly(2, 7, 0) },
                    { new Guid("a95591f5-dd0f-4a4b-9cec-038092363c56"), "USA", new Guid("26fd500a-2a6b-454c-9d63-090d6208181d"), "Movie", "page5", 9.0, "path5", 1, "The Dark Knight", 2008, "A movie about batman", new TimeOnly(2, 32, 0) },
                    { new Guid("bfce8308-86eb-4bc6-9af6-42eb169d6590"), "USA", new Guid("26fd500a-2a6b-454c-9d63-090d6208181d"), "Movie", "page4", 8.5, "path4", 1, "Prestige", 2006, "A movie about magic", new TimeOnly(2, 10, 0) },
                    { new Guid("d4a28b46-b993-4da7-a95c-10a649e32e18"), "South Korea", new Guid("9c4ee8f2-2919-48a3-9729-fdce1248da14"), "Movie", "page8", 8.0999999999999996, "path8", 8, "Memories of Murder", 2003, "A movie about murder", new TimeOnly(2, 12, 0) },
                    { new Guid("fd9de4b8-4334-437e-a398-30dfc8b6c414"), "USA", new Guid("26fd500a-2a6b-454c-9d63-090d6208181d"), "Movie", "page1", 8.8000000000000007, "path1", 1, "Inception", 2010, "A movie about time", new TimeOnly(2, 28, 0) }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ShowsArtistsJoin",
                columns: new[] { "ArtistID", "ShowID" },
                values: new object[,]
                {
                    { new Guid("24e28be2-d38a-43a0-8c4f-7112728e82ea"), new Guid("5b0480af-6013-423d-8b20-90a979a87f10") },
                    { new Guid("24e28be2-d38a-43a0-8c4f-7112728e82ea"), new Guid("5e8887ac-fd4e-46c4-8f16-62e6ac1aad2a") },
                    { new Guid("63e90859-dbce-4436-877c-f5c4731c1ce2"), new Guid("499ac217-491c-44e6-95a7-ecaaccf98c68") },
                    { new Guid("ac0e7576-52cf-42ec-b56e-035b9d72177e"), new Guid("a95591f5-dd0f-4a4b-9cec-038092363c56") },
                    { new Guid("ac0e7576-52cf-42ec-b56e-035b9d72177e"), new Guid("bfce8308-86eb-4bc6-9af6-42eb169d6590") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ShowsGenresJoin",
                columns: new[] { "GenreID", "ShowID" },
                values: new object[,]
                {
                    { new Guid("4f803afc-54bd-4ce7-a7c9-5e01801dc1d0"), new Guid("5b0480af-6013-423d-8b20-90a979a87f10") },
                    { new Guid("4f803afc-54bd-4ce7-a7c9-5e01801dc1d0"), new Guid("a95591f5-dd0f-4a4b-9cec-038092363c56") },
                    { new Guid("4f803afc-54bd-4ce7-a7c9-5e01801dc1d0"), new Guid("fd9de4b8-4334-437e-a398-30dfc8b6c414") },
                    { new Guid("69f4ac8f-dfc1-4597-811b-7a7853540b91"), new Guid("00cb4ff8-1cad-4da3-afc6-fd36cda5b1af") },
                    { new Guid("69f4ac8f-dfc1-4597-811b-7a7853540b91"), new Guid("5b0480af-6013-423d-8b20-90a979a87f10") },
                    { new Guid("69f4ac8f-dfc1-4597-811b-7a7853540b91"), new Guid("bfce8308-86eb-4bc6-9af6-42eb169d6590") },
                    { new Guid("69f4ac8f-dfc1-4597-811b-7a7853540b91"), new Guid("d4a28b46-b993-4da7-a95c-10a649e32e18") },
                    { new Guid("81e8f391-5290-49db-a14d-3ddcdf0c0307"), new Guid("00cb4ff8-1cad-4da3-afc6-fd36cda5b1af") },
                    { new Guid("81e8f391-5290-49db-a14d-3ddcdf0c0307"), new Guid("5e8887ac-fd4e-46c4-8f16-62e6ac1aad2a") },
                    { new Guid("c4535ccd-b984-46b5-a440-de61612684d0"), new Guid("499ac217-491c-44e6-95a7-ecaaccf98c68") },
                    { new Guid("c4535ccd-b984-46b5-a440-de61612684d0"), new Guid("bfce8308-86eb-4bc6-9af6-42eb169d6590") },
                    { new Guid("c4535ccd-b984-46b5-a440-de61612684d0"), new Guid("fd9de4b8-4334-437e-a398-30dfc8b6c414") },
                    { new Guid("e51325d4-34f0-42b0-9ac5-a92fc028da7a"), new Guid("5e8887ac-fd4e-46c4-8f16-62e6ac1aad2a") },
                    { new Guid("e51325d4-34f0-42b0-9ac5-a92fc028da7a"), new Guid("a95591f5-dd0f-4a4b-9cec-038092363c56") },
                    { new Guid("e51325d4-34f0-42b0-9ac5-a92fc028da7a"), new Guid("d4a28b46-b993-4da7-a95c-10a649e32e18") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ShowsWritersJoin",
                columns: new[] { "ShowID", "WriterID" },
                values: new object[,]
                {
                    { new Guid("00cb4ff8-1cad-4da3-afc6-fd36cda5b1af"), new Guid("e5f623df-90df-4fab-831a-ce5547812810") },
                    { new Guid("499ac217-491c-44e6-95a7-ecaaccf98c68"), new Guid("73308ea6-f6f4-41c8-928b-ee6a4832357c") },
                    { new Guid("499ac217-491c-44e6-95a7-ecaaccf98c68"), new Guid("e5f623df-90df-4fab-831a-ce5547812810") },
                    { new Guid("5b0480af-6013-423d-8b20-90a979a87f10"), new Guid("76b25d7d-508e-4bc6-bccc-d696e2c08516") },
                    { new Guid("5e8887ac-fd4e-46c4-8f16-62e6ac1aad2a"), new Guid("76b25d7d-508e-4bc6-bccc-d696e2c08516") },
                    { new Guid("a95591f5-dd0f-4a4b-9cec-038092363c56"), new Guid("73308ea6-f6f4-41c8-928b-ee6a4832357c") },
                    { new Guid("a95591f5-dd0f-4a4b-9cec-038092363c56"), new Guid("e5f623df-90df-4fab-831a-ce5547812810") },
                    { new Guid("bfce8308-86eb-4bc6-9af6-42eb169d6590"), new Guid("73308ea6-f6f4-41c8-928b-ee6a4832357c") },
                    { new Guid("bfce8308-86eb-4bc6-9af6-42eb169d6590"), new Guid("e5f623df-90df-4fab-831a-ce5547812810") },
                    { new Guid("d4a28b46-b993-4da7-a95c-10a649e32e18"), new Guid("063565a3-5879-4226-8a73-44105188ccbf") },
                    { new Guid("fd9de4b8-4334-437e-a398-30dfc8b6c414"), new Guid("73308ea6-f6f4-41c8-928b-ee6a4832357c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "dbo",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "dbo",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "dbo",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "dbo",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesDirectorsJoin_SerialID",
                schema: "dbo",
                table: "SeriesDirectorsJoin",
                column: "SerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_DirectorID",
                schema: "dbo",
                table: "Shows",
                column: "DirectorID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowsArtistsJoin_ShowID",
                schema: "dbo",
                table: "ShowsArtistsJoin",
                column: "ShowID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowsGenresJoin_ShowID",
                schema: "dbo",
                table: "ShowsGenresJoin",
                column: "ShowID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowsWritersJoin_WriterID",
                schema: "dbo",
                table: "ShowsWritersJoin",
                column: "WriterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SeriesDirectorsJoin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ShowsArtistsJoin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ShowsGenresJoin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ShowsWritersJoin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Shows",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "dbo");
        }
    }
}
