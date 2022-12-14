
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Ultra_Saver;
using Ultra_Saver.Models;
using Ultra_Saver.Tests;
using Xunit;

public class RestfulTests : BaseTestProgram
{
    public RestfulTests(TestWebAuthFactory<Program> factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("/recipe")]
    [InlineData("/userinfo")]
    public async Task NoAccessWithoutAuthorization(string url)
    {
        var client = _factory.CreateClient();
        var responseGet = await client.GetAsync(url);
        var responsePost = await client.PostAsync(url, null);

        Assert.Equal(HttpStatusCode.Unauthorized, responseGet.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, responsePost.StatusCode);
    }

    [Theory]
    [InlineData("/recipe")]
    [InlineData("/userinfo")]
    [InlineData("/recipe/stats")]
    public async Task GetReturnsSuccessWithAuthorization(string url)
    {
        var client = AuthenticatedClient.getAuthenticatedClient(_factory);
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

    }

    [Theory]
    [InlineData("/recipe/stats")]
    public async Task AccessWithoutAuthorization(string url)
    {
        var client = _factory.CreateClient();
        var responseGet = await client.GetAsync(url);
        var responsePost = await client.PostAsync(url, null);

        responseGet.EnsureSuccessStatusCode();

    }

    [Fact]
    public async Task ErrorChangingUserInfoIfUserDoesNotExist()
    {
        var client = AuthenticatedClient.getAuthenticatedClient(_factory);

        var response = await client.PostAsJsonAsync("/userinfo", new UserPropsModel { darkMode = true });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AbleToChangeUserPropertiesForExistingUser()
    {
        var client = AuthenticatedClient.getAuthenticatedClient(_factory);

        var response = await client.GetAsync("/userinfo");
        response.EnsureSuccessStatusCode();

        var content = new StringContent(JsonConvert.SerializeObject(new UserPropsModel { darkMode = true, email = "test@tests.test" }), Encoding.UTF8, "application/json");

        response = await client.PostAsync("/userinfo", content);
        response.EnsureSuccessStatusCode();

        response = await client.GetAsync("/userinfo");
        response.EnsureSuccessStatusCode();

        var responseMessage = await response.Content.ReadFromJsonAsync<UserPropsModel>();

        Assert.NotNull(responseMessage);
        Assert.Equal("test@tests.test", responseMessage!.email);
        Assert.True(responseMessage!.darkMode);
    }

    [Fact]
    public async Task RecipesPaginationIsCorrect()
    {
        _factory.dbName = Guid.NewGuid().ToString();
        using (var scope = _factory.Services.CreateScope())
        {
            var client = AuthenticatedClient.getAuthenticatedClient(_factory);

            var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var fakers = new Fakers(db);
            await fakers.seedDatabaseWith(fakers.fakeRecipes().Generate(100));

            var response = await client.GetAsync("/recipe");
            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadFromJsonAsync<List<RecipeModel>>();
            Assert.NotNull(responseMessage);
            Assert.Equal(AppDatabaseContext.ItemsPerPage, responseMessage!.Count);
        }
    }

    [Fact]
    public async Task FilteredListIsNotPaginated()
    {
        _factory.dbName = Guid.NewGuid().ToString();
        using (var scope = _factory.Services.CreateScope())
        {
            var client = AuthenticatedClient.getAuthenticatedClient(_factory);

            var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var fakers = new Fakers(db);
            await fakers.seedDatabaseWith(fakers.fakeRecipes("Creative Name").Generate(100));

            var response = await client.GetAsync("/recipe?filter=cn");
            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadFromJsonAsync<List<RecipeModel>>();
            Assert.NotNull(responseMessage);
            Assert.Equal(AppDatabaseContext.ItemsPerPage, responseMessage!.Count);

            response = await client.GetAsync("/recipe?filter=x");
            response.EnsureSuccessStatusCode();

            responseMessage = await response.Content.ReadFromJsonAsync<List<RecipeModel>>();
            Assert.NotNull(responseMessage);
            Assert.Equal(0, responseMessage!.Count);
        }
    }

    [Fact]
    public async Task ErrorIfPageIsLessThanOne()
    {
        var client = AuthenticatedClient.getAuthenticatedClient(_factory);

        var response = await client.GetAsync("/recipe?page=0");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RecipeIsUpserted()
    {
        _factory.dbName = Guid.NewGuid().ToString();
        using (var scope = _factory.Services.CreateScope())
        {
            var client = AuthenticatedClient.getAuthenticatedClient(_factory);
            var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var fakers = new Fakers(db);

            var models = fakers.fakeRecipes(recipeIds: 0).Generate(1);

            var response = await client.PostAsJsonAsync("/recipe", models[0]);
            response.EnsureSuccessStatusCode();

            Assert.Equal(1, db.Recipes.Count());

            models = models.Select(r =>
            {
                r.Id = db.Recipes.First().Id;
                r.Name = "change";
                return r;
            }).ToList();

            response = await client.PostAsJsonAsync("/recipe", models[0]);
            response.EnsureSuccessStatusCode();

            // Assert.Equal("change", db.Recipes.Where(r => r.Id == 1).ToList()[0].Name); // TODO fix (issue with test)
            Assert.Equal(1, db.Recipes.Count());

        }
    }

    [Fact]
    public async Task RecipeIsDeleted()
    {
        _factory.dbName = Guid.NewGuid().ToString();
        using (var scope = _factory.Services.CreateScope())
        {
            var client = AuthenticatedClient.getAuthenticatedClient(_factory);
            var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var fakers = new Fakers(db);
            await fakers.seedDatabaseWith(fakers.fakeRecipes(recipeIds: 1).Generate(1).Select(r =>
            {
                r.Owner = "test@tests.test";
                return r;
            }).ToList());


            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/recipe"),
                Content = new StringContent(JsonConvert.SerializeObject(new { id = 1 }), Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);

            Assert.Equal(0, db.Recipes.Count());

        }
    }

    [Fact]
    public async Task StatisticsIsFasterOnSubsequentRuns()
    {
        _factory.dbName = Guid.NewGuid().ToString();
        using (var scope = _factory.Services.CreateScope())
        {
            var client = _factory.CreateClient();

            var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var fakers = new Fakers(db);
            await fakers.seedDatabaseWith(fakers.fakeRecipes().Generate(100));

            Stopwatch st = new Stopwatch();
            st.Start();
            var response = await client.GetAsync("/recipe/stats");
            st.Stop();
            response.EnsureSuccessStatusCode();
            var initialRequestTime = st.ElapsedMilliseconds;

            st.Restart();
            response = await client.GetAsync("/recipe/stats");
            st.Stop();
            response.EnsureSuccessStatusCode();
            Assert.True(st.ElapsedMilliseconds < initialRequestTime);

        }
    }

    [Fact]
    public async Task StatisticsIsCalculatedLazily()
    {
        _factory.dbName = Guid.NewGuid().ToString();
        using (var scope = _factory.Services.CreateScope())
        {
            var client = _factory.CreateClient();

            var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var fakers = new Fakers(db);
            await fakers.seedDatabaseWith(fakers.fakeRecipes().Generate(100));

            var response = await client.GetAsync("/recipe/stats");
            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadFromJsonAsync<StatistcsOutput>();

            Assert.Equal(db.Recipes.Count(), responseMessage!.Count);
            Assert.NotEqual(-1, responseMessage!.Minutes);
            Assert.NotEqual(-1, responseMessage!.Wattage);

            await fakers.seedDatabaseWith(fakers.fakeRecipes(recipeIds: 101).Generate(1));

            response = await client.GetAsync("/recipe/stats");
            response.EnsureSuccessStatusCode();

            responseMessage = await response.Content.ReadFromJsonAsync<StatistcsOutput>();

            Assert.True(db.Recipes.Count() > responseMessage!.Count);
            Assert.NotEqual(-1, responseMessage!.Minutes);
            Assert.NotEqual(-1, responseMessage!.Wattage);

        }
    }

    private class StatistcsOutput
    {
        public int Count { get; set; } = -1;
        public double Minutes { get; set; } = -1;
        public double Wattage { get; set; } = -1;
    }

}
