using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Recipendium_Reparsed.Data;
using Recipendium_Reparsed.Models;

namespace Recipendium_Reparsed.Controllers;

/// <summary>
/// Handles the home page and navigation to recipe sections.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RecipeDbContext _context;

    /// <summary>
    /// Creates a new instance of HomeController with logging support and database context.
    /// </summary>
    public HomeController(ILogger<HomeController> logger, RecipeDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /// <summary>
    /// Displays the home page with a welcome message, featured recipes, and links to recipes.
    /// GET: /Home/Index
    /// </summary>
    public IActionResult Index()
    {
        try
        {
            var featuredRecipes = _context.Recipes
                .Where(r => r.Featured)
                .OrderByDescending(r => r.Rating)
                .Take(6)
                .Select(r => new
                {
                    r.Id,
                    r.Name,
                    r.Image,
                    r.ShortDescription,
                    r.Time,
                    r.Servings,
                    r.Rating,
                    CategoryName = r.Category != null ? r.Category.Name : ""
                })
                .ToList();

            ViewData["FeaturedRecipes"] = System.Text.Json.JsonSerializer.Serialize(featuredRecipes);
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving featured recipes.");
            ViewData["FeaturedRecipes"] = System.Text.Json.JsonSerializer.Serialize(new List<object>());
            return View();
        }
    }

    /// <summary>
    /// Displays a friendly error page when an exception occurs or a resource is not found.
    /// GET: /Home/Error
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode = null)
    {
        if (statusCode.HasValue)
        {
            ViewData["StatusCode"] = statusCode.Value;
        }

        _logger.LogError("An error occurred. Status Code: {StatusCode}", statusCode ?? 500);

        return View();
    }
}