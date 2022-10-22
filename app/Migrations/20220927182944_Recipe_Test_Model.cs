using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultra_Saver.Migrations
{
    public partial class Recipe_Test_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "properties",
                columns: table => new
                {
                    email = table.Column<string>(type: "text", nullable: false),
                    darkMode = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_properties", x => x.email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "properties");
        }
    }
}
