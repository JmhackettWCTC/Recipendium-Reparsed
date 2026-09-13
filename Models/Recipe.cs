namespace Recipendium_Reparsed.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public string Time { get; set; } = "";
    public string Servings { get; set; } = "";
    public string Ingredients { get; set; } = "";
    public string Instructions { get; set; } = "";
    public string Image { get; set; } = "";
    public string ShortDescription { get; set; } = "";
    public string LongDescription { get; set; } = "";
    public double Rating { get; set; }
    
}