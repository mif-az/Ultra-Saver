using System.ComponentModel.DataAnnotations;

namespace UltraSaver;

public class NewRecipeModel
{
    [Key]
    public int Id { get; set; }

    public string Owner { get; set; } = "";

    public string Instruction { get; set; } = "";

    public int CalorieCount { get; set; }

    public int FullPrepTime { get; set; }

    public string Name { get; set; } = null!;


    //picture

    public ICollection<UserLikedRecipeModel> UserLikedRecipe { get; set; } = null!;

    public ICollection<RecipeIngredientModel> RecipeIngredient { get; set; } = null!;
}