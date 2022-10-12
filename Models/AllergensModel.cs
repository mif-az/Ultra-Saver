using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;
public class AllergensModel
{
    [Key]
    [EmailAddress]
    public string email { get; set; } = null!; // Not nullable

    public bool Vegetarian { get; set; }

    public bool Vegan { get; set; }

    public bool DairyAllergy { get; set; }

    public bool EggsAllergy { get; set; }

    public bool FishAllergy { get; set; }

    public bool ShellfishAllergy { get; set; }

    public bool NutsAllergy { get; set; }

    public bool WheatAllergy { get; set; }

    public bool SoybeanAllergy { get; set; }

}