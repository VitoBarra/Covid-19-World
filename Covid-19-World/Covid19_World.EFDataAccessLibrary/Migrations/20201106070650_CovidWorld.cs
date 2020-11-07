using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid_World.EFDataAccessLibrary.Migrations
{
    public partial class CovidWorld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CovidDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(nullable: false),
                    Time = table.Column<string>(nullable: false),
                    CaseNew = table.Column<string>(maxLength: 45, nullable: true),
                    CaseActive = table.Column<string>(maxLength: 45, nullable: true),
                    CaseCritical = table.Column<string>(maxLength: 45, nullable: true),
                    CaseRecovered = table.Column<string>(maxLength: 45, nullable: true),
                    CaseTotal = table.Column<string>(maxLength: 45, nullable: true),
                    DeathNew = table.Column<string>(maxLength: 45, nullable: true),
                    DeathTotal = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CovidDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 45, nullable: true),
                    Nome = table.Column<string>(maxLength: 45, nullable: true),
                    Cognome = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CovidDatas");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
