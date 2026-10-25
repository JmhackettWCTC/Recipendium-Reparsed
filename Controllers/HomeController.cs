using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Recipendium_Reparsed.Controllers;

/// <summary>
/// Handles the home page and navigation to recipe sections.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// Creates a new instance of HomeController with logging support.
    /// </summary>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Displays the home page with a welcome message and links to recipes.
    /// GET: /Home/Index
    /// </summary>
    public IActionResult Index()
    {
        return View();
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