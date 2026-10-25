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
}
