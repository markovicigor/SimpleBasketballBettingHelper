using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBallByStats.Migrations
{
    public partial class addNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuestTeamName",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeTeamName",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestTeamName",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeamName",
                table: "Games");
        }
    }
}
