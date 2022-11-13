using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class UserPropsModel
{
    [Key]
    [EmailAddress]
    public string email { get; set; } = ""; // Not nullable

    public bool darkMode { get; set; } = false;
}