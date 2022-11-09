using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultra_Saver.Migrations
{
    public partial class manytomany_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredientModel_Ingredient_IngredientName_IngredientC~",
                table: "RecipeIngredientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredientModel_Recipe_RecipeId",
                table: "RecipeIngredientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOwnedApplianceModel_Appliance_ApplianceId",
                table: "UserOwnedApplianceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOwnedApplianceModel_User_UserEmail",
                table: "UserOwnedApplianceModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOwnedApplianceModel",
                table: "UserOwnedApplianceModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredientModel",
                table: "RecipeIngredientModel");

            migrationBuilder.RenameTable(
                name: "UserOwnedApplianceModel",
                newName: "UserOwnedAppliance");

            migrationBuilder.RenameTable(
                name: "RecipeIngredientModel",
                newName: "RecipeIngredient");

            migrationBuilder.RenameIndex(
                name: "IX_UserOwnedApplianceModel_UserEmail",
                table: "UserOwnedAppliance",
                newName: "IX_UserOwnedAppliance_UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_UserOwnedApplianceModel_ApplianceId",
                table: "UserOwnedAppliance",
                newName: "IX_UserOwnedAppliance_ApplianceId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredientModel_RecipeId",
                table: "RecipeIngredient",
                newName: "IX_RecipeIngredient_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredientModel_IngredientName_IngredientCookingMethod",
                table: "RecipeIngredient",
                newName: "IX_RecipeIngredient_IngredientName_IngredientCookingMethod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOwnedAppliance",
                table: "UserOwnedAppliance",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientName_IngredientCookin~",
                table: "RecipeIngredient",
                columns: new[] { "IngredientName", "IngredientCookingMethod" },
                principalTable: "Ingredient",
                principalColumns: new[] { "Name", "CookingMethod" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Recipe_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOwnedAppliance_Appliance_ApplianceId",
                table: "UserOwnedAppliance",
                column: "ApplianceId",
                principalTable: "Appliance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOwnedAppliance_User_UserEmail",
                table: "UserOwnedAppliance",
                column: "UserEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Ingredient_IngredientName_IngredientCookin~",
                table: "RecipeIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Recipe_RecipeId",
                table: "RecipeIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOwnedAppliance_Appliance_ApplianceId",
                table: "UserOwnedAppliance");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOwnedAppliance_User_UserEmail",
                table: "UserOwnedAppliance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOwnedAppliance",
                table: "UserOwnedAppliance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient");

            migrationBuilder.RenameTable(
                name: "UserOwnedAppliance",
                newName: "UserOwnedApplianceModel");

            migrationBuilder.RenameTable(
                name: "RecipeIngredient",
                newName: "RecipeIngredientModel");

            migrationBuilder.RenameIndex(
                name: "IX_UserOwnedAppliance_UserEmail",
                table: "UserOwnedApplianceModel",
                newName: "IX_UserOwnedApplianceModel_UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_UserOwnedAppliance_ApplianceId",
                table: "UserOwnedApplianceModel",
                newName: "IX_UserOwnedApplianceModel_ApplianceId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredient_RecipeId",
                table: "RecipeIngredientModel",
                newName: "IX_RecipeIngredientModel_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredient_IngredientName_IngredientCookingMethod",
                table: "RecipeIngredientModel",
                newName: "IX_RecipeIngredientModel_IngredientName_IngredientCookingMethod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOwnedApplianceModel",
                table: "UserOwnedApplianceModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredientModel",
                table: "RecipeIngredientModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredientModel_Ingredient_IngredientName_IngredientC~",
                table: "RecipeIngredientModel",
                columns: new[] { "IngredientName", "IngredientCookingMethod" },
                principalTable: "Ingredient",
                principalColumns: new[] { "Name", "CookingMethod" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredientModel_Recipe_RecipeId",
                table: "RecipeIngredientModel",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOwnedApplianceModel_Appliance_ApplianceId",
                table: "UserOwnedApplianceModel",
                column: "ApplianceId",
                principalTable: "Appliance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOwnedApplianceModel_User_UserEmail",
                table: "UserOwnedApplianceModel",
                column: "UserEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
