using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultra_Saver.Migrations
{
    public partial class Recipe_TotalEnergy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "GasPrice",
                table: "User",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<double>(
                name: "TotalEnergy",
                table: "Recipes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "CookingPowerScale",
                table: "Ingredient",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GasPrice",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TotalEnergy",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CookingPowerScale",
                table: "Ingredient");
        }
    }
}
