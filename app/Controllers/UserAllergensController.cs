using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultra_Saver.Models;

namespace Ultra_Saver;

[ApiController]
[Route("[controller]")]
public class UserAllergensController : ControllerBase
{
    private readonly AppDatabaseContext _db;

    public UserAllergensController(AppDatabaseContext db)
    {
        _db = db;
    }

    [HttpPost]
    [Authorize]
    public IActionResult SetAllergens(AllergensDTO allergens) //For upserting we need the full model information (id can be ommited for creating a new recipe)
    {
        string? userEmail = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim();
        var user = _db.User.Find(userEmail);

        if (userEmail != null)
        {

            AllergensModel AllergensModel = new AllergensModel
            {
                User = user,
                Vegetarian = allergens.Vegetarian,
                Vegan = allergens.Vegan,
                DairyAllergy = allergens.Dairy,
                EggsAllergy = allergens.Eggs,
                FishAllergy = allergens.Fish,
                ShellfishAllergy = allergens.Shellfish,
                NutsAllergy = allergens.Nuts,
                WheatAllergy = allergens.Wheat,
                SoybeanAllergy = allergens.Soybean
            };
            _db.Allergens.Add(AllergensModel);
            _db.SaveChanges();

            return Ok();
        }
        else { return BadRequest(); }
    }
}