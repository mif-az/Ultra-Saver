using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultra_Saver.Models;
namespace Ultra_Saver.Controllers;

[ApiController]
[Route("[controller]")]
public class UserInfoController : ControllerBase
{
    private readonly AppDatabaseContext _db;
    private readonly ILogger<UserInfoController> _logger;
    private readonly INewUserInitService _newUserInitService;

    public UserInfoController(AppDatabaseContext db, ILogger<UserInfoController> logger, INewUserInitService newUserInitService)
    {
        // Use dependency injection to get access to the database
        this._db = db;
        _logger = logger;
        _newUserInitService = newUserInitService;
    }

    [HttpGet]
    [Authorize] // This ensures that only the connections that pass a valid JWT token in the authentication header get here
    public IActionResult GetUserInfo()
    {

        var email = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim(); // Get email from the JWT

        if (email == null)
            return BadRequest("No email was provided");

        var res = this._db.User.Find(email); // Try to find users properties here

        if (res == null)
        {
            // If this user logged in for the first time - we call a service that initializes a new user in the database
            _logger.LogInformation("A new user has registered.");
            _newUserInitService.Init(db: _db, email: email);
            res = this._db.User.Find(email);
        }

        return Ok(res);
    }

    [HttpPost]
    [Authorize]
    public IActionResult SetUserInfo(UserDTO postRequest)
    {
        var email = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim(); // Get email from the JWT
        UserModel? user = _db.User.Find(email);

        if (email == null)
            return BadRequest("Not logged in");

        var curr = _db.User.Find(email);

        if (curr == null)
        {
            return BadRequest("User does not exist");
        }

        curr.ElectricityPrice = postRequest.ElectricityPrice;
        curr.DarkMode = postRequest.DarkMode;

        _db.Update(curr);
        _db.SaveChanges();


        return Ok();
    }
}

