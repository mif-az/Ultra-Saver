using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class UserModel
{
    [Key]
    [EmailAddress]
    public string Email { get; set; } = "";

    public bool DarkMode { get; set; } = false;

    public float ElectricityPrice { get; set; } = 0;

    public float GasPrice { get; set; } = 0;

    public virtual AllergensModel Allergens { get; set; } = null!;

    public virtual ICollection<UserLikedRecipeModel> UserLikedRecipe { get; set; } = null!;

    public virtual ICollection<UserOwnedApplianceModel> UserOwnedAppliance { get; set; } = null!;
}