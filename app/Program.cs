using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Ultra_Saver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add the AppDatabaseContext class as a service and connect it to the real PostgreSQL database
builder.Services.AddDbContext<AppDatabaseContext>(
    o =>
    {
        var conectionString = builder.Configuration.GetConnectionString("db") + $";Database={builder.Configuration["DB:Name"]};User Id={builder.Configuration["DB:User"]};Password={builder.Configuration["DB:Password"]}";
        o.UseNpgsql(conectionString);
    }
);

// Add a service that can process the JWT token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwt => jwt.UseGoogle(
        clientId: "929531042172-l5h1hbegcb3qm6nkpg1r7m4aa6seb98n.apps.googleusercontent.com"
    ));

builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
// app.UseStaticFiles();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://usaver.ddns.net:3477", "https://localhost:44462"));

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseWebSockets();

app.MapHub<ChatHandlerService>("/chat/send");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
