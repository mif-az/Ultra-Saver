using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ultra_Saver.Models;
[ExcludeFromCodeCoverage]


public class ChatMessageModel
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; } = null!;

    public string User { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Updated_at { get; set; }

}