using Microsoft.EntityFrameworkCore;
using UltraSaver;

namespace Ultra_Saver;

public class AppDatabaseContext : DbContext
{

    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
    {
    }

    public DbSet<UserPropsModel> Properties { get; set; } // UserProps table
    public DbSet<RecipeModel> Recipes { get; set; }


}