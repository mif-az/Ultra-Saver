using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ultra_Saver.Migrations
{
    public partial class _1_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appliance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CookingMethod = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appliance", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Owner = table.Column<string>(type: "text", nullable: false),
                    Instruction = table.Column<string>(type: "text", nullable: false),
                    CalorieCount = table.Column<int>(type: "integer", nullable: false),
                    FullPrepTime = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    DarkMode = table.Column<bool>(type: "boolean", nullable: false),
                    ElectricityPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredientModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    IngredientName = table.Column<string>(type: "text", nullable: false),
                    IngredientCookingMethod = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredientModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredientModel_Ingredient_IngredientName_IngredientC~",
                        columns: x => new { x.IngredientName, x.IngredientCookingMethod },
                        principalTable: "Ingredient",
                        principalColumns: new[] { "Name", "CookingMethod" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredientModel_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    Vegetarian = table.Column<bool>(type: "boolean", nullable: false),
                    Vegan = table.Column<bool>(type: "boolean", nullable: false),
                    DairyAllergy = table.Column<bool>(type: "boolean", nullable: false),
                    EggsAllergy = table.Column<bool>(type: "boolean", nullable: false),
                    FishAllergy = table.Column<bool>(type: "boolean", nullable: false),
                    ShellfishAllergy = table.Column<bool>(type: "boolean", nullable: false),
                    NutsAllergy = table.Column<bool>(type: "boolean", nullable: false),
                    WheatAllergy = table.Column<bool>(type: "boolean", nullable: false),
                    SoybeanAllergy = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Allergens_User_Email",
                        column: x => x.Email,
                        principalTable: "User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLikedRecipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikedRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLikedRecipe_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikedRecipe_User_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOwnedApplianceModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    ApplianceId = table.Column<int>(type: "integer", nullable: false),
                    ApplianceWattage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOwnedApplianceModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOwnedApplianceModel_Appliance_ApplianceId",
                        column: x => x.ApplianceId,
                        principalTable: "Appliance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOwnedApplianceModel_User_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredientModel_IngredientName_IngredientCookingMethod",
                table: "RecipeIngredientModel",
                columns: new[] { "IngredientName", "IngredientCookingMethod" });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredientModel_RecipeId",
                table: "RecipeIngredientModel",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedRecipe_RecipeId",
                table: "UserLikedRecipe",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedRecipe_UserEmail",
                table: "UserLikedRecipe",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_UserOwnedApplianceModel_ApplianceId",
                table: "UserOwnedApplianceModel",
                column: "ApplianceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOwnedApplianceModel_UserEmail",
                table: "UserOwnedApplianceModel",
                column: "UserEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "RecipeIngredientModel");

            migrationBuilder.DropTable(
                name: "UserLikedRecipe");

            migrationBuilder.DropTable(
                name: "UserOwnedApplianceModel");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Appliance");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
