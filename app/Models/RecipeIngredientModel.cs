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

    public int IngredientAmount { get; set; } = 0;

    public string IngredientCookingMethod { get; set; } = "";

    public virtual RecipeModel Recipe { get; set; } = null!;
}