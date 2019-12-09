using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballManager2000.Data.Migrations
{
    public partial class BusinessDataModel7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Members_PaidByMemberId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "PaidByMemberId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Members_PaidByMemberId",
                table: "Games",
                column: "PaidByMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Members_PaidByMemberId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "PaidByMemberId",
                table: "Games",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Members_PaidByMemberId",
                table: "Games",
                column: "PaidByMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
