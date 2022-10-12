using Microsoft.EntityFrameworkCore;
using UltraSaver;

namespace Ultra_Saver;

public class AppDatabaseContext : DbContext
{

    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserLikedRecipeModel>()
            .HasOne(b => b.UserModel)
            .WithMany(ba => ba.UserLikedRecipeModel)
            .HasForeignKey(bf => bf.UserEmail);

        modelBuilder.Entity<UserLikedRecipeModel>()
            .HasOne(b => b.RecipeModel)
            .WithMany(ba => ba.UserLikedRecipeModel)
            .HasForeignKey(bf => bf.RecipeId);
        
        modelBuilder.Entity<UserOwnedApplianceModel>()
            .HasOne(b => b.UserModel)
            .WithMany(ba => ba.UserOwnedApplianceModel)
            .HasForeignKey(bf => bf.UserEmail);

        modelBuilder.Entity<UserOwnedApplianceModel>()
            .HasOne(b => b.ApplianceModel)
            .WithMany(ba => ba.UserOwnedApplianceModel)
            .HasForeignKey(bf => bf.ApplianceId);

        modelBuilder.Entity<RecipeIngredientModel>()
            .HasOne(b => b.RecipeModel)
            .WithMany(ba => ba.RecipeIngredientModel)
            .HasForeignKey(bf => bf.RecipeId);

        modelBuilder.Entity<RecipeIngredientModel>()
            .HasOne(b => b.IngredientModel)
            .WithMany(ba => ba.RecipeIngredientModel)
            .HasForeignKey(bf => bf.IngredientName)
            .HasForeignKey(bf => bf.IngredientCookingMethod);
    }


    
    public DbSet<UserModel> User { get; set; }

    public DbSet<RecipeModel> Recipe { get; set; }

    public DbSet<ApplianceModel> Appliance { get; set; }

    public DbSet<UserLikedRecipeModel> UserLikedRecipe { get; set; }

    public DbSet<UserOwnedApplianceModel> UserOwnedAppliance { get; set; }

    public DbSet<RecipeIngredientModel> RecipeIngredient { get; set; }

    public DbSet<CommentModel> Comments { get; set; }

    public DbSet<AllergensModel> Allergens { get; set; }

}