

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
    private readonly ILogger<UserLikedRecipeController> _logger;

    public UserLikedRecipeController(AppDatabaseContext db, ILogger<UserLikedRecipeController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetLikedRecipes(uint page = 1, string? filter = null)
    {
        if (page < 1)
        {
            return BadRequest();
        }

        string? userEmail = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim();
        IQueryable<RecipeModel> userLikedRecipes = (from recipes in _db.UserLikedRecipe
                                                    where recipes.UserEmail == userEmail
                                                    select recipes.Recipe);

        if (filter != null)
        {
            string str = filter.map(letter => $".*?{Regex.Escape(letter.ToString().ToLower())}.*?");
            return Ok(_db.Recipes.Where(c => Regex.IsMatch(c.Name.ToLower(), str)).Take(AppDatabaseContext.ItemsPerPage));
        }

        return Ok(userLikedRecipes.Skip(((int)(page) - 1) * AppDatabaseContext.ItemsPerPage).Take(AppDatabaseContext.ItemsPerPage));
    }

    [HttpPost]
    [Authorize]
    public IActionResult LikeRecipe(int recipeId)
    {
        string? userEmail = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim();

        var user = _db.User.Find(userEmail);
        var recipe = _db.Recipes.Find(recipeId);

        if (user == null || recipe == null || userEmail == null)
        {
            return BadRequest();
        }

        UserLikedRecipeModel recipeModel = new UserLikedRecipeModel
        {
            UserEmail = userEmail, // to avoid warning, not sure if thats the best way
            RecipeId = recipeId,
            User = user,
            Recipe = recipe
        };

        IQueryable<UserLikedRecipeModel> existingUserLikedRecipe = (from r in _db.UserLikedRecipe
                                                                    where r.RecipeId == recipe.Id && r.UserEmail == user.Email
                                                                    select r);

        if (!existingUserLikedRecipe.Any()) // Add liked recipe to database if it hasn't been liked before
        {
            try
            {
                _db.UserLikedRecipe.Add(recipeModel);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Error adding liked recipe to db.");
                return StatusCode(500);
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Authorize]
    public IActionResult UnlikeRecipe(int recipeId)
    {
        string? userEmail = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim();

        if (userEmail == null)
        {
            return BadRequest("Not logged in");
        }

        try
        {
            IQueryable<UserLikedRecipeModel> existingUserLikedRecipe = (from r in _db.UserLikedRecipe
                                                                        where r.RecipeId == recipeId && r.UserEmail == userEmail
                                                                        select r);

            UserLikedRecipeModel likedRecipeModel = existingUserLikedRecipe.First();
            _db.Remove(likedRecipeModel);
            _db.SaveChanges();
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error deleting liked recipe from db.");
            return StatusCode(500);
        }
    }
}