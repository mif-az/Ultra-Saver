using System.ComponentModel.DataAnnotations;

namespace UltraSaver;

public class IngredientModel
{
    [Key]
    public string Name { get; set; } = "";

    public string CookingMethod { get; set; } = "";

    public int CookingTime { get; set; }

    public int Price { get; set; }


    //picture

    public ICollection<RecipeIngredientModel> RecipeIngredient { get; set; } = null!;
}