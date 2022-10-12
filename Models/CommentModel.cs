using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UltraSaver;
public class CommentModel
{
    [Key]
    public int Id { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } // Navigation Properties

    public string UserEmail { get; set; }

    public string Comment { get; set; }

    
}