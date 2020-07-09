using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Covid_World.EFDataAccessLibrary.Migrations
{
    public partial class Covid19W : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "coviddatas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    Time = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    Case_new = table.Column<string>(unicode: false, maxLength: 45, nullable: true),
                    Case_Active = table.Column<string>(unicode: false, maxLength: 45, nullable: true),
                    Case_Critical = table.Column<string>(unicode: false, maxLength: 45, nullable: true),
                    Case_Recovered = table.Column<string>(unicode: false, maxLength: 45, nullable: true),
                    Case_Total = table.Column<string>(unicode: false, maxLength: 45, nullable: true),
                    Death_new = table.Column<string>(unicode: false, maxLength: 45, nullable: true),
                    Death_Total = table.Column<string>(unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coviddatas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 45, nullable: true),
                    Cognome = table.Column<string>(unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coviddatas");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
