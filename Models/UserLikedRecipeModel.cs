using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;
public class RecipeLikedRecipeModel
{
    [Key]
    public int Id { get; set; }

    public string UserEmail { get; set; }
    public User User { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

}