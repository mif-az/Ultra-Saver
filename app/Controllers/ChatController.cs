using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ultra_Saver;

[ApiController]
[Authorize]
[Route("/chat/messages")]
public class ChatController : ControllerBase
{
    private readonly AppDatabaseContext _db;
    public ChatController(AppDatabaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> getMessages()
    {
        var dt = DateTime.UtcNow.ToString();
        var messages = await _db.ChatMessage.OrderByDescending(o => o.Id)
            .Take(100).Reverse().ToListAsync();
        return Ok(messages);
    }

}