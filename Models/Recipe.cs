using System.ComponentModel.DataAnnotations;

namespace Recipendium_Reparsed.Models;

public class Recipe
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Recipe name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Recipe name must be between 2 and 100 characters.")]
    [Display(Name = "Recipe Name")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Category is required.")]
    [Display(Name = "Category")]
    public string Category { get; set; } = "";

    [Required(ErrorMessage = "Time is required.")]
    [StringLength(50, ErrorMessage = "Time cannot be longer than 50 characters.")]
    [Display(Name = "Preparation Time")]
    public string Time { get; set; } = "";

    [Required(ErrorMessage = "Servings is required.")]
    [StringLength(50, ErrorMessage = "Servings cannot be longer than 50 characters.")]
    [Display(Name = "Servings")]
    public string Servings { get; set; } = "";

    [Required(ErrorMessage = "Ingredients are required.")]
    [Display(Name = "Ingredients")]
    public string Ingredients { get; set; } = "";

    [Required(ErrorMessage = "Instructions are required.")]
    [Display(Name = "Instructions")]
    public string Instructions { get; set; } = "";

    [Display(Name = "Image")]
    public string Image { get; set; } = "";

    [Required(ErrorMessage = "Short description is required.")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Short description must be between 5 and 200 characters.")]
    [Display(Name = "Short Description")]
    public string ShortDescription { get; set; } = "";

    [Required(ErrorMessage = "Long description is required.")]
    [Display(Name = "Long Description")]
    public string LongDescription { get; set; } = "";

    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    [Display(Name = "Rating")]
    public double Rating { get; set; }
}