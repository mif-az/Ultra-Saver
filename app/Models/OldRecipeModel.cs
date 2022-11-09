using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;
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