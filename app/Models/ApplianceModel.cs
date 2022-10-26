using System.ComponentModel.DataAnnotations;

namespace UltraSaver;

public class ApplianceModel
{
    [Key]
    public int Id { get; set; }

    public string CookingMethod { get; set; } = "";

    public string Name { get; set; } = "";

    public ICollection<UserOwnedApplianceModel> UserOwnedAppliance { get; set; } = null!;
}