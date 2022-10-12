using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;
public class UserModel
{
    [Key]
    [EmailAddress]
    public string email { get; set; } = null!; // Not nullable

    public bool DarkMode { get; set; } = false;

    public float ElectricityPrice { get; set; }

    // Navigation Properties
    public List<Recipe> OwnedRecipe { get; set; }

    public List<UserLikedRecipe> UserLikedRecipe { get; set; }
    
    public List<UserOwnedAppliance> UserOwnedAppliance { get; set; }
}