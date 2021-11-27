﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cypher.Infrastructure.Migrations
{
    public partial class FixPreviousMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Players_PlayerId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_PlayerId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Players");

            migrationBuilder.CreateTable(
                name: "PlayerFriend",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    FriendId = table.Column<int>(type: "integer", nullable: false),
                    FriendId1 = table.Column<int>(type: "integer", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                        name: "FK_PlayerFriend_Players_FriendId1",
                        column: x => x.FriendId1,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFriend_FriendId",
                table: "PlayerFriend",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFriend_FriendId1",
                table: "PlayerFriend",
                column: "FriendId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerFriend");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerId",
                table: "Players",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Players_PlayerId",
                table: "Players",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
