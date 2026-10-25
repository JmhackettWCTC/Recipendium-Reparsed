using System.ComponentModel.DataAnnotations;

namespace Recipendium_Reparsed.Models;

/// <summary>
/// Represents a recipe with its details, ingredients, and instructions.
/// </summary>
public class Recipe
{
    /// <summary>
    /// Unique identifier for the recipe.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name/title of the recipe.
    /// </summary>
    [Required(ErrorMessage = "Recipe name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Recipe name must be between 2 and 100 characters.")]
    [Display(Name = "Recipe Name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Foreign key referencing the Category table.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Navigation property to the related Category.
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// Preparation time in minutes (stored as string, converted to int for DB).
    /// </summary>
    [Required(ErrorMessage = "Time is required.")]
    [StringLength(50, ErrorMessage = "Time cannot be longer than 50 characters.")]
    [Display(Name = "Preparation Time")]
    public string Time { get; set; } = "";

    /// <summary>
    /// Number of servings (stored as string, converted to int for DB).
    /// </summary>
    [Required(ErrorMessage = "Servings is required.")]
    [StringLength(50, ErrorMessage = "Servings cannot be longer than 50 characters.")]
    [Display(Name = "Servings")]
    public string Servings { get; set; } = "";

    /// <summary>
    /// Comma-separated list of ingredients.
    /// </summary>
    [Required(ErrorMessage = "Ingredients are required.")]
    [Display(Name = "Ingredients")]
    public string Ingredients { get; set; } = "";

    /// <summary>
    /// Pipe-separated list of cooking instructions/steps.
    /// </summary>
    [Required(ErrorMessage = "Instructions are required.")]
    [Display(Name = "Instructions")]
    public string Instructions { get; set; } = "";

    /// <summary>
    /// Path to the recipe's image file.
    /// </summary>
    [Display(Name = "Image")]
    public string Image { get; set; } = "";

    /// <summary>
    /// Brief summary of the recipe shown on listing pages.
    /// </summary>
    [Required(ErrorMessage = "Short description is required.")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Short description must be between 5 and 200 characters.")]
    [Display(Name = "Short Description")]
    public string ShortDescription { get; set; } = "";

    /// <summary>
    /// Full detailed description of the recipe shown on the details page.
    /// </summary>
    [Required(ErrorMessage = "Long description is required.")]
    [Display(Name = "Long Description")]
    public string LongDescription { get; set; } = "";

    /// <summary>
    /// User rating from 0 to 5.
    /// </summary>
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    [Display(Name = "Rating")]
    public double Rating { get; set; }
}