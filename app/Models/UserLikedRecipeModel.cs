using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;

public class UserLikedRecipeModel
{
    [Key]
    public int Id { get; set; }

    public string UserEmail { get; set; } = "";

    public int RecipeId { get; set; }

    public UserModel User { get; set; } = null!;

    public RecipeModel Recipe { get; set; } = null!;
}