using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class AllergensModel
{
    [Key]
    [EmailAddress]
    public string Email { get; set; } = "";

    public bool Vegetarian { get; set; }

    public bool Vegan { get; set; }

    public bool DairyAllergy { get; set; }

    public bool EggsAllergy { get; set; }

    public bool FishAllergy { get; set; }

    public bool ShellfishAllergy { get; set; }

    public bool NutsAllergy { get; set; }

    public bool WheatAllergy { get; set; }

    public bool SoybeanAllergy { get; set; }

    [Required]
    public virtual UserModel User { get; set; } = null!;
}