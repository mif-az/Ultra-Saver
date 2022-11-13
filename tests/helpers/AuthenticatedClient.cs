using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Ultra_Saver.Tests;

public class AuthenticatedClient
{
    public static HttpClient getAuthenticatedClient(WebApplicationFactory<Program> factory)
    {
        var client = factory.WithWebHostBuilder(builder =>
       {
           builder.ConfigureTestServices(services =>
           {
               services.AddAuthentication(defaultScheme: "TestScheme")
                   .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                       "TestScheme", options => { });
           });
       })
       .CreateClient(new WebApplicationFactoryClientOptions
       {
           AllowAutoRedirect = false,
       });

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: "TestScheme");

        return client;
    }
}