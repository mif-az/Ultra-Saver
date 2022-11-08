using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace Ultra_Saver;

[Authorize(AuthenticationSchemes = "Bearer")]
public class ChatHandlerService : Hub
{
    private readonly AppDatabaseContext _db;

    public ChatHandlerService(AppDatabaseContext db)
    {
        this._db = db;
    }

    public async Task SendMessage(string msg)
    {
        var user = Context?.User?.Identity?.Name ?? @"N\A";

        var email = (Context.User.Identity as ClaimsIdentity).getEmailFromClaim();

        if (email.IsNullOrEmpty())
            throw new Exception("Illegal State");

        var savedMessage = new ChatMessageModel()
        {
            Email = email!,
            User = user,
            Message = msg,
            Updated_at = DateTime.UtcNow.ToString() ?? "?"
        };

        _db.ChatMessage.Add(savedMessage);
        await _db.SaveChangesAsync();
        await Clients.All.SendAsync("msg", savedMessage);
    }

    public async Task RemoveMessage(int id)
    {
        var email = (Context.User.Identity as ClaimsIdentity).getEmailFromClaim();

        if (email.IsNullOrEmpty())
            throw new Exception("Illegal State");

        var message = _db.ChatMessage.Find(id);

        if (message?.Email.Equals(email) ?? false)
        {
            _db.ChatMessage.Remove(message);
            await _db.SaveChangesAsync();
            await Clients.All.SendAsync("msg_removed", id);
        }
    }

}
