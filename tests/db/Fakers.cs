using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Ultra_Saver.Models;
using Xunit.Sdk;

namespace Ultra_Saver.Tests;

public class Fakers
{
    private readonly AppDatabaseContext _db;

    public Fakers(AppDatabaseContext db)
    {
        _db = db;
    }

    public Faker<RecipeModel> fakeRecipes(String? constantName = null, int recipeIds = 1)
    {
        return new Faker<RecipeModel>()
           .StrictMode(true)
           .RuleFor(o => o.Id, f => recipeIds++)
           .RuleFor(o => o.CalorieCount, f => f.Random.Number(100, 3000))
           .RuleFor(o => o.FullPrepTime, f => f.Random.Number(5, 300))
           .RuleFor(o => o.Owner, f => f.PickRandom(new String[] { "test@tests.test", "whatever@doesnotmatter.com" }))
           .RuleFor(o => o.Name, f => constantName == null ? String.Join(' ', f.Lorem.Words(f.Random.Number(5))) : constantName)
           .RuleFor(o => o.Instruction, f => f.Lorem.Paragraph())
           .RuleFor(o => o.ImageData, f => new byte[0])
           .RuleFor(o => o.UserLikedRecipe, f => new Collection<UserLikedRecipeModel>()) // Will update
           .RuleFor(o => o.RecipeIngredient, f => new Collection<RecipeIngredientModel>()); // Will update
    }

    public async Task seedDatabaseWith<T>(List<T> items) where T : class
    {
        await _db.AddRangeAsync(items);
        await _db.SaveChangesAsync();
    }

}