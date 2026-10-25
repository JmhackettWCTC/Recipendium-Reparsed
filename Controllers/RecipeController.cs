using Microsoft.AspNetCore.Mvc;
using Recipendium_Reparsed.Data;
using Recipendium_Reparsed.Models;

namespace Recipendium_Reparsed.Controllers;

/// <summary>
/// Handles all recipe-related operations including listing, creating, editing, and deleting recipes.
/// </summary>
public class RecipeController : Controller
{
    private readonly RecipeDbContext _context;

    /// <summary>
    /// Creates a new instance of RecipeController with the specified database context.
    /// </summary>
    public RecipeController(RecipeDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Displays a list of all recipes.
    /// GET: /Recipe/Index
    /// </summary>
    public IActionResult Index()
    {
        return View(_context.Recipes.ToList());
    }

    /// <summary>
    /// Displays detailed information about a specific recipe.
    /// GET: /Recipe/Details/5
    /// </summary>
    public IActionResult Details(int id)
    {
        var recipe = _context.Recipes.FirstOrDefault(t => t.Id == id);

        if (recipe == null)
        {
            return NotFound();       
        }

        return View(recipe);
    }

    /// <summary>
    /// Shows the create recipe form.
    /// GET: /Recipe/Create
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
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
            return View(recipe);
        }

        _context.Recipes.Add(recipe);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Shows the edit recipe form with the existing recipe data.
    /// GET: /Recipe/Edit/5
    /// </summary>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var recipe = _context.Recipes.FirstOrDefault(t => t.Id == id);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
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
            return View(recipe);
        }

        _context.Recipes.Update(recipe);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Shows the delete confirmation page for a specific recipe.
    /// GET: /Recipe/Delete/5
    /// </summary>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var recipe = _context.Recipes.FirstOrDefault(t => t.Id == id);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    /// <summary>
    /// Processes the delete recipe form submission and removes the recipe from the database.
    /// POST: /Recipe/Delete
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Delete(Recipe recipe)
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
}
