using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltraSaver;

namespace Ultra_Saver.Controllers;

[ApiController]
[Route("[controller]")]
public class UserInfoController : ControllerBase
{
    private readonly AppDatabaseContext _db;

    public UserInfoController(AppDatabaseContext db)
    {
        // Use dependency injection to get access to the database
        this._db = db;
    }

    [HttpGet]
    [Authorize] // This ensures that only the connections that pass a valid JWT token in the authentication header get here
    public IActionResult GetUserInfo()
    {
        var email = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim(); // Get email from the JWT

        if (email == null)
            return BadRequest("No email was provided");

        var res = this._db.Properties.Find(email); // Try to find users properties here

        if (res == null)
        {
            // If this user logged in for the first time - we call a service that initializes a new user in the database
            NewUserInitService.init(db: _db, email: email);
            res = this._db.Properties.Find(email);
        }

        return Ok(res);
    }

    [HttpPost]
    [Authorize]
    public IActionResult setUserInfo(UserPropsModel props)
    {
        var email = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim(); // Get email from the JWT

        if (email == null)
            return BadRequest("No email was provided");

        if (!email.Equals(props.email))
            return BadRequest("Incorrect identity");

        _db.Properties.Update(props);

        return Ok(_db.SaveChanges());
    }
}

