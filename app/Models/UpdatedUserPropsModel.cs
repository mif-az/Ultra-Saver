using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class UpdatedUserPropsModel : Model<string>
{
    [Key]
    [EmailAddress]
    public string email { get; set; } = null!; // Not nullable

    public bool darkMode { get; set; } = false;

    public List<String>? favorites { get; set; } // shows dishes that a User marked as favorite/saved

    public List<String>? followed { get; set; } // shows accounts that a User follows

    public override string GetSignature()
    {
        return this.email;
    }
}