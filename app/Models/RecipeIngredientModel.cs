using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class RecipeIngredientModel
{
    [Key]
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public string IngredientName { get; set; } = "";

    public string IngredientCookingMethod { get; set; } = "";

    public RecipeModel Recipe { get; set; } = null!;

    public IngredientModel Ingredient { get; set; } = null!;
}