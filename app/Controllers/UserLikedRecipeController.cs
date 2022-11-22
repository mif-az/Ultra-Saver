

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
public class UserLikedRecipeController : ControllerBase
{
    private readonly AppDatabaseContext _db;

    [HttpPost]
    [Authorize]
    public IActionResult LikeRecipe(UserLikedRecipeModel recipe) //For upserting we need the full model information (id can be ommited for creating a new recipe)
    {
        _db.UserLikedRecipe.Add(recipe);
        _db.SaveChanges();
        return Ok();
    }
}