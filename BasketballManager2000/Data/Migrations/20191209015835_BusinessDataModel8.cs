using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballManager2000.Data.Migrations
{
    public partial class BusinessDataModel8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Members",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
