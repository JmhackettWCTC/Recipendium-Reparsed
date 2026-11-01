using System.ComponentModel.DataAnnotations;

namespace Recipendium_Reparsed.Models;

/// <summary>
/// Represents a sub-recipe within a main recipe (e.g., a dressing, sauce, or side component).
/// </summary>
public class SubRecipe
{
    /// <summary>
    /// Unique identifier for the sub-recipe.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key referencing the parent Recipe.
    /// </summary>
    public int RecipeId { get; set; }

    /// <summary>
    /// Navigation property to the parent Recipe.
    /// </summary>
    public Recipe? Recipe { get; set; }

    /// <summary>
    /// Name/title of the sub-recipe (e.g., "Vinaigrette Dressing").
    /// </summary>
    [Required(ErrorMessage = "Sub-recipe name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Sub-recipe name must be between 2 and 100 characters.")]
    [Display(Name = "Sub-Recipe Name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Comma-separated list of ingredients for the sub-recipe.
    /// </summary>
    [Required(ErrorMessage = "Ingredients are required.")]
    [Display(Name = "Ingredients")]
    public string Ingredients { get; set; } = "";

    /// <summary>
    /// Pipe-separated list of instructions/steps for the sub-recipe.
    /// </summary>
    [Required(ErrorMessage = "Instructions are required.")]
    [Display(Name = "Instructions")]
    public string Instructions { get; set; } = "";

    /// <summary>
    /// Display order within the parent recipe.
    /// </summary>
    [Display(Name = "Order")]
    public int Order { get; set; }
}
