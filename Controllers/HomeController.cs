using Microsoft.AspNetCore.Mvc;

namespace Recipendium_Reparsed.Controllers;

/// <summary>
/// Handles the home page and navigation to recipe sections.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Displays the home page with a welcome message and links to recipes.
    /// GET: /Home/Index
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }
}