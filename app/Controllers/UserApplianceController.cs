using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultra_Saver.Models;

namespace Ultra_Saver;

[ApiController]
[Route("[controller]")]
public class UserApplianceController : ControllerBase
{
    private readonly AppDatabaseContext _db;
    public UserApplianceController(AppDatabaseContext db) {  _db = db; }

    [HttpPost]
    [Authorize]
    public IActionResult SetAppliance(UserOwnedApplianceModelDTO appliance) //For upserting we need the full model information (id can be ommited for creating a new recipe)
    {
        string? userEmail = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim();
        var user = _db.UserOwnedAppliance.Find(userEmail);

        if (userEmail != null && user != null)
        {
            {
                user.ApplianceWattage = appliance.ApplianceWattage;
                // user.Appliance.CookingMethod = appliance.ApplianceType;
            };
            // _db.User.Update(user);
            _db.SaveChanges();

            return Ok();
        }
        else { return BadRequest(); }
    }
}