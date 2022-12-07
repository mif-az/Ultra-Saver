

using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
// System.InvalidOperationException:
// The property 'AllergensModel.Email' is part of a key and so cannot be modified or marked as modified.
// To change the principal of an existing entity with an identifying foreign key, first delete the dependent and invoke 'SaveChanges',
// and then associate the dependent with the new principal.
   

            AllergensModel AllergensModel = new AllergensModel
            {
                User = user,
                Vegetarian = allergens.Vegetarian,
                Vegan = allergens.Vegan,
                DairyAllergy = allergens.DairyAllergy,
                EggsAllergy = allergens.EggsAllergy,
                FishAllergy = allergens.FishAllergy,
                ShellfishAllergy = allergens.ShellfishAllergy,
                NutsAllergy = allergens.NutsAllergy,
                WheatAllergy = allergens.WheatAllergy,
                SoybeanAllergy = allergens.SoybeanAllergy
            };
            // var check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [Table] WHERE ([user] = @user)");
            // check_User_Name.Parameters.AddWithValue("@user", txtBox_UserName.Text);
            // int UserExist = (int)check_User_Name.ExecuteScalar();
            _db.Allergens.Add(AllergensModel);
            _db.SaveChanges();

            return Ok();
        }
        else { return BadRequest(); }
    }
}