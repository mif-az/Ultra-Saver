using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class UserLikedRecipeModel
{
    [Key]
    public int Id { get; set; }

    public string UserEmail { get; set; } = "";

    public int RecipeId { get; set; }

    public UserModel User { get; set; } = null!;

    public RecipeModel Recipe { get; set; } = null!;
}