using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBallByStats.Migrations
{
    public partial class creatingGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeam = table.Column<int>(type: "int", nullable: false),
                    GuestTeam = table.Column<int>(type: "int", nullable: false),
                    HomeTeamPoints = table.Column<int>(type: "int", nullable: false),
                    GuestTeamPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
