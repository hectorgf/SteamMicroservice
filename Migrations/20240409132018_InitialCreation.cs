using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredAge = table.Column<int>(type: "int", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutGame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapsuleImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapsuleImageV5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price_Initial = table.Column<long>(type: "bigint", nullable: true),
                    Price_Final = table.Column<long>(type: "bigint", nullable: true),
                    Price_Discount = table.Column<long>(type: "bigint", nullable: true),
                    Windows = table.Column<bool>(type: "bit", nullable: false),
                    MacOS = table.Column<bool>(type: "bit", nullable: false),
                    Linux = table.Column<bool>(type: "bit", nullable: false),
                    Recomendations = table.Column<long>(type: "bigint", nullable: false),
                    ReleaseDate_ComingSoon = table.Column<bool>(type: "bit", nullable: false),
                    ReleaseDate_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnedGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    appid = table.Column<int>(type: "int", nullable: false),
                    playtime_forever = table.Column<int>(type: "int", nullable: false),
                    playtime_windows_forever = table.Column<int>(type: "int", nullable: false),
                    playtime_mac_forever = table.Column<int>(type: "int", nullable: false),
                    playtime_linux_forever = table.Column<int>(type: "int", nullable: false),
                    playtime_deck_forever = table.Column<int>(type: "int", nullable: false),
                    rtime_last_played = table.Column<int>(type: "int", nullable: false),
                    playtime_disconnected = table.Column<int>(type: "int", nullable: false),
                    playtime_2weeks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    steamid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    communityvisibilitystate = table.Column<int>(type: "int", nullable: false),
                    profilestate = table.Column<int>(type: "int", nullable: false),
                    personaname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    commentpermission = table.Column<int>(type: "int", nullable: false),
                    profileurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatarmedium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatarfull = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatarhash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastlogoff = table.Column<int>(type: "int", nullable: false),
                    personastate = table.Column<int>(type: "int", nullable: false),
                    realname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    primaryclanid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timecreated = table.Column<int>(type: "int", nullable: false),
                    personastateflags = table.Column<int>(type: "int", nullable: false),
                    loccountrycode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    locstatecode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loccityid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Minimum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recomended = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirements_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screenshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamId = table.Column<long>(type: "bigint", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Full = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenshots_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SteamCategorySteamGame",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamCategorySteamGame", x => new { x.CategoriesId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_SteamCategorySteamGame_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SteamCategorySteamGame_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SteamDeveloperSteamGame",
                columns: table => new
                {
                    DevelopersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamDeveloperSteamGame", x => new { x.DevelopersId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_SteamDeveloperSteamGame_Developers_DevelopersId",
                        column: x => x.DevelopersId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SteamDeveloperSteamGame_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SteamGameSteamGenre",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamGameSteamGenre", x => new { x.GamesId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_SteamGameSteamGenre_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SteamGameSteamGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnedGamePlayer",
                columns: table => new
                {
                    OwnedGamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ownersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedGamePlayer", x => new { x.OwnedGamesId, x.ownersId });
                    table.ForeignKey(
                        name: "FK_OwnedGamePlayer_OwnedGames_OwnedGamesId",
                        column: x => x.OwnedGamesId,
                        principalTable: "OwnedGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnedGamePlayer_Players_ownersId",
                        column: x => x.ownersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SteamGameSteamPublisher",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublishersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamGameSteamPublisher", x => new { x.GamesId, x.PublishersId });
                    table.ForeignKey(
                        name: "FK_SteamGameSteamPublisher_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SteamGameSteamPublisher_Publishers_PublishersId",
                        column: x => x.PublishersId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnedGamePlayer_ownersId",
                table: "OwnedGamePlayer",
                column: "ownersId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_GameId",
                table: "Requirements",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenshots_GameId",
                table: "Screenshots",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SteamCategorySteamGame_GamesId",
                table: "SteamCategorySteamGame",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_SteamDeveloperSteamGame_GamesId",
                table: "SteamDeveloperSteamGame",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_SteamGameSteamGenre_GenresId",
                table: "SteamGameSteamGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_SteamGameSteamPublisher_PublishersId",
                table: "SteamGameSteamPublisher",
                column: "PublishersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnedGamePlayer");

            migrationBuilder.DropTable(
                name: "Requirements");

            migrationBuilder.DropTable(
                name: "Screenshots");

            migrationBuilder.DropTable(
                name: "SteamCategorySteamGame");

            migrationBuilder.DropTable(
                name: "SteamDeveloperSteamGame");

            migrationBuilder.DropTable(
                name: "SteamGameSteamGenre");

            migrationBuilder.DropTable(
                name: "SteamGameSteamPublisher");

            migrationBuilder.DropTable(
                name: "OwnedGames");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
