using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;
public class RecipeModel
{
    public int Id { get; set; }

    [Required]
    public int Minutes { get; set; }
    public int Wattage { get; set; }

    [Required]
    public string Owner { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}