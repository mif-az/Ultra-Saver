using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;
public class UserOwnedApplianceModel
{
    [Key]
    public int Id { get; set; }

    public string UserEmail { get; set; }
    public User User { get; set; }

    public int ApplianceId { get; set; }
    public Appliance Appliance { get; set; }

    public int ApplianceWattage { get; set; }

}