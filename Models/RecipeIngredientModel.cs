using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;
public class RecipeIngredientModel
{
    [Key]
    public int Id { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public string IngredientName { get; set; }
    public string IngredientCookingMethod { get; set; }
    public Ingredient Ingredient { get; set; }

}