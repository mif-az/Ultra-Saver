using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class RecipeModel : Model<string>
{
    [Key]
    public int Id { get; set; }

    public string Owner { get; set; } = "";

    public string Instruction { get; set; } = "";

    public int CalorieCount { get; set; }

    public int FullPrepTime { get; set; }

    public string Name { get; set; } = null!;

    public byte[] ImageData { get; set; } = null!;

    public virtual ICollection<UserLikedRecipeModel> UserLikedRecipe { get; set; } = null!;

    public virtual ICollection<RecipeIngredientModel> RecipeIngredient { get; set; } = null!;

    public override string GetSignature()
    {
        return $"{this.Id}_{this.Owner}";
    }
}

public class RecipeDTO
{
    public string Owner { get; set; } = "";

    public string Instruction { get; set; } = "";

    public int CalorieCount { get; set; }

    public int FullPrepTime { get; set; }

    public string Name { get; set; } = null!;

    public byte[] ImageData { get; set; } = null!;

    public ICollection<RecipeIngredientDTO> RecipeIngredient { get; set; } = null!;
}