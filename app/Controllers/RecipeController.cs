

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Ultra_Saver;

[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private readonly AppDatabaseContext _db;

    public RecipeController(AppDatabaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetRecipies(uint page = 1, string? filter = null) // Query parameter with default value
    {
        if (page < 1)
        {
            return BadRequest();
        }

        if (filter != null)
        {
            string str = filter.map(letter => $".*?{Regex.Escape(letter.ToString().ToLower())}.*?");
            return Ok(_db.Recipes.Where(c => Regex.IsMatch(c.Name.ToLower(), str)).Take(AppDatabaseContext.ItemsPerPage));
        }

        return Ok(_db.Recipes.Skip(((int)(page) - 1) * AppDatabaseContext.ItemsPerPage).Take(AppDatabaseContext.ItemsPerPage));
    }

    [HttpPost]
    [Authorize]
    public IActionResult UpsertRecipe(RecipeModel recipe) //For upserting we need the full model information (id can be ommited for creating a new recipe)
    {
        recipe.Owner = (HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim() ?? recipe.Owner; // Set recipe owner to current user

        if ((from r in _db.Recipes
             where r.Id == recipe.Id
             select r).AsNoTracking().First()?.Equals(recipe) ?? true) // Check if signatures of the object in db and provided object match (if recipe exists in db)
        {
            try
            {
                _db.Recipes.Update(recipe);
                _db.SaveChanges();
                return Ok();
            }
#pragma warning disable // TODO

            catch (Exception e)
            {
                // TODO log the exception
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
    public IActionResult DeleteRecipe([FromBody] dynamic req) // We only need the id of the recipe
    {
        RecipeModel? recipe = null;
        dynamic data = JsonConvert.DeserializeObject<dynamic>(req.ToString());
        try
        {
            if (data.id != null
                && (recipe = _db.Recipes.Find((int)data.id.Value)) != null
                && recipe?.Owner.Equals((HttpContext.User.Identity as ClaimsIdentity)?.getEmailFromClaim()))
            {
                _db.Remove(recipe); // recipe cannot be null here
                _db.SaveChanges();
                return Ok();
            }
        }
        catch (Exception e)
        {
            // TODO log the exception
            return StatusCode(500);
        }

        return BadRequest();
    }

}