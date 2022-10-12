using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;
public class RecipeModel
{

    [Key]
    public int Id { get; set; }

    public string Owner { get; set; }
    public Owner Owner { get; set; }  // Navigation Properties

    public string Instruction { get; set; }

    public int CalorieCount { get; set; }

    public int FullPrepTime { get; set; }

    // Picture

    // Navigation Properties
    public List<Comment> Comments { get; set; }

    public List<UserLikedRecipe> UserLikedRecipe { get; set; }

    public List<RecipeIngredient> RecipeIngredient { get; set; }
}