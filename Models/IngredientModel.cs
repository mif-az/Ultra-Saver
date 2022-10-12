using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UltraSaver;
public class IngredientModel
{
    [Key, Column(Order = 1)]
    public string Name { get; set; }

    [Key, Column(Order = 2)]
    public string CookingMethod { get; set; }

    public int CookingTime { get; set; }

    public int Price { get; set; }

    // Picture

    // Navigation Properties
    public List<RecipeIngredient> RecipeIngredient { get; set; }
}