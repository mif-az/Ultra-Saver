

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

    public UserLikedRecipeController(AppDatabaseContext db, ILogger<RecipeController> logger, IStatisticsProcessorFactory statisticsProcessorFactory, IEnergyCostAlgorithm energyCostAlgorithm)
    {
        _db = db;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetLikedRecipes(uint page = 1)
    {
        if (page < 1)
        {
            return BadRequest();
        }

        string? userEmail = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim();

        IQueryable<RecipeModel> userLikedRecipes = (from recipes in _db.UserLikedRecipe
                                                    where recipes.UserEmail == userEmail
                                                    select recipes.Recipe);

        return Ok(userLikedRecipes.Skip(((int)(page) - 1) * AppDatabaseContext.ItemsPerPage).Take(AppDatabaseContext.ItemsPerPage));
    }

    [HttpPost]
    [Authorize]
    public IActionResult LikeRecipe(UserLikedRecipeDTO postRequest)
    {
        var user = _db.User.Find(postRequest.UserEmail);
        var recipe = _db.Recipes.Find(postRequest.RecipeId);

        if (user == null || recipe == null)
        {
            return BadRequest();
        }

        UserLikedRecipeModel recipeModel = new UserLikedRecipeModel
        {
            Id = postRequest.Id,
            UserEmail = postRequest.UserEmail,
            RecipeId = postRequest.RecipeId,
            User = user,
            Recipe = recipe
        };

        IQueryable<UserLikedRecipeModel> existingUserLikedRecipe = (from r in _db.UserLikedRecipe
                                                                    where r.RecipeId == recipe.Id && r.UserEmail == user.Email
                                                                    select r);

        if (!existingUserLikedRecipe.Any()) // Add liked recipe to database if it hasn't been liked before
        {
            _db.UserLikedRecipe.Add(recipeModel);
            _db.SaveChanges();
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}