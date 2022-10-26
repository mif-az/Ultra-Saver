using System.ComponentModel.DataAnnotations;

namespace UltraSaver;

public class UserModel
{
    [Key]
    [EmailAddress]
    public string Email { get; set; } = "";

    public bool DarkMode { get; set; } = false;

    public float ElectricityPrice { get; set; }

    public AllergensModel Allergens { get; set; } = null!;

    public ICollection<UserLikedRecipeModel> UserLikedRecipe { get; set; } = null!;

    public ICollection<UserOwnedApplianceModel> UserOwnedAppliance { get; set; } = null!;
}