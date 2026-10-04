using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipendium_Reparsed.Data;
using Recipendium_Reparsed.Models;

namespace Recipendium_Reparsed.Controllers;

public class RecipeController : Controller
{
    private readonly RecipeDbContext _context;

    public RecipeController(RecipeDbContext context)
    {
        _context = context;
    }

    // GET
    public IActionResult Index()
    {
        return View(_context.Recipes.ToList());
    }

    public IActionResult Details(int id)
    {
        var recipe = _context.Recipes.FirstOrDefault(t => t.Id == id);

        if (recipe == null)
        {
            return NotFound();       
        }

        return View(recipe);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

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
}
