using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultra_Saver.Migrations
{
    public partial class Addedownertorecipies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_properties",
                table: "properties");

            migrationBuilder.RenameTable(
                name: "properties",
                newName: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Recipes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "properties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_properties",
                table: "properties",
                column: "email");
        }
    }
}
