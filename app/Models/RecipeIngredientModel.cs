using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;

public class RecipeIngredientModel
{
    [Key]
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public string IngredientName { get; set; } = "";

    public string IngredientCookingMethod { get; set; } = "";

    public NewRecipeModel Recipe { get; set; } = null!;

    public IngredientModel Ingredient { get; set; } = null!;
}