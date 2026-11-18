using System.ComponentModel.DataAnnotations;

namespace Recipendium_Reparsed.Models.Dtos;

/// <summary>
/// Data transfer object for creating a new recipe via the API.
/// Includes validation attributes matching the Recipe entity.
/// </summary>
public class CreateRecipeDto
{
    [Required(ErrorMessage = "Recipe name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Recipe name must be between 2 and 100 characters.")]
    public string Name { get; set; } = "";

    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Time is required.")]
    [StringLength(50, ErrorMessage = "Time cannot be longer than 50 characters.")]
    public string Time { get; set; } = "";

    [Required(ErrorMessage = "Servings is required.")]
    [StringLength(50, ErrorMessage = "Servings cannot be longer than 50 characters.")]
    public string Servings { get; set; } = "";

    [Required(ErrorMessage = "Ingredients are required.")]
    public string Ingredients { get; set; } = "";

    [Required(ErrorMessage = "Instructions are required.")]
    public string Instructions { get; set; } = "";

    public string Image { get; set; } = "";

    [Required(ErrorMessage = "Short description is required.")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Short description must be between 5 and 200 characters.")]
    public string ShortDescription { get; set; } = "";

    [Required(ErrorMessage = "Long description is required.")]
    public string LongDescription { get; set; } = "";

    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public double Rating { get; set; }

    public bool Featured { get; set; }
}
