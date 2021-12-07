using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cypher.Infrastructure.Migrations
{
    public partial class SimplifiedFriendRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerFriend");

            migrationBuilder.CreateTable(
                name: "PlayerPlayer",
                columns: table => new
                {
                    FriendsId = table.Column<int>(type: "integer", nullable: false),
                    PlayersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPlayer", x => new { x.FriendsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_PlayerPlayer_Players_FriendsId",
                        column: x => x.FriendsId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPlayer_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPlayer_PlayersId",
                table: "PlayerPlayer",
                column: "PlayersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPlayer");

            migrationBuilder.CreateTable(
                name: "PlayerFriend",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    FriendId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFriend", x => new { x.PlayerId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_PlayerFriend_Players_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerFriend_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFriend_FriendId",
                table: "PlayerFriend",
                column: "FriendId");
        }
    }
}
