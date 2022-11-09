using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;

public class UserOwnedApplianceModel
{
    [Key]
    public int Id { get; set; }

    public string UserEmail { get; set; } = "";

    public int ApplianceId { get; set; }

    public int ApplianceWattage { get; set; }

    public UserModel User { get; set; } = null!;

    public ApplianceModel Appliance { get; set; } = null!;
}