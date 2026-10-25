using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Recipendium_Reparsed.Data;
using Recipendium_Reparsed.Models;

namespace Recipendium_Reparsed.Controllers;

/// <summary>
/// Handles all recipe-related operations including listing, creating, editing, and deleting recipes.
/// </summary>
public class RecipeController : Controller
{
    private readonly RecipeDbContext _context;
    private readonly ILogger<RecipeController> _logger;

    /// <summary>
    /// Creates a new instance of RecipeController with the specified database context and logger.
    /// </summary>
    public RecipeController(RecipeDbContext context, ILogger<RecipeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Displays a list of recipes with optional category filtering and sorting.
    /// GET: /Recipe/Index
    /// </summary>
    public IActionResult Index(string? category, string? sort, string? search)
    {
        try
        {
            var query = _context.Recipes.Include(r => r.Category).AsQueryable();

            // Filter by category if specified
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(r => r.Category != null && r.Category.Name == category);
            }

            // Filter by search query if specified
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(r => r.Name.Contains(search) 
                    || r.ShortDescription.Contains(search) 
                    || r.LongDescription.Contains(search)
                    || r.Ingredients.Contains(search));
            }

            // Apply sorting
            sort = sort ?? "nameAsc";
            query = sort switch
            {
                "nameDesc" => query.OrderByDescending(r => r.Name),
                "ratingAsc" => query.OrderBy(r => r.Rating),
                "ratingDesc" => query.OrderByDescending(r => r.Rating),
                "timeAsc" => query.OrderBy(r => r.Time),
                "timeDesc" => query.OrderByDescending(r => r.Time),
                _ => query.OrderBy(r => r.Name)
            };

            var recipes = query.ToList();

            // Populate categories for filter dropdown
            ViewData["Categories"] = _context.Categories.ToList();
            ViewData["CurrentCategory"] = category;
            ViewData["CurrentSort"] = sort;
            ViewData["CurrentSearch"] = search;

            return View(recipes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving recipes.");
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Displays detailed information about a specific recipe.
    /// GET: /Recipe/Details/5
    /// </summary>
    public IActionResult Details(int id)
    {
        try
        {
            var recipe = _context.Recipes.Include(r => r.Category).FirstOrDefault(t => t.Id == id);

            if (recipe == null)
            {
                return NotFound();       
            }

            return View(recipe);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving recipe with ID: {RecipeId}", id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Shows the create recipe form.
    /// GET: /Recipe/Create
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Categories"] = _context.Categories.ToList();
        return View();
    }

    /// <summary>
    /// Maps a CategoryId to its default image path.
    /// </summary>
    private string GetDefaultImageForCategory(int categoryId)
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

    /// <summary>
    /// Processes the create recipe form submission and saves the new recipe.
    /// POST: /Recipe/Create
    /// </summary>
    [HttpPost]
    public IActionResult Create(Recipe recipe)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Categories"] = _context.Categories.ToList();
            return View(recipe);
        }

        try
        {
            // Assign default image based on category if no image is provided
            if (string.IsNullOrEmpty(recipe.Image))
            {
                recipe.Image = GetDefaultImageForCategory(recipe.CategoryId);
            }

            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a recipe.");
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Shows the edit recipe form with the existing recipe data.
    /// GET: /Recipe/Edit/5
    /// </summary>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        try
        {
            var recipe = _context.Recipes.Include(r => r.Category).FirstOrDefault(t => t.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            ViewData["Categories"] = _context.Categories.ToList();
            return View(recipe);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving recipe for editing with ID: {RecipeId}", id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Processes the edit recipe form submission and updates the existing recipe.
    /// POST: /Recipe/Edit
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(Recipe recipe)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Categories"] = _context.Categories.ToList();
            return View(recipe);
        }

        try
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating recipe with ID: {RecipeId}", recipe.Id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Shows the delete confirmation page for a specific recipe.
    /// GET: /Recipe/Delete/5
    /// </summary>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        try
        {
            var recipe = _context.Recipes.Include(r => r.Category).FirstOrDefault(t => t.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving recipe for deletion with ID: {RecipeId}", id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Processes the delete recipe form submission and removes the recipe from the database.
    /// POST: /Recipe/Delete
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Delete(Recipe recipe)
    {
        try
        {
            var existing = _context.Recipes.FirstOrDefault(t => t.Id == recipe.Id);
            if (existing == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(existing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting recipe with ID: {RecipeId}", recipe.Id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }
}
