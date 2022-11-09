using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultra_Saver.Migrations
{
    public partial class recipe_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Recipe_RecipeId",
                table: "RecipeIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedRecipe_Recipe_RecipeId",
                table: "UserLikedRecipe");

            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "Recipe");

            migrationBuilder.RenameColumn(
                name: "Wattage",
                table: "Recipes",
                newName: "FullPrepTime");

            migrationBuilder.RenameColumn(
                name: "Minutes",
                table: "Recipes",
                newName: "CalorieCount");

            migrationBuilder.RenameColumn(
                name: "FullPrepTime",
                table: "Recipe",
                newName: "Wattage");

            migrationBuilder.RenameColumn(
                name: "CalorieCount",
                table: "Recipe",
                newName: "Minutes");

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "Recipes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Recipes_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedRecipe_Recipes_RecipeId",
                table: "UserLikedRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Recipes_RecipeId",
                table: "RecipeIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedRecipe_Recipes_RecipeId",
                table: "UserLikedRecipe");

            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "FullPrepTime",
                table: "Recipes",
                newName: "Wattage");

            migrationBuilder.RenameColumn(
                name: "CalorieCount",
                table: "Recipes",
                newName: "Minutes");

            migrationBuilder.RenameColumn(
                name: "Wattage",
                table: "Recipe",
                newName: "FullPrepTime");

            migrationBuilder.RenameColumn(
                name: "Minutes",
                table: "Recipe",
                newName: "CalorieCount");

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "Recipe",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Recipe_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedRecipe_Recipe_RecipeId",
                table: "UserLikedRecipe",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
