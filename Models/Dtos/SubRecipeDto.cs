namespace Recipendium_Reparsed.Models.Dtos;

/// <summary>
/// Data transfer object for a sub-recipe in API responses.
/// </summary>
public class SubRecipeDto
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Name { get; set; } = "";
    public string Ingredients { get; set; } = "";
    public string Instructions { get; set; } = "";
    public int Order { get; set; }
}
