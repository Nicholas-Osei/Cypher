using Microsoft.EntityFrameworkCore.Migrations;

namespace Cypher.Infrastructure.Migrations
{
    public partial class ReworkPlayerLobbyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessagePlayers_Messages_MessageId",
                table: "MessagePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_MessagePlayers_Players_PlayerId",
                table: "MessagePlayers");

            migrationBuilder.DropTable(
                name: "PlayerLobbies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessagePlayers",
                table: "MessagePlayers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "MessagePlayers",
                newName: "MessagePlayer");

            migrationBuilder.RenameIndex(
                name: "IX_MessagePlayers_PlayerId",
                table: "MessagePlayer",
                newName: "IX_MessagePlayer_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessagePlayer",
                table: "MessagePlayer",
                columns: new[] { "MessageId", "PlayerId" });

            migrationBuilder.CreateTable(
                name: "LobbyPlayer",
                columns: table => new
                {
                    LobbiesJoinedId = table.Column<int>(type: "integer", nullable: false),
                    PlayersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LobbyPlayer", x => new { x.LobbiesJoinedId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_LobbyPlayer_Lobbies_LobbiesJoinedId",
                        column: x => x.LobbiesJoinedId,
                        principalTable: "Lobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LobbyPlayer_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LobbyPlayer_PlayersId",
                table: "LobbyPlayer",
                column: "PlayersId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessagePlayer_Messages_MessageId",
                table: "MessagePlayer",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessagePlayer_Players_PlayerId",
                table: "MessagePlayer",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessagePlayer_Messages_MessageId",
                table: "MessagePlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_MessagePlayer_Players_PlayerId",
                table: "MessagePlayer");

            migrationBuilder.DropTable(
                name: "LobbyPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessagePlayer",
                table: "MessagePlayer");

            migrationBuilder.RenameTable(
                name: "MessagePlayer",
                newName: "MessagePlayers");

            migrationBuilder.RenameIndex(
                name: "IX_MessagePlayer_PlayerId",
                table: "MessagePlayers",
                newName: "IX_MessagePlayers_PlayerId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessagePlayers",
                table: "MessagePlayers",
                columns: new[] { "MessageId", "PlayerId" });

            migrationBuilder.CreateTable(
                name: "PlayerLobbies",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    LobbyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerLobbies", x => new { x.PlayerId, x.LobbyId });
                    table.ForeignKey(
                        name: "FK_PlayerLobbies_Lobbies_LobbyId",
                        column: x => x.LobbyId,
                        principalTable: "Lobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerLobbies_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLobbies_LobbyId",
                table: "PlayerLobbies",
                column: "LobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessagePlayers_Messages_MessageId",
                table: "MessagePlayers",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessagePlayers_Players_PlayerId",
                table: "MessagePlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
