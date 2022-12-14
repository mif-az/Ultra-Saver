

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
    public async Task<IActionResult> GetRecipeIngredientsFromRecipeID(int recipeID)
    {
        IQueryable<RecipeIngredientModel> recipeIngredients = (from r in _db.RecipeIngredient
                                                               where r.RecipeId == recipeID
                                                               select r);

        if (recipeIngredients.Any())
        {
            return Ok(recipeIngredients);
        }
        else
        {
            return StatusCode(404);
        }

    }
}