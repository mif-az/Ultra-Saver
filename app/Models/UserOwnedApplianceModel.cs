using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class UserOwnedApplianceModel
{
    [Key]
    public int Id { get; set; }

    public string UserEmail { get; set; } = "";

    public int ApplianceId { get; set; }

    public int ApplianceWattage { get; set; }

    public virtual UserModel User { get; set; } = null!;

    public virtual ApplianceModel Appliance { get; set; } = null!;
}