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

    public virtual UserModel User { get; set; } = null!;

    public virtual RecipeModel Recipe { get; set; } = null!;
}

public class UserLikedRecipeDTO
{
    public int Id { get; set; }

    public string UserEmail { get; set; } = "";

    public int RecipeId { get; set; }
}