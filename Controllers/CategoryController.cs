using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Recipendium_Reparsed.Data;
using Recipendium_Reparsed.Models;

namespace Recipendium_Reparsed.Controllers;

/// <summary>
/// Handles all category-related operations including listing, creating, editing, and deleting categories.
/// </summary>
public class CategoryController : Controller
{
    private readonly RecipeDbContext _context;
    private readonly ILogger<CategoryController> _logger;

    /// <summary>
    /// Creates a new instance of CategoryController with the specified database context and logger.
    /// </summary>
    public CategoryController(RecipeDbContext context, ILogger<CategoryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Displays a list of all categories.
    /// GET: /Category/Index
    /// </summary>
    public IActionResult Index()
    {
        try
        {
            var categories = _context.Categories.Include(c => c.Recipes).ToList();
            return View(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving categories.");
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Displays detailed information about a specific category.
    /// GET: /Category/Details/5
    /// </summary>
    public IActionResult Details(int id)
    {
        try
        {
            var category = _context.Categories.Include(c => c.Recipes).FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving category with ID: {CategoryId}", id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Shows the create category form.
    /// GET: /Category/Create
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Recipes"] = _context.Recipes.ToList();
        return View();
    }

    /// <summary>
    /// Processes the create category form submission and saves the new category.
    /// POST: /Category/Create
    /// </summary>
    [HttpPost]
    public IActionResult Create(Category category, int[]? selectedRecipes)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Recipes"] = _context.Recipes.ToList();
            return View(category);
        }

        try
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            // Assign selected recipes to the new category
            if (selectedRecipes != null)
            {
                var recipes = _context.Recipes.Where(r => selectedRecipes.Contains(r.Id)).ToList();
                foreach (var recipe in recipes)
                {
                    recipe.CategoryId = category.Id;
                }
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a category.");
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Shows the edit category form with the existing category data.
    /// GET: /Category/Edit/5
    /// </summary>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        try
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving category for editing with ID: {CategoryId}", id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Processes the edit category form submission and updates the existing category.
    /// POST: /Category/Edit
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        try
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating category with ID: {CategoryId}", category.Id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Shows the delete confirmation page for a specific category.
    /// GET: /Category/Delete/5
    /// </summary>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        try
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving category for deletion with ID: {CategoryId}", id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }

    /// <summary>
    /// Processes the delete category form submission and removes the category from the database.
    /// POST: /Category/Delete
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Delete(Category category)
    {
        try
        {
            var existing = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existing == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(existing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting category with ID: {CategoryId}", category.Id);
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
    }
}
