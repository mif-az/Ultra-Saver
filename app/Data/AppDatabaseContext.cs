using Microsoft.EntityFrameworkCore;
using UltraSaver;

namespace Ultra_Saver;


public class AppDatabaseContext : DbContext
{
    public static readonly int ItemsPerPage = 10;

    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
    {
    }

    public DbSet<UserPropsModel> Properties { get; set; } = null!; // UserProps table
    public DbSet<RecipeModel> Recipes { get; set; } = null!;


}