using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballManager2000.Data.Migrations
{
    public partial class BusinessDataModel4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Members_PaidByMemberId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Members_PaidByMemberId",
                table: "Games",
                column: "PaidByMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Members_PaidByMemberId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Members_PaidByMemberId",
                table: "Games",
                column: "PaidByMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
