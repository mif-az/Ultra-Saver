using Microsoft.EntityFrameworkCore;
using Ultra_Saver.Models;

namespace Ultra_Saver;


public class AppDatabaseContext : DbContext
{
    public static readonly int ItemsPerPage = 10;

    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>()
            .HasOne(a => a.Allergens)
            .WithOne(b => b.User)
            .HasForeignKey<AllergensModel>(b => b.Email);

        modelBuilder.Entity<UserLikedRecipeModel>()
       .HasKey(ab => new { ab.Id });

        modelBuilder.Entity<UserLikedRecipeModel>()
            .HasOne<UserModel>(ab => ab.User)
            .WithMany(b => b.UserLikedRecipe)
            .HasForeignKey(ab => ab.UserEmail);

        modelBuilder.Entity<UserLikedRecipeModel>()
            .HasOne<RecipeModel>(ab => ab.Recipe)
            .WithMany(a => a.UserLikedRecipe)
            .HasForeignKey(ab => ab.RecipeId);

        modelBuilder.Entity<UserOwnedApplianceModel>()
            .HasOne(ab => ab.User)
            .WithMany(b => b.UserOwnedAppliance)
            .HasForeignKey(ab => ab.UserEmail);

        modelBuilder.Entity<UserOwnedApplianceModel>()
            .HasOne(ab => ab.Appliance)
            .WithMany(a => a.UserOwnedAppliance)
            .HasForeignKey(ab => ab.ApplianceId);

        modelBuilder.Entity<RecipeIngredientModel>()
            .HasOne(ab => ab.Recipe)
            .WithMany(b => b.RecipeIngredient)
            .HasForeignKey(ab => ab.RecipeId);
    }

    public DbSet<UserPropsModel> Properties { get; set; } = null!; // UserProps table
    public DbSet<RecipeModel> Recipes { get; set; } = null!;

    public DbSet<UserModel> User { get; set; } = null!;

    public DbSet<OldRecipeModel> Recipe { get; set; } = null!;

    public DbSet<ApplianceModel> Appliance { get; set; } = null!;

    public DbSet<AllergensModel> Allergens { get; set; } = null!;

    public DbSet<UserLikedRecipeModel> UserLikedRecipe { get; set; } = null!;

    public DbSet<UserOwnedApplianceModel> UserOwnedAppliance { get; set; } = null!;

    public DbSet<RecipeIngredientModel> RecipeIngredient { get; set; } = null!;

    public DbSet<ChatMessageModel> ChatMessage { get; set; } = null!;
}