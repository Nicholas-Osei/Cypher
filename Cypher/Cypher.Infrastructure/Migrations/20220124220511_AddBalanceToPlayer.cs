using Microsoft.EntityFrameworkCore.Migrations;

namespace Cypher.Infrastructure.Migrations
{
    public partial class AddBalanceToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Balance",
                table: "Players",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
