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

    public IActionResult Create()
    {
        return View();
    }
}