
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
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
    public async Task GetReturnsSuccessWithAuthorization(string url)
    {
        var client = AuthenticatedClient.getAuthenticatedClient(_factory);

        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();

    }

}