namespace Recipendium_Reparsed.Models.Dtos;

/// <summary>
/// Data transfer object for a recipe in API responses.
/// Uses flat properties to avoid circular references between Recipe and Category.
/// </summary>
public class RecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
    public string Time { get; set; } = "";
    public string Servings { get; set; } = "";
    public string Ingredients { get; set; } = "";
    public string Instructions { get; set; } = "";
    public string Image { get; set; } = "";
    public string ShortDescription { get; set; } = "";
    public string LongDescription { get; set; } = "";
    public double Rating { get; set; }
    public bool Featured { get; set; }
    public List<SubRecipeDto> SubRecipes { get; set; } = new();
}
