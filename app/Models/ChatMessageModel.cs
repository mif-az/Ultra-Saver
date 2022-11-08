using System.ComponentModel.DataAnnotations;

namespace Ultra_Saver;

public class ChatMessageModel
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; } = null!;

    public string User { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Updated_at { get; set; }

}