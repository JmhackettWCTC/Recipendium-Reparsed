using Microsoft.AspNetCore.Mvc;
using Recipendium_Reparsed.Models;

namespace Recipendium_Reparsed.Controllers;

public class RecipeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(RecipeData.All);
    }

    public IActionResult Details(int id)
    {
        var recipe = RecipeData.All.FirstOrDefault(t => t.Id == id);

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

        recipe.Id = RecipeData.All.Any() ? RecipeData.All.Max(r => r.Id) + 1 : 1;
        RecipeData.All.Add(recipe);
        return RedirectToAction(nameof(Index));
    }
}