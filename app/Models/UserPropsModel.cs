using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;

public class UserPropsModel
{
    [Key]
    [EmailAddress]
    public string email { get; set; } = ""; // Not nullable

    public bool darkMode { get; set; } = false;
}