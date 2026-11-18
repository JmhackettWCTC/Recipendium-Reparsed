using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipendium_Reparsed.Data;
using Recipendium_Reparsed.Models;
using Recipendium_Reparsed.Models.Dtos;

namespace Recipendium_Reparsed.Controllers;

/// <summary>
/// REST API controller for recipe operations.
/// Returns JSON responses using DTOs to avoid circular references.
/// </summary>
[ApiController]
[Route("api/recipes")]
public class RecipesApiController : ControllerBase
{
    private readonly RecipeDbContext _context;

    /// <summary>
    /// Creates a new instance of RecipesApiController with the specified database context.
    /// </summary>
    public RecipesApiController(RecipeDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all recipes as a JSON list.
    /// GET: /api/recipes
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetAll()
    {
        var recipes = await _context.Recipes
            .Include(r => r.Category)
            .Include(r => r.SubRecipes)
            .ToListAsync();

        return Ok(recipes.Select(MapToDto));
    }

    /// <summary>
    /// Retrieves a single recipe by ID with category name and sub-recipes.
    /// GET: /api/recipes/5
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<RecipeDto>> GetById(int id)
    {
        var recipe = await _context.Recipes
            .Include(r => r.Category)
            .Include(r => r.SubRecipes)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null)
        {
            return NotFound(new { message = $"Recipe with ID {id} not found." });
        }

        return Ok(MapToDto(recipe));
    }

    /// <summary>
    /// Creates a new recipe from the request body.
    /// Requires Admin role. Returns 401 when signed out, 403 without Admin role.
    /// POST: /api/recipes
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RecipeDto>> Post([FromBody] CreateRecipeDto dto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var recipe = new Recipe
        {
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Time = dto.Time,
            Servings = dto.Servings,
            Ingredients = dto.Ingredients,
            Instructions = dto.Instructions,
            Image = string.IsNullOrEmpty(dto.Image) ? GetDefaultImageForCategory(dto.CategoryId) : dto.Image,
            ShortDescription = dto.ShortDescription,
            LongDescription = dto.LongDescription,
            Rating = dto.Rating,
            Featured = dto.Featured
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        var createdDto = MapToDto(recipe);
        return CreatedAtAction(nameof(GetById), new { id = recipe.Id }, createdDto);
    }

    /// <summary>
    /// Deletes a recipe by ID.
    /// Requires Admin role. Returns 401 when signed out, 403 without Admin role.
    /// DELETE: /api/recipes/5
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {

        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound(new { message = $"Recipe with ID {id} not found." });
        }

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Maps a Recipe entity to a RecipeDto, including category name and sub-recipes.
    /// </summary>
    private static RecipeDto MapToDto(Recipe recipe)
    {
        return new RecipeDto
        {
            Id = recipe.Id,
            Name = recipe.Name,
            CategoryId = recipe.CategoryId,
            CategoryName = recipe.Category?.Name ?? "",
            Time = recipe.Time,
            Servings = recipe.Servings,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            Image = recipe.Image,
            ShortDescription = recipe.ShortDescription,
            LongDescription = recipe.LongDescription,
            Rating = recipe.Rating,
            Featured = recipe.Featured,
            SubRecipes = recipe.SubRecipes?.Select(sr => new SubRecipeDto
            {
                Id = sr.Id,
                RecipeId = sr.RecipeId,
                Name = sr.Name,
                Ingredients = sr.Ingredients,
                Instructions = sr.Instructions,
                Order = sr.Order
            }).ToList() ?? new List<SubRecipeDto>()
        };
    }

    /// <summary>
    /// Maps a CategoryId to its default image path.
    /// </summary>
    private static string GetDefaultImageForCategory(int categoryId)
    {
        return categoryId switch
        {
            1 => "/images/categories/breakfast.svg",
            2 => "/images/categories/lunch.svg",
            3 => "/images/categories/dinner.svg",
            4 => "/images/categories/dessert.svg",
            5 => "/images/categories/snack.svg",
            6 => "/images/categories/drink.svg",
            _ => "/images/categories/dinner.svg"
        };
    }
}
