using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;
public class ApplianceModel
{

    [Key]
    public int Id { get; set; }

    public string CookingMethod { get; set; }

    public string Name { get; set; }

    // Navigation Properties
    public List<UserOwnedAppliance> UserOwnedAppliance { get; set; }

}