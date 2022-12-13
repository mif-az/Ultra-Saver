

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
public class RecipeIngredientController : ControllerBase
{
    private readonly AppDatabaseContext _db;
    private readonly ILogger<RecipeIngredientController> _logger;

    public RecipeIngredientController(AppDatabaseContext db, ILogger<RecipeIngredientController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet("{recipeID}")]
    public async Task<IActionResult> GetRecipeFromID(int recipeID)
    {
        RecipeModel? recipe = _db.Recipes.Find(recipeID);

        if (recipe != null)
        {
            return Ok(_db.Recipes.Find(recipeID));
        }
        else
        {
            return StatusCode(404);
        }

    }

    [HttpPost]
    [Authorize]
    public IActionResult PostRecipeIngredients(ICollection<RecipeIngredientModel> ingredients) //For upserting we need the full model information (id can be ommited for creating a new recipe)
    {
        foreach (var recipe in ingredients)
        {
            recipe.RecipeId++;
        }

        return Ok();
    }
}