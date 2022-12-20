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

    public double TotalEnergy { get; set; }

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
    public int Id { get; set; } = 0;
    public string Owner { get; set; } = "";

    public string Instruction { get; set; } = "";

    public int CalorieCount { get; set; }

    public int FullPrepTime { get; set; } = 0;

    public string Name { get; set; } = null!;

    public byte[] ImageData { get; set; } = null!;

    public RecipeStepsDTO[] Steps { get; set; } = null!;

    public ICollection<RecipeIngredientDTO> RecipeIngredient
    { get; set; } = null!;
}

public class RecipeStepsDTO
{
    public string stepInstruction { get; set; } = "";
    public int stepTime { get; set; } = 1;
    public int stepPowerScale { get; set; } = 1;
    public string stepAppliance { get; set; } = "ELECTRIC_COIL_STOVE";
}