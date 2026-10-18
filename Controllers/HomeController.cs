using Microsoft.AspNetCore.Mvc;

namespace Recipendium_Reparsed.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}