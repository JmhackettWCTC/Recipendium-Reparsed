using System.ComponentModel.DataAnnotations;

namespace Recipendium_Reparsed.Models;

public class Recipe
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Recipe name is required.")]
    [StringLength(100, ErrorMessage = "Recipe name cannot be longer than 100 characters.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; } = "";

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
    [StringLength(200, ErrorMessage = "Short description cannot be longer than 200 characters.")]
    public string ShortDescription { get; set; } = "";

    [Required(ErrorMessage = "Long description is required.")]
    public string LongDescription { get; set; } = "";

    public double Rating { get; set; }
}