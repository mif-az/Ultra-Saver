using System.ComponentModel.DataAnnotations;

namespace UltraSaver;

public class UserLikedRecipeModel
{
    [Key]
    public int Id { get; set; }

    public string UserEmail { get; set; } = "";

    public int RecipeId { get; set; }

    public UserModel User { get; set; } = null!;

    public NewRecipeModel Recipe { get; set; } = null!;
}