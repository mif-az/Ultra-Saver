using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultra_Saver.Migrations
{
    public partial class UpdatedRecipeIngredientModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientName_IngredientCookin~",
                table: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_IngredientName_IngredientCookingMethod",
                table: "RecipeIngredient");

            migrationBuilder.AddColumn<int>(
                name: "IngredientAmount",
                table: "RecipeIngredient",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientAmount",
                table: "RecipeIngredient");

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    CookingMethod = table.Column<string>(type: "text", nullable: false),
                    CookingTime = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => new { x.Name, x.CookingMethod });
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientName_IngredientCookingMethod",
                table: "RecipeIngredient",
                columns: new[] { "IngredientName", "IngredientCookingMethod" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientName_IngredientCookin~",
                table: "RecipeIngredient",
                columns: new[] { "IngredientName", "IngredientCookingMethod" },
                principalTable: "Ingredient",
                principalColumns: new[] { "Name", "CookingMethod" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
