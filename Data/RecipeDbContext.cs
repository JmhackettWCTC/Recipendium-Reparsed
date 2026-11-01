

using Microsoft.EntityFrameworkCore;
using Recipendium_Reparsed.Models;

namespace Recipendium_Reparsed.Data
{
    /// <summary>
    /// Database context for the Recipendium application.
    /// Manages Recipe entities.
    /// </summary>
    public class RecipeDbContext : DbContext
    {
        /// <summary>
        /// Collection of Recipe entities.
        /// </summary>
        public DbSet<Recipe> Recipes { get; set; }

        /// <summary>
        /// Collection of Category entities.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Collection of SubRecipe entities.
        /// </summary>
        public DbSet<SubRecipe> SubRecipes { get; set; }

        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configures the entity column mappings and seeds initial data.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");
                entity.Property(e => e.Name).HasColumnName("Title");
                entity.Property(e => e.Time).HasColumnName("CookTime")
                    .HasConversion((string v) => StringToIntConverter(v), (int v) => IntToStringConverter(v));
                entity.Property(e => e.Servings).HasColumnName("Servings")
                    .HasConversion((string v) => StringToIntConverter(v), (int v) => IntToStringConverter(v));
                entity.Property(e => e.Image).HasColumnName("ImagePath");
                entity.Property(e => e.ShortDescription).HasColumnName("ShortDescription");
                entity.Property(e => e.LongDescription).HasColumnName("LongDescription");
                entity.Property(e => e.Rating).HasColumnName("Rating").HasDefaultValue(0);
                entity.Property(e => e.Featured).HasColumnName("Featured").HasDefaultValue(false);
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.HasOne(r => r.Category)
                    .WithMany(c => c.Recipes)
                    .HasForeignKey(r => r.CategoryId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.Property(e => e.Id).HasColumnName("CategoryID");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Image).HasColumnName("image");
                entity.Property(e => e.Description).HasColumnName("Description");
                entity.Property(e => e.Slug).HasColumnName("Slug");
            });

            modelBuilder.Entity<SubRecipe>(entity =>
            {
                entity.ToTable("SubRecipe");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Ingredients).HasColumnName("Ingredients");
                entity.Property(e => e.Instructions).HasColumnName("Instructions");
                entity.Property(e => e.Order).HasColumnName("Order");
                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
                entity.HasOne(s => s.Recipe)
                    .WithMany(r => r.SubRecipes)
                    .HasForeignKey(s => s.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        /// <summary>
        /// Converts a string value to an integer for database storage.
        /// Returns 0 if the conversion fails.
        /// </summary>
        private static int StringToIntConverter(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }

        /// <summary>
        /// Converts an integer value from the database back to a string.
        /// </summary>
        private static string IntToStringConverter(int value)
        {
            return value.ToString();
        }
    }
}