// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ultra_Saver;

#nullable disable

namespace Ultra_Saver.Migrations
{
    [DbContext(typeof(AppDatabaseContext))]
    [Migration("20221105153426_chat_message")]
    partial class chat_message
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ultra_Saver.ChatMessageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Updated_at")
                        .HasColumnType("text");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ChatMessage");
                });

            modelBuilder.Entity("Ultra_Saver.RecipeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Minutes")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Wattage")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("UltraSaver.AllergensModel", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("DairyAllergy")
                        .HasColumnType("boolean");

                    b.Property<bool>("EggsAllergy")
                        .HasColumnType("boolean");

                    b.Property<bool>("FishAllergy")
                        .HasColumnType("boolean");

                    b.Property<bool>("NutsAllergy")
                        .HasColumnType("boolean");

                    b.Property<bool>("ShellfishAllergy")
                        .HasColumnType("boolean");

                    b.Property<bool>("SoybeanAllergy")
                        .HasColumnType("boolean");

                    b.Property<bool>("Vegan")
                        .HasColumnType("boolean");

                    b.Property<bool>("Vegetarian")
                        .HasColumnType("boolean");

                    b.Property<bool>("WheatAllergy")
                        .HasColumnType("boolean");

                    b.HasKey("Email");

                    b.ToTable("Allergens");
                });

            modelBuilder.Entity("UltraSaver.ApplianceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CookingMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Appliance");
                });

            modelBuilder.Entity("UltraSaver.IngredientModel", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("CookingMethod")
                        .HasColumnType("text");

                    b.Property<int>("CookingTime")
                        .HasColumnType("integer");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("Name", "CookingMethod");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("UltraSaver.NewRecipeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CalorieCount")
                        .HasColumnType("integer");

                    b.Property<int>("FullPrepTime")
                        .HasColumnType("integer");

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("UltraSaver.RecipeIngredientModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("IngredientCookingMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("IngredientName", "IngredientCookingMethod");

                    b.ToTable("RecipeIngredient");
                });

            modelBuilder.Entity("UltraSaver.UserLikedRecipeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserEmail");

                    b.ToTable("UserLikedRecipe");
                });

            modelBuilder.Entity("UltraSaver.UserModel", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("DarkMode")
                        .HasColumnType("boolean");

                    b.Property<float>("ElectricityPrice")
                        .HasColumnType("real");

                    b.HasKey("Email");

                    b.ToTable("User");
                });

            modelBuilder.Entity("UltraSaver.UserOwnedApplianceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplianceId")
                        .HasColumnType("integer");

                    b.Property<int>("ApplianceWattage")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplianceId");

                    b.HasIndex("UserEmail");

                    b.ToTable("UserOwnedAppliance");
                });

            modelBuilder.Entity("UltraSaver.UserPropsModel", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<bool>("darkMode")
                        .HasColumnType("boolean");

                    b.HasKey("email");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("UltraSaver.AllergensModel", b =>
                {
                    b.HasOne("UltraSaver.UserModel", "User")
                        .WithOne("Allergens")
                        .HasForeignKey("UltraSaver.AllergensModel", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UltraSaver.RecipeIngredientModel", b =>
                {
                    b.HasOne("UltraSaver.NewRecipeModel", "Recipe")
                        .WithMany("RecipeIngredient")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UltraSaver.IngredientModel", "Ingredient")
                        .WithMany("RecipeIngredient")
                        .HasForeignKey("IngredientName", "IngredientCookingMethod")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("UltraSaver.UserLikedRecipeModel", b =>
                {
                    b.HasOne("UltraSaver.NewRecipeModel", "Recipe")
                        .WithMany("UserLikedRecipe")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UltraSaver.UserModel", "User")
                        .WithMany("UserLikedRecipe")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UltraSaver.UserOwnedApplianceModel", b =>
                {
                    b.HasOne("UltraSaver.ApplianceModel", "Appliance")
                        .WithMany("UserOwnedAppliance")
                        .HasForeignKey("ApplianceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UltraSaver.UserModel", "User")
                        .WithMany("UserOwnedAppliance")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appliance");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UltraSaver.ApplianceModel", b =>
                {
                    b.Navigation("UserOwnedAppliance");
                });

            modelBuilder.Entity("UltraSaver.IngredientModel", b =>
                {
                    b.Navigation("RecipeIngredient");
                });

            modelBuilder.Entity("UltraSaver.NewRecipeModel", b =>
                {
                    b.Navigation("RecipeIngredient");

                    b.Navigation("UserLikedRecipe");
                });

            modelBuilder.Entity("UltraSaver.UserModel", b =>
                {
                    b.Navigation("Allergens")
                        .IsRequired();

                    b.Navigation("UserLikedRecipe");

                    b.Navigation("UserOwnedAppliance");
                });
#pragma warning restore 612, 618
        }
    }
}
