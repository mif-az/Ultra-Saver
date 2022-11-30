using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class ApplianceModel
{
    [Key]
    public int Id { get; set; }

    public string CookingMethod { get; set; } = "";

    public string Name { get; set; } = "";

    public virtual ICollection<UserOwnedApplianceModel> UserOwnedAppliance { get; set; } = null!;
}