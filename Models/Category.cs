using System.ComponentModel.DataAnnotations;

namespace Recipendium_Reparsed.Models;

/// <summary>
/// Represents a category that recipes can belong to.
/// </summary>
public class Category
{
    /// <summary>
    /// Unique identifier for the category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the category (e.g., Breakfast, Lunch, Dinner).
    /// </summary>
    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Category name must be between 1 and 50 characters.")]
    [Display(Name = "Category Name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// URL or path to the category's default image.
    /// </summary>
    public string Image { get; set; } = "";

    /// <summary>
    /// Brief description of the category.
    /// </summary>
    [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
    [Display(Name = "Description")]
    public string Description { get; set; } = "";

    /// <summary>
    /// URL-friendly slug for the category name.
    /// </summary>
    [StringLength(50, ErrorMessage = "Slug cannot be longer than 50 characters.")]
    [Display(Name = "Slug")]
    public string Slug { get; set; } = "";

    /// <summary>
    /// Navigation property for all recipes in this category.
    /// </summary>
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
