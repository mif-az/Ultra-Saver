using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]

public class OldRecipeModel : Model<string>
{

    public int Id { get; set; }

    [Required]
    public int Minutes { get; set; }
    public int Wattage { get; set; }

    [Required]
    public string Owner { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    public override string GetSignature()
    {
        return $"{this.Id}_{this.Owner}";
    }
}