using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ultra_Saver;
using Ultra_Saver.Configuration;


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

builder.Logging.AddProvider(new FileLoggerProvider(new FileLoggerConfiguration(
    filename: builder.Configuration["Logging:LogFile"] ?? "saver.log",
    level: Enum.Parse<LogLevel>(builder.Configuration["Logging:LogLevel:Default"])
)));

builder.Services.AddLogging();

builder.Services.AddScoped<INewUserInitService, NewUserInitService>();

builder.Services.AddScoped<IEnergyCostAlgorithm, EnergyCostAlgorithm>();

builder.Services.AddSingleton<IStatisticsProcessorFactory>(new StatisticsProcessorFactory());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UltraSaver Docs", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UltraSaver Docs v1"));

}
else
{
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://usaver.ddns.net:2781", "https://localhost:44462"));

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseWebSockets();

app.UseMiddleware<LoggingInterceptor>();

app.MapHub<ChatHandlerService>("/chat/send");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

public partial class Program { }
