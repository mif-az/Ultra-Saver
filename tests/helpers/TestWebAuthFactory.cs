using System;
using System.Linq;
using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ultra_Saver.Tests;

public class TestWebAuthFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{

    internal String dbName { get; set; } = "defaultDBName";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<AppDatabaseContext>));

            services.Remove(descriptor);

            services.AddDbContext<AppDatabaseContext>(options =>
            {
                options.UseInMemoryDatabase(dbName);
            });

        });

        builder.UseEnvironment("Development");
    }
}